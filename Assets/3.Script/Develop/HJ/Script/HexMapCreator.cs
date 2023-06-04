using System.Collections;
using UnityEngine;


//육각형의 가로 세로
public class HexSixe
{
    public float hexHeight = 3.332823f;
    public float hexWidth = 2.886311f;
}

//[System.Serializable]
//public class MapSaveData
//{
//    public int index;
//    public int mapType = 0;
//}

public class HexMapCreator : MonoBehaviour
{

    HexSixe hexSixe = new HexSixe();

    //아래 사이즈의 육각형 맵을 만들자
    private int width = 60;
    private int height = 50;

    public int mapSize;

    //materials (바다=0, 숲=1, 평원=2)
    [SerializeField] Material[] materials = new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    public HexMember[] hexMembers;
    [SerializeField] GameObject groundBFobj;
    [SerializeField] GameObject groundBPobj;

    //이동횟수를 표시할 캔버스
    private Canvas gridCanvas;

    //전체 그리드에 이미지를 까니까 너무 렉이 걸림
    //차라리 0~10 이미지를 배열에 담아서
    //이동경로 노드에 띄우면 어떰

    //나중에 에이스타 테스트할때 사용할거임
    //[SerializeField] private Text gridText; //나중에 이미지 교체할거임
    //private Text[] gridTexts; //인덱스 정보로 관리할예정 (hexMembers[0] =짝꿍= gridImages[0])


    //시작 센터
    int centerIndex = 1350;
    //퍼지는 횟수
    int extendSize = 50;
    int randomNum;
    bool isLoop = true;

    public int forestNodeCount = 0;
    public int plainsNodeCount = 0;


    bool plainsStartDir;

    [SerializeField] GameObject mapObjectCreatorObj;
    GameObject mapObjectCreatorObjsave = null;

    public LowPolyWater lowPolyWater;
    public HexMember lowPolyWataer;

    [SerializeField] GameObject cloudBoxObj;
    GameObject cloudBoxObjsave = null;

