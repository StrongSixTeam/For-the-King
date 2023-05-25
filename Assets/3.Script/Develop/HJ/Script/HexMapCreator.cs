using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    //MapSaveData[] mapSaveData;
    string path;


    private void Start()
    {
        path = Path.Combine(Application.dataPath + "MapSaveData.json");
        mapSize = height * width;

        gridCanvas = GetComponentInChildren<Canvas>();

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
        //Debug.Log(path);
        //if (File.Exists(path))
        //{
        //    //맵 정보 불러오기
        //    string jsonData = File.ReadAllText(path);
        //    mapSaveData = JsonUtility.FromJson<MapSaveData[]>(jsonData);

        //    for (int i = 0; i > mapSize; i++)
        //    {
        //        if (mapSaveData[i].mapType != 0)
        //        {
        //            ChangeMaterial(i, mapSaveData[i].mapType);
        //        }
        //    }

        //}
        //else
        //{
        //    //땅 영역 지정
        //    SetGround();


        //    mapSaveData = new MapSaveData[forestNodeCount + plainsNodeCount];
        //    int indexCount = 0;

        //    //맵 정보를 저장
        //    for (int i = 0; i < mapSize; i++)
        //    {
        //        if (hexMembers[i].mapType != 0)
        //        {
        //            mapSaveData[indexCount] = new MapSaveData
        //            {
        //                index = i,
        //                mapType = hexMembers[i].mapType
        //            };
        //            indexCount++;
        //        }
        //    }

        //    string jsonData = JsonUtility.ToJson(mapSaveData);
        //    Debug.Log(jsonData);
        //    //File.WriteAllText(path, jsonData);
        //}

        //맵 오브젝트 크리에이터 생성
        StartCoroutine(StartMapObjectCreate_co());
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
        while (forestNodeCount < 220)
        {
            ForestNode();
        }


        //황금평원 시작점 찾기
        int loopNum = 0;
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

            if (loopNum++ > 500)
            {
                throw new System.Exception("Infinite Loop : 1");
            }
        }
        isLoop = true;


        //황금평원
        PlainsNode();
        while (plainsNodeCount < 200)
        {
            PlainsNode();
        }

    }


    IEnumerator StartMapObjectCreate_co()
    {
        yield return null; //hexMembers[]가 채워지고 다음 프레임에 실행

        Instantiate(mapObjectCreatorObj);
        
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

                if (loopNum++ > 500)
                {
                    throw new System.Exception("Infinite Loop : 2");
                }
            }


            isLoop = true;

            centerIndex = hexMembers[centerIndex].neighbors[randomNum].index;
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
