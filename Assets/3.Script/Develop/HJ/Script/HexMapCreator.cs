using System.Collections;
using UnityEngine;


//�������� ���� ����
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

    //�Ʒ� �������� ������ ���� ������
    private int width = 60;
    private int height = 50;

    public int mapSize;

    //materials (�ٴ�=0, ��=1, ���=2)
    [SerializeField] Material[] materials = new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    public HexMember[] hexMembers;
    [SerializeField] GameObject groundBFobj;
    [SerializeField] GameObject groundBPobj;

    //�̵�Ƚ���� ǥ���� ĵ����
    private Canvas gridCanvas;

    //��ü �׸��忡 �̹����� ��ϱ� �ʹ� ���� �ɸ�
    //���� 0~10 �̹����� �迭�� ��Ƽ�
    //�̵���� ��忡 ���� �

    //���߿� ���̽�Ÿ �׽�Ʈ�Ҷ� ����Ұ���
    //[SerializeField] private Text gridText; //���߿� �̹��� ��ü�Ұ���
    //private Text[] gridTexts; //�ε��� ������ �����ҿ��� (hexMembers[0] =¦��= gridImages[0])


    //���� ����
    int centerIndex = 1350;
    //������ Ƚ��
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

        //������ ������ŭ ����
        hexMembers = new HexMember[mapSize];
        //gridTexts = new Text[mapSize];


        //x�� z�� ��ǥ�� �ȴ�
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHex(x, z, i++);
            }
        }


        //�� ���� ����
        SetGround();

        if (isReload)
        {
            isReload = false;
            return;
        }

        //�� ������Ʈ ũ�������� ���� & ���� ����
        StartCoroutine(StartMapObjectCreate_co());

        StartCoroutine(GroundBottomCo());
    }

    int resetCount = 0;
    //���� �ٽ� ����� 
    public void ResetMap()
    {
        resetCount++;
        if(resetCount > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        Debug.Log("�� �ٽ� �ε�");
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

        //�� ���� ����
        SetGround();

        //�� ������Ʈ ũ�������� ���� & ���� ����
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

        hex.SetHexMemberData(x, z, i); //��Ÿ�� ���� �ʿ�


        //�̿� ����
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

    //���� �� ��带 �����Ѵ�
    private void SetGround()
    {
        //������Ʈ
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
                //��带 ����� ������ ����
                ResetMap();
                isReload = true;
                return;
                throw new System.Exception("�����۾�");
            }
        }


        //Ȳ����� ������ ã��
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
                case true: //1���ؿ�������
                    centerIndex -= 2;
                    break;

                case false: //1���� ���������� 
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


        //Ȳ�����
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
                throw new System.Exception("������۾�");
            }
        }


        //�ٴ� ������ LowPolyWater ��ũ��Ʈ�� �ο��Ѵ�
        AddLowPolyWater();
    }


    IEnumerator StartMapObjectCreate_co()
    {
        yield return null; //hexMembers[]�� ä������ ���� �����ӿ� ����

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

    //�Ű������� ���޹޴� index�� Material�� �ٲ۴�
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