    bool isReload = false;
    int reloadCount = 0;
    private void Start()
    {
        mapSize = height * width;

        //gridCanvas = GetComponentInChildren<Canvas>();

        //지정한 개수만큼 생성
        hexMembers = new HexMember[mapSize];
        //gridTexts = new Text[mapSize];


        //x와 z가 좌표가 된다
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHex(x, z, i++);
            }
        }


        //땅 영역 지정
        SetGround();

        if (isReload)
        {
            isReload = false;
            return;
        }

        //맵 오브젝트 크리에이터 생성 & 구름 생성
        StartCoroutine(StartMapObjectCreate_co());

        StartCoroutine(GroundBottomCo());
    }

    int resetCount = 0;
    //맵을 다시 만든다 
    public void ResetMap()
    {
        resetCount++;
        if(resetCount > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        Debug.Log("맵 다시 로드");
        StopAllCoroutines();

        reloadCount++;
        if (reloadCount > 6)
        {
            return;
        }

        if (cloudBoxObjsave != null)
        {
            Destroy(cloudBoxObjsave);
            Destroy(mapObjectCreatorObjsave);
            cloudBoxObjsave = null;
            mapObjectCreatorObjsave = null;
        }

        centerIndex = 1350;
        extendSize = 50;
        isLoop = true;
        forestNodeCount = 0;
        plainsNodeCount = 0;


        for(int i=0; i<mapSize; i++)
        {
            if (hexMembers[i].mapType != 0)
            {
                hexMembers[i].SetMapType(0);
                hexMembers[i].doNotUse = false;
                Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
                renderer.material = materials[0];
            }
        }

        if (isReload)
        {
            isReload = false;
            return;
        }

        //땅 영역 지정
        SetGround();

        //맵 오브젝트 크리에이터 생성 & 구름 생성
        StartCoroutine(StartMapObjectCreate_co());

        StartCoroutine(GroundBottomCo());
    }

    private void CreateHex(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
        position.y = -0.5f;
        position.z = z * (hexSixe.hexHeight * 0.75f);

        HexMember hex = hexMembers[i] = Instantiate(hexMember);
        hex.transform.SetParent(transform, false);
        hex.transform.localPosition = position;

        hex.SetHexMemberData(x, z, i); //맵타입 설정 필요


        //이웃 설정
        if (x > 0)
        {
            hex.SetNeighbor(HexDirection.W, hexMembers[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                hex.SetNeighbor(HexDirection.SE, hexMembers[i - width]);
                if (x > 0)
                {
                    hex.SetNeighbor(HexDirection.SW, hexMembers[i - width - 1]);
                }
            }
            else
            {
                hex.SetNeighbor(HexDirection.SW, hexMembers[i - width]);
                if (x < width - 1)
                {
                    hex.SetNeighbor(HexDirection.SE, hexMembers[i - width + 1]);
                }
            }
        }

    }

    //땅이 될 노드를 지정한다
    private void SetGround()
    {
        //포레스트
        ForestNode();
        int loopNum = 0;
        while (forestNodeCount < 230)
        {
            ForestNode();
            if (isReload)
            {
                return;
            }
            if (loopNum++ > 1000)
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                //노드를 지우고 정보도 리셋
                ResetMap();
                isReload = true;
                return;
                throw new System.Exception("숲이작아");
            }
        }


        //황금평원 시작점 찾기
        loopNum = 0;
        if (hexMembers[centerIndex].xNum >= 30)
        {
            plainsStartDir = true;
        }
        else
        {
            plainsStartDir = false;
        }

        while (isLoop)
        {
            switch (plainsStartDir)
            {
                case true: //1기준왼쪽으로
                    centerIndex -= 2;
                    break;

                case false: //1기준 오른쪽으로 
                    centerIndex += 2;
                    break;
            }

            if (hexMembers[centerIndex].mapType != 1)
            {
                isLoop = false;
                break;
            }

            if (loopNum++ > 1000)
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                ResetMap();
                isReload = true;
                return;
                throw new System.Exception("Infinite Loop : 1");
            }
        }
        isLoop = true;


        //황금평원
        loopNum = 0;
        PlainsNode();
        while (plainsNodeCount < 180)
        {
            PlainsNode();
            if (isReload)
            {
                return;
            }
            if (loopNum++ > 1000)
            {
                ResetMap();
                isReload = true;
                return;
                throw new System.Exception("평원이작아");
            }
        }


        //바다 영역에 LowPolyWater 스크립트를 부여한다
        AddLowPolyWater();
    }


    IEnumerator StartMapObjectCreate_co()
    {
        yield return null; //hexMembers[]가 채워지고 다음 프레임에 실행

        cloudBoxObjsave = Instantiate(cloudBoxObj);
        mapObjectCreatorObjsave = Instantiate(mapObjectCreatorObj);

        yield break;
    }


    void ForestNode()
    {
        for (int c = 0; c < extendSize; c++)
        {
            for (int d = 0; d < 6; d++)
            {
                ChangeMaterial(hexMembers[centerIndex].neighbors[d].index, 1);
            }

            int loopNum = 0;
            while (isLoop)
            {
                randomNum = Random.Range(0, 6);
                if (hexMembers[centerIndex].neighbors[randomNum].xNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].xNum < 49 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum < 39)
                {
                    isLoop = false;
                }

                if (loopNum++ > 1000)
                {
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                    ResetMap();
                    isReload = true;
                    return;
                    throw new System.Exception("Infinite Loop : 2");
                }
            }

            isLoop = true;

            centerIndex = hexMembers[centerIndex].neighbors[randomNum].index;
        }
    }

    void PlainsNode()
    {
        for (int c = 0; c < extendSize; c++)
        {
            for (int d = 0; d < 6; d++)
            {
                ChangeMaterial(hexMembers[centerIndex].neighbors[d].index, 2);
            }


            int loopNum = 0;
            while (isLoop)
            {
                randomNum = Random.Range(0, 6);
                if (hexMembers[centerIndex].neighbors[randomNum].xNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].xNum < 49 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum < 39)
                {
                    if (hexMembers[centerIndex].neighbors[randomNum].mapType != 1)
                    {
                        isLoop = false;
                    }
                }

                if (loopNum++ > 1000)
                {
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                    ResetMap();
                    isReload = true;
                    return;
                    throw new System.Exception("Infinite Loop : 2");
                }
            }

            isLoop = true;

            centerIndex = hexMembers[centerIndex].neighbors[randomNum].index;
        }
    }

    void AddLowPolyWater()
    {
        for (int i = 0; i < hexMembers.Length; i++)
        {
            if (hexMembers[i].mapType.Equals(0))
            {
                hexMembers[i].gameObject.AddComponent<LowPolyWater>();
            }
        }
    }

    //매개변수로 전달받는 index의 Material를 바꾼다
    private void ChangeMaterial(int i, int materialsNumber)
    {
        if (!hexMembers[i].isGround)
        {
            Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
            renderer.material = materials[materialsNumber];
            hexMembers[i].mapType = materialsNumber;
            hexMembers[i].isGround = true;
            hexMembers[i].ispass = true;
            if (materialsNumber.Equals(1))
            {
                forestNodeCount++;
            }
            else if (materialsNumber.Equals(2))
            {
                plainsNodeCount++;
            }

            hexMembers[i].SetMapType(materialsNumber);
        }
    }



    private IEnumerator GroundBottomCo()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        for(int i=0; i<mapSize; i++)
        {
            for(int j=0; j<6; j++)
            {
                if(hexMembers[i].mapType == 1)
                {
                    if (hexMembers[i].neighbors[j].mapType == 0)
                    {
                        GameObject temp = Instantiate(groundBFobj);
                        temp.transform.position = hexMembers[i].transform.position + new Vector3(0f, -1f, 0f);
                        temp.transform.SetParent(gameObject.transform);
                        break;
                    }
                }

                else if (hexMembers[i].mapType == 2)
                {
                    if (hexMembers[i].neighbors[j].mapType == 0)
                    {
                        GameObject temp = Instantiate(groundBPobj);
                        temp.transform.position = hexMembers[i].transform.position + new Vector3(0f, -1f, 0f);
                        temp.transform.SetParent(gameObject.transform);
                        break;
                    }
                }
            }
        }
    }


    //private void CreateGrid(int x, int z, int i)
    //{
    //    Vector3 position;
    //    position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
    //    position.y = (z) * (hexSixe.hexHeight * 0.75f);
    //    position.z = 0f;

    //    Text text = gridTexts[i] = Instantiate(gridText);
    //    text.text = "" + i;
    //    text.transform.SetParent(gridCanvas.transform, false);
    //    text.transform.localPosition = position;
    //}

}
