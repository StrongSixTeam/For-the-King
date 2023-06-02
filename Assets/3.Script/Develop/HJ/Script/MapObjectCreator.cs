using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapObjectCreator : MonoBehaviour
{

    HexMapCreator hexMapCreator;
    [SerializeField] GameObject playerSpawner;
    bool isReSet = false;

    Transform fixedObjectBox;
    Transform hideObjectBox;
    Transform obstacleBox;

    //��� ����
    public HexMember[] forestNode;
    public HexMember[] plainsNode;

    [Header("���� ������Ʈ Index")]
    public List<int> objectIndex = new List<int>();
    //[����]
    //����ư=0, ��彺��ũ, �����ǽĵ���, ī������θӸ�, ���νű���, �и���, �����������, ī�����ǽð�, ��ü�����Ͻ�, ���߼���, ������, ��������=11

    [Header("������ ������Ʈ Index")]
    public List<int> randomObjectIndex = new List<int>();
    //[����]
    //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5

    [Header("���� �̸��� Index")]
    public List<int> randomMonsterName = new List<int>();
    public List<int> randomMonsterIndex = new List<int>();
    //[�̸�] randomMonsterName�� ���ڰ� ���ϴ� ���͸�
    //����=0, ��, ����, �Ŵ����, ȩ���, �ذ񺴻�=5, ��, �ٴ��ǳ���, �巡��, ����, �Ź�=10


    //������ ���� GameObject�� �����ϱ����� ����Ʈ
    private List<GameObject> activerandomObject = new List<GameObject>(); //������ ������Ʈ
    private List<GameObject> activeMonster = new List<GameObject>(); //randomMonsterName�� randomMonsterIndex�� ���� ��ġ��

    //_____________________________

    public GameObject[] forestObj = new GameObject[5];
    public GameObject[] plainsObj = new GameObject[4];
    private GameObject[] sanctumObj = new GameObject[3];
    private GameObject[] randomObj = new GameObject[6];
    private GameObject[] ObstacleObj = new GameObject[9];

    [Header("���� ������Ʈ")]
    [SerializeField] GameObject forest01; //����ư(��������) (townForest1)
    [SerializeField] GameObject forest02; //��彺��ũ (townForest2)
    [SerializeField] GameObject forest03; //���� �ǽĵ��� (sealMovingParts)
    [SerializeField] GameObject forest003; //���� �ǽĵ��� (sealBase_0)
    [SerializeField] GameObject forest03Use; //���� �ǽĵ��� (sealBase_0)
    [SerializeField] GameObject forest04; //ī���� ��θӸ� (enCultistA3)
    [SerializeField] GameObject forest05; //���ν� ���� (dungeon05)

    [SerializeField] GameObject plains01; //�и��� (townPlains1)
    [SerializeField] GameObject plains02; //������ ����� (dungeon07)
    [SerializeField] GameObject plains03; //ī������ �ð� (townPlains2)
    [SerializeField] GameObject plains04; //��ü�� ���Ͻ� (licheCrypt_0)

    [Header("���� : ����, ����, ����")]
    [SerializeField] GameObject sanctumFocus;
    [SerializeField] GameObject sanctumLife;
    [SerializeField] GameObject sanctumWisdow;
    [SerializeField] GameObject sanctumUse;


    [Header("��ֹ�")]
    [SerializeField] GameObject tree; //(Tree_Fir)
    [SerializeField] GameObject stoneForest01; //(Stone_2F)
    [SerializeField] GameObject stoneForest02; //(Stone_3F)
    [SerializeField] GameObject stoneForest03; //(Stone_4F)
    [SerializeField] GameObject flower; //(Flowers)

    [SerializeField] GameObject grass; //(hexPlains01_grass)
    [SerializeField] GameObject stonePlains01; //(Stone_2P)
    [SerializeField] GameObject stonePlains02; //(Stone_3P)
    [SerializeField] GameObject stonePlains03; //(Stone_4P)


    [Header("������ ������Ʈ")]
    [SerializeField] GameObject monster01;
    [SerializeField] GameObject monster02;
    [SerializeField] GameObject monster03;
    [SerializeField] GameObject monster04;
    [SerializeField] GameObject exclamation01;
    [SerializeField] GameObject exclamation02;


    [Header("����")]
    [SerializeField] GameObject morningMonster01; //���� enArmoredWolf
    [SerializeField] GameObject morningMonster02; //�� enHawk
    [SerializeField] GameObject morningMonster03; //����  enForestHag
    [SerializeField] GameObject morningMonster04; //�Ŵ���� enCragHulk
    [SerializeField] GameObject morningMonster05; //ȩ��� enHobgoblinA 1
    [SerializeField] GameObject nightMonster01; //�ذ񺴻� enSkellySoldier
    [SerializeField] GameObject nightMonster02; //�� enFairy
    [SerializeField] GameObject nightMonster03; //�ٴ��ǳ��� enHagB
    [SerializeField] GameObject nightMonster04; //�巡�� enDragon
    [SerializeField] GameObject nightMonster05; //���� enBat
    [SerializeField] GameObject nightMonster06; //�Ź� enSpiderB
    [SerializeField] GameObject nightMonster07; //��īƮ���� enCockatriceA_minion

    private GameObject[] morningFMonsterBox = new GameObject[3];
    private GameObject[] morningPMonsterBox = new GameObject[2];
    private GameObject[] nightMonsterBox = new GameObject[7];


    List<int> closeList = new List<int>();


    //HexMapCreator�� �� ������ �Ϸ�� �Ŀ�, �� ��ũ��Ʈ�� ���� ������Ʈ�� �����ɰ���
    private void Start() //�� ������ �ʰ� ����
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();
        fixedObjectBox = transform.GetChild(0);
        obstacleBox = fixedObjectBox.transform.GetChild(0);
        hideObjectBox = transform.GetChild(1);

        forestNode = new HexMember[hexMapCreator.forestNodeCount];
        plainsNode = new HexMember[hexMapCreator.plainsNodeCount];

        int forestIndexNum = 0;
        int plainsIndexNum = 0;


        for (int i = 0; i < hexMapCreator.hexMembers.Length; i++)
        {
            switch (hexMapCreator.hexMembers[i].mapType)
            {
                case 1:
                    forestNode[forestIndexNum] = hexMapCreator.hexMembers[i];
                    forestIndexNum++;
                    break;
                case 2:
                    plainsNode[plainsIndexNum] = hexMapCreator.hexMembers[i];
                    plainsIndexNum++;
                    break;
            }
        }

        morningFMonsterBox[0] = morningMonster01;
        morningFMonsterBox[1] = morningMonster02;
        morningFMonsterBox[2] = morningMonster03;
        morningPMonsterBox[0] = morningMonster04;
        morningPMonsterBox[1] = morningMonster05;
        nightMonsterBox[0] = nightMonster01;
        nightMonsterBox[1] = nightMonster02;
        nightMonsterBox[2] = nightMonster03;
        nightMonsterBox[3] = nightMonster04;
        nightMonsterBox[4] = nightMonster05;
        nightMonsterBox[5] = nightMonster06;
        nightMonsterBox[6] = nightMonster07;


        //������ �� ������ ��� ������ �޾ƿ�����, ���� ������Ʈ�� ��������
        CreateMapObject();
    }


    //���� ������Ʈ (����Ʈ ������Ʈ��, ��ȣ�ǽ��� ������ ��, Ȳ������� �ܵ�� ����)
    private void CreateMapObject()
    {

        //��ȣ�� �� Forest
        ForestMapObject();

        if (isReSet)
        {
            return;
        }

        //Ȳ����� Plains
        PlainsMapObject();

        if (isReSet)
        {
            return;
        }

        //�� ������ �Ϸ�Ǿ����� �÷��̾���ʸ� ����������
        StartCoroutine(PlayerSpawner_co());


        //���� ����
        CreateSanctum();


        //���� ������Ʈ ����
        RandomObject();


        //��ֹ� ����
        CreateObstacle();
    }


    IEnumerator PlayerSpawner_co()
    {
        yield return null;
        if(FindObjectOfType<PlayerSpawner>() == null)
        {
            Instantiate(playerSpawner);
        }
        yield break;
    }



    int random = 0;
    bool checkPlace = true; //false�� �Ǹ� ��� �ٽ� ã��

    private void ForestMapObject()
    {
        forestObj[0] = forest01;
        forestObj[1] = forest02;
        forestObj[2] = forest03;
        forestObj[3] = forest04;
        forestObj[4] = forest05;

        for (int i = 0; i < forestObj.Length; i++)
        {
            int loopNum = 0;
            while (true)
            {
                random = Random.Range(0, forestNode.Length);

                if (!objectIndex.Contains(forestNode[random].index) && !closeList.Contains(random))
                {

                    //������Ʈ �ֺ� ���� Ȯ��
                    for (int o = 0; o < 6; o++)
                    {
                        for (int p = 0; p < 6; p++)
                        {
                            if (forestNode[random].neighbors[o].neighbors[p].mapType != 1 ||
                                forestNode[random].neighbors[o].neighbors[p].doNotUse)
                            {
                                checkPlace = false;
                                closeList.Add(forestNode[random].index);
                                break;
                            }
                        }
                    }

                    if (loopNum++ > 5000)
                    {
                        //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                        isReSet = true;
                        hexMapCreator.ResetMap();
                        return;
                        throw new System.Exception("�� ���� ��");
                    }

                    if (!checkPlace)
                    {
                        checkPlace = true;
                        continue;
                    }


                    //��ġ�� ������Ʈ ������ �ٸ� ������Ʈ�� �پ ��ġ���� �ʵ���
                    for (int o = 0; o < 6; o++)
                    {
                        for (int p = 0; p < 6; p++)
                        {
                            if (!forestNode[random].neighbors[o].neighbors[p].doNotUse)
                            {
                                forestNode[random].neighbors[o].neighbors[p].doNotUse = true;
                            }
                        }
                    }

                    //������Ʈ�� �̵��Ұ����� ����
                    switch (i)
                    {
                        case 0: //����ư 1
                            //forestNode[random].eventType = 1;
                            //forestNode[random].neighbors[5].eventType = 1;
                            //forestNode[random].neighbors[5].neighbors[0].eventType = 1;
                            //forestNode[random].neighbors[5].neighbors[1].eventType = 1;
                            forestNode[random].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].eventtype = "����ư";
                            forestNode[random].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].eventtype = "����ư";
                            forestNode[random].neighbors[5].neighbors[0].ispass = false;
                            forestNode[random].neighbors[5].neighbors[0].eventtype = "����ư";
                            forestNode[random].neighbors[5].neighbors[1].ispass = false;
                            forestNode[random].neighbors[5].neighbors[1].eventtype = "����ư";
                            break;
                        case 1: //��彺��ũ 2
                            //forestNode[random].eventType = 2;
                            //forestNode[random].neighbors[0].eventType = 2;
                            //forestNode[random].neighbors[5].eventType = 2;
                            //forestNode[random].neighbors[5].neighbors[0].eventType = 2;
                            //forestNode[random].neighbors[5].neighbors[5].eventType = 2;
                            //forestNode[random].neighbors[0].neighbors[0].eventType = 2;
                            forestNode[random].neighbors[0].ispass = false;
                            forestNode[random].neighbors[0].eventtype = "��彺��ũ";
                            forestNode[random].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].eventtype = "��彺��ũ";
                            forestNode[random].neighbors[5].neighbors[0].ispass = false;
                            forestNode[random].neighbors[5].neighbors[0].eventtype = "��彺��ũ";
                            forestNode[random].neighbors[5].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].neighbors[5].eventtype = "��彺��ũ";
                            forestNode[random].neighbors[0].neighbors[0].ispass = false;
                            forestNode[random].neighbors[0].neighbors[0].eventtype = "��彺��ũ";
                            break;

                        case 2: //�ŵ� �ǽĵ���
                            GameObject temp3 = Instantiate(forest003);
                            temp3.transform.position = forestNode[random].transform.position + new Vector3(0, 0.2f, 0);
                            temp3.transform.SetParent(gameObject.transform);
                            break;
                    }


                    objectIndex.Add(forestNode[random].index);
                    GameObject temp = Instantiate(forestObj[i]);
                    forestObj[i] = temp;
                    temp.SetActive(true);
                    temp.transform.position = forestNode[random].transform.position + new Vector3(0, 0.2f, 0);
                    temp.transform.SetParent(fixedObjectBox);

                    break;
                }
            }
            closeList.Clear();
        }

        forestObj[2].transform.GetChild(0).gameObject.SetActive(false);
        forestObj[3].transform.GetChild(0).gameObject.SetActive(false);
        forestObj[3].transform.GetChild(1).gameObject.SetActive(false);
        forestObj[3].transform.GetChild(2).gameObject.SetActive(false);
        forestObj[4].transform.GetChild(0).gameObject.SetActive(false);
        forestObj[4].transform.GetChild(1).gameObject.SetActive(false);


    }//��ȣ�ǽ�:����������Ʈ

    private void PlainsMapObject()
    {
        plainsObj[0] = plains01;
        plainsObj[1] = plains02;
        plainsObj[2] = plains03;
        plainsObj[3] = plains04;


        for (int i = 0; i < plainsObj.Length; i++)
        {
            int loopNum = 0;
            while (true)
            {
                random = Random.Range(0, plainsNode.Length);

                if (!objectIndex.Contains(plainsNode[random].index) && !closeList.Contains(random))
                {

                    //������Ʈ �ֺ� ���� Ȯ��
                    for (int o = 0; o < 6; o++)
                    {
                        for (int p = 0; p < 6; p++)
                        {
                            if (plainsNode[random].neighbors[o].neighbors[p].mapType != 2 ||
                                plainsNode[random].neighbors[o].neighbors[p].doNotUse)
                            {
                                checkPlace = false;
                                closeList.Add(plainsNode[random].index);
                                break;
                            }
                        }
                    }

                    if (loopNum++ > 5000)
                    {
                        //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); //-------------------------------------------------------------------
                        isReSet = true;
                        hexMapCreator.ResetMap();
                        return;
                        throw new System.Exception("�� ���� ���");
                    }


                    if (!checkPlace)
                    {
                        checkPlace = true;
                        continue;
                    }


                    //��ġ�� ������Ʈ ������ �ٸ� ������Ʈ�� �پ ��ġ���� �ʵ���
                    for (int o = 0; o < 6; o++)
                    {
                        for (int p = 0; p < 6; p++)
                        {
                            if (!plainsNode[random].neighbors[o].neighbors[p].doNotUse)
                            {
                                plainsNode[random].neighbors[o].neighbors[p].doNotUse = true;
                            }
                        }
                    }


                    //������Ʈ�� �̵��Ұ����� ����
                    switch (i)
                    {
                        case 0: //�и��� 6
                            //plainsNode[random].eventType = 6;
                            //plainsNode[random].neighbors[5].eventType = 6;
                            //plainsNode[random].neighbors[0].eventType = 6;
                            plainsNode[random].neighbors[5].eventtype = "�и���";
                            plainsNode[random].neighbors[5].ispass = false;
                            plainsNode[random].neighbors[0].eventtype = "�и���";
                            plainsNode[random].neighbors[0].ispass = false;
                            break;
                        case 1: //����������� 7
                            //plainsNode[random].eventType = 7;
                            break;

                        case 2: //ī������ �ð� 8
                            //plainsNode[random].eventType = 8;
                            //plainsNode[random].neighbors[0].eventType = 8;
                            plainsNode[random].neighbors[0].eventtype = "ī�����ǽð�";
                            plainsNode[random].neighbors[0].ispass = false;
                            break;
                        case 3:
                            //plainsNode[random].eventType = 9;
                            break;

                    }


                    objectIndex.Add(plainsNode[random].index);
                    GameObject temp = Instantiate(plainsObj[i]);
                    plainsObj[i] = temp;
                    temp.SetActive(true);
                    temp.transform.position = plainsNode[random].transform.position + new Vector3(0, 0.2f, 0);
                    temp.transform.SetParent(fixedObjectBox);

                    break;
                }
            }
            closeList.Clear();
        }

        plainsObj[3].GetComponent<MeshRenderer>().enabled = false;
    }//Ȳ�����:����������Ʈ


    private void CreateSanctum()
    {
        sanctumObj[0] = sanctumFocus;
        sanctumObj[1] = sanctumLife;
        sanctumObj[2] = sanctumWisdow;


        for (int i = 0; i < 3; i++)
        {

            if (i.Equals(0))
            {
                //��ȣ�� ���� ����
                while (true)
                {
                    bool check = false;
                    random = Random.Range(0, forestNode.Length);
                    if (!forestNode[random].doNotUse)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (forestNode[random].neighbors[j].doNotUse)
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            continue;
                        }

                        GameObject sanctum = Instantiate(sanctumObj[i]);
                        sanctum.transform.position = forestNode[random].transform.position + new Vector3(0f, 0.1f, 0f);
                        sanctum.transform.SetParent(fixedObjectBox);
                        sanctumObj[i] = sanctum;
                        forestNode[random].doNotUse = true;
                        objectIndex.Add(forestNode[random].index);
                        break;
                    }
                }
            }
            else
            {
                //Ȳ������� ����
                while (true)
                {
                    bool check = false;
                    random = Random.Range(0, plainsNode.Length);
                    if (!plainsNode[random].doNotUse)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (plainsNode[random].neighbors[j].doNotUse)
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            continue;
                        }

                        GameObject sanctum = Instantiate(sanctumObj[i]);
                        sanctum.transform.position = plainsNode[random].transform.position + new Vector3(0f, 0.1f, 0f);
                        sanctum.transform.SetParent(fixedObjectBox);
                        sanctumObj[i] = sanctum;
                        plainsNode[random].doNotUse = true;
                        objectIndex.Add(plainsNode[random].index);
                        break;
                    }
                }

            }
        }
    }//����:����


    private void RandomObject()
    {
        randomObj[0] = monster01;
        randomObj[1] = monster02;
        randomObj[2] = monster03;
        randomObj[3] = monster04;
        randomObj[4] = exclamation01;
        randomObj[5] = exclamation02;


        for (int i = 0; i < 6; i++)
        {
            int randomNumber = Random.Range(0, 2);
            if (randomNumber.Equals(0))
            {
                //��ȣ�� ���� ����
                while (true)
                {
                    bool check = false;
                    random = Random.Range(0, forestNode.Length);
                    if (!forestNode[random].doNotUse)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (forestNode[random].neighbors[j].doNotUse)
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            continue;
                        }

                        GameObject randomobj = Instantiate(randomObj[i]);
                        randomobj.transform.position = forestNode[random].transform.position + new Vector3(0f, 0.1f, 0f); ;
                        randomobj.transform.SetParent(hideObjectBox);
                        forestNode[random].doNotUse = true;
                        randomObjectIndex.Add(forestNode[random].index);
                        activerandomObject.Add(randomobj);
                        randomObj[i].SetActive(false);
                        break;
                    }
                }
            }
            else
            {
                //Ȳ������� ����
                while (true)
                {
                    bool check = false;
                    random = Random.Range(0, plainsNode.Length);
                    if (!plainsNode[random].doNotUse)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (plainsNode[random].neighbors[j].doNotUse)
                            {
                                check = true;
                            }
                        }

                        if (check)
                        {
                            continue;
                        }

                        GameObject randomobj = Instantiate(randomObj[i]);
                        randomobj.transform.position = plainsNode[random].transform.position + new Vector3(0f, 0.1f, 0f); ;
                        randomobj.transform.SetParent(hideObjectBox);
                        plainsNode[random].doNotUse = true;
                        randomObjectIndex.Add(plainsNode[random].index);
                        activerandomObject.Add(randomobj);
                        randomObj[i].SetActive(false);
                        break;
                    }
                }

            }
        }



    }//����:����������Ʈ(��Ȱ��ȭ)


    private void CreateObstacle()
    {
        //�����ϰ� �迭�� ����
        //���� ��忡 ��ֹ��� �����ϰ�
        //ispass�� false�� ����
        ObstacleObj[0] = tree;
        ObstacleObj[1] = stoneForest01;
        ObstacleObj[2] = stoneForest02;
        ObstacleObj[3] = stoneForest03;
        ObstacleObj[4] = flower;  //0~4
        ObstacleObj[5] = grass;  //5~8
        ObstacleObj[6] = stonePlains01;
        ObstacleObj[7] = stonePlains02;
        ObstacleObj[8] = stonePlains03;


        for (int i = 0; i < forestNode.Length; i++)
        {
            if (!forestNode[i].doNotUse)
            {
                int randomCreate = Random.Range(0, 5);
                if (randomCreate.Equals(0))
                {
                    int objNum = Random.Range(0, 4);
                    GameObject obstacle = Instantiate(ObstacleObj[objNum]);
                    obstacle.transform.position = forestNode[i].transform.position + new Vector3(0f, 0.1f, 0f);
                    obstacle.transform.SetParent(obstacleBox);
                    forestNode[i].doNotUse = true;
                    forestNode[i].ispass = false;
                }
                else if (randomCreate.Equals(2))
                {
                    //��
                    GameObject obstacle = Instantiate(ObstacleObj[4]);
                    obstacle.transform.localScale = new Vector3(3f, 3f, 3f);
                    obstacle.transform.position = forestNode[i].transform.position + new Vector3(0f, -0.5f, 0f);
                    obstacle.transform.SetParent(obstacleBox);
                    forestNode[i].doNotUse = true;
                    forestNode[i].eventtype = obstacle.ToString();
                }

            }
        }

        for (int i = 0; i < plainsNode.Length; i++)
        {
            if (!plainsNode[i].doNotUse)
            {
                int randomCreate = Random.Range(0, 5);
                if (randomCreate.Equals(0))
                {
                    int objNum = Random.Range(6, 9);
                    GameObject obstacle = Instantiate(ObstacleObj[objNum]);
                    obstacle.transform.position = plainsNode[i].transform.position + new Vector3(0f, 0.1f, 0f);
                    plainsNode[i].ispass = false;
                    obstacle.transform.SetParent(obstacleBox);
                    plainsNode[i].doNotUse = true;
                }
                else if (randomCreate.Equals(2))
                {
                    //�ܵ�
                    GameObject obstacle = Instantiate(ObstacleObj[5]);
                    obstacle.transform.position = plainsNode[i].transform.position + new Vector3(0f, 0.6f, 0f);
                    obstacle.transform.SetParent(obstacleBox);
                    plainsNode[i].doNotUse = true;
                    plainsNode[i].eventtype = obstacle.ToString();
                }
            }
        }
    }//����:��ֹ�



    public void timeMonsterSpawn(bool isMorning)
    {
        switch (isMorning)
        {
            case true:
                //�㿡 ��ȯ�� ���Ͱ� �ִٸ� ������
                while (activeMonster.Count > 0)
                {
                    Destroy(activeMonster[0]);
                    activeMonster.RemoveAt(0);
                }
                randomMonsterName.Clear();
                randomMonsterIndex.Clear();


                //���� ��ȯ�� ���� �� �������� n���� ��������

                for (int i = 0; i < 4; i++) //��
                {
                    int randomNum = Random.Range(0, 4);
                    if (randomNum != 3)
                    {
                        while (true)
                        {
                            int randomNumber = Random.Range(0, forestNode.Length);
                            if (!forestNode[randomNumber].doNotUse)
                            {
                                int randomMonsterNum = Random.Range(0, 3);
                                GameObject moster = Instantiate(morningFMonsterBox[randomMonsterNum]);
                                moster.transform.position = forestNode[randomNumber].transform.position + new Vector3(0f, 0.1f, 0f);
                                moster.SetActive(true);
                                forestNode[randomNumber].doNotUse = true;

                                //������ ���� ������ �������
                                randomMonsterName.Add(randomMonsterNum);
                                randomMonsterIndex.Add(forestNode[randomNumber].index);
                                activeMonster.Add(moster);
                                break;
                            }
                        }

                    }
                }

                for (int i = 0; i < 4; i++) //���
                {
                    int randomNum = Random.Range(0, 4);
                    if (randomNum != 3)
                    {
                        while (true)
                        {
                            int randomNumber = Random.Range(0, plainsNode.Length);
                            if (!plainsNode[randomNumber].doNotUse)
                            {
                                int randomMonsterNum = Random.Range(0, 2);
                                GameObject moster = Instantiate(morningPMonsterBox[randomMonsterNum]);
                                moster.transform.position = plainsNode[randomNumber].transform.position + new Vector3(0f, 0.1f, 0f);
                                moster.SetActive(true);
                                plainsNode[randomNumber].doNotUse = true;

                                //������ ���� ������ �������
                                randomMonsterName.Add(randomMonsterNum + 3);
                                randomMonsterIndex.Add(plainsNode[randomNumber].index);
                                activeMonster.Add(moster);
                                break;
                            }
                        }

                    }
                }

                break;

            case false:
                //���� ��ȯ�� ���Ͱ� �ִٸ� ������
                while (activeMonster.Count > 0)
                {
                    Destroy(activeMonster[0]);
                    activeMonster.RemoveAt(0);
                }
                randomMonsterName.Clear();
                randomMonsterIndex.Clear();

                //�㿡 ��ȯ�� ���� �� �������� n���� ��������

                for (int i = 0; i < 7; i++) //����
                {
                    int randomNum = Random.Range(0, 5);
                    if (randomNum != 3)
                    {
                        int area = Random.Range(0, 2);
                        switch (area)
                        {
                            case 0://��
                                while (true)
                                {
                                    int randomNumber = Random.Range(0, forestNode.Length);
                                    if (!forestNode[randomNumber].doNotUse)
                                    {
                                        int randomMonsterNum = Random.Range(0, 7);
                                        GameObject moster = Instantiate(nightMonsterBox[randomMonsterNum]);
                                        moster.transform.position = forestNode[randomNumber].transform.position + new Vector3(0f, 0.1f, 0f);
                                        moster.SetActive(true);
                                        forestNode[randomNumber].doNotUse = true;

                                        randomMonsterName.Add(randomMonsterNum + 5);
                                        randomMonsterIndex.Add(forestNode[randomNumber].index);
                                        activeMonster.Add(moster);
                                        break;
                                    }
                                }
                                break;

                            case 1://���
                                while (true)
                                {
                                    int randomNumber = Random.Range(0, plainsNode.Length);
                                    if (!plainsNode[randomNumber].doNotUse)
                                    {
                                        int randomMonsterNum = Random.Range(0, 7);
                                        GameObject moster = Instantiate(nightMonsterBox[randomMonsterNum]);
                                        moster.transform.position = plainsNode[randomNumber].transform.position + new Vector3(0f, 0.1f, 0f);
                                        moster.SetActive(true);
                                        plainsNode[randomNumber].doNotUse = true;

                                        randomMonsterName.Add(randomMonsterNum + 5);
                                        randomMonsterIndex.Add(plainsNode[randomNumber].index);
                                        activeMonster.Add(moster);
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                }

                break;
        }
    }

    //����Ʈ ������Ʈ ���̰� �ϱ�
    public void ShowObject(int objNum)
    {
        StartCoroutine(ShowObjectCo(objNum));
    }
    IEnumerator ShowObjectCo(int objNum)
    {
        yield return new WaitForSeconds(0.2f);
        switch (objNum)
        {
            case 0:
                forestObj[2].transform.GetChild(0).localScale = Vector3.zero;
                forestObj[2].transform.GetChild(0).gameObject.SetActive(true);
                for (int i = 0; i < 20; i++)
                {
                    forestObj[2].transform.GetChild(0).localScale += new Vector3(1f, 1f, 1f);
                    yield return new WaitForSeconds(0.02f);
                }
                forestObj[2].transform.GetChild(0).localScale = new Vector3(20f, 20f, 20f);
                yield break;
            case 1:
                forestObj[3].transform.GetChild(0).localScale = Vector3.zero;
                forestObj[3].transform.GetChild(1).localScale = Vector3.zero;
                forestObj[3].transform.GetChild(2).localScale = Vector3.zero;
                forestObj[3].transform.GetChild(0).gameObject.SetActive(true);
                forestObj[3].transform.GetChild(1).gameObject.SetActive(true);
                forestObj[3].transform.GetChild(2).gameObject.SetActive(true);

                for (int i = 0; i < 20; i++)
                {
                    forestObj[3].transform.GetChild(1).localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    forestObj[3].transform.GetChild(2).localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    yield return new WaitForSeconds(0.02f);
                }
                forestObj[3].transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
                forestObj[3].transform.GetChild(1).localScale = new Vector3(1f, 1f, 1f);
                forestObj[3].transform.GetChild(2).localScale = new Vector3(1f, 1f, 1f);

                yield break;
            case 2:
                forestObj[4].transform.GetChild(0).localScale = Vector3.zero;
                forestObj[4].transform.GetChild(1).localScale = Vector3.zero;
                forestObj[4].transform.GetChild(0).gameObject.SetActive(true);
                forestObj[4].transform.GetChild(1).gameObject.SetActive(true);

                for (int i = 0; i < 20; i++)
                {
                    forestObj[4].transform.GetChild(0).localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    forestObj[4].transform.GetChild(1).localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    yield return new WaitForSeconds(0.02f);
                }
                forestObj[4].transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
                forestObj[4].transform.GetChild(1).localScale = new Vector3(1f, 1f, 1f);
                yield break;
            case 3:
                plainsObj[3].transform.localScale = Vector3.zero;
                plainsObj[3].GetComponent<MeshRenderer>().enabled = true;
                for (int i = 0; i < 20; i++)
                {
                    plainsObj[3].transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    yield return new WaitForSeconds(0.02f);
                }
                plainsObj[3].transform.localScale = new Vector3(1f, 1f, 1f);
                yield break;
        }
    }





    //������ ������Ʈ ���̰�
    public void ShowRandomObject(int objNum)
    {
        StartCoroutine(ShowRandomObjectCo(objNum));
    }
    IEnumerator ShowRandomObjectCo(int objNum)
    {
        switch (objNum)
        {
            case 0: //monster01
                if (activerandomObject[0].activeSelf)
                {
                    yield break;
                }
                activerandomObject[0].SetActive(true);
                activerandomObject[0].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[0].transform.localScale += new Vector3(3.5f, 3.5f, 3.5f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[0].transform.localScale = new Vector3(70f, 70f, 70f);
                yield break;

            case 1: //monster02
                if (activerandomObject[1].activeSelf)
                {
                    yield break;
                }
                activerandomObject[1].SetActive(true);
                activerandomObject[1].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[1].transform.localScale += new Vector3(4f, 4f, 4f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[1].transform.localScale = new Vector3(80f, 80f, 80f);
                yield break;

            case 2: //monster03 
                if (activerandomObject[2].activeSelf)
                {
                    yield break;
                }
                activerandomObject[2].SetActive(true);
                activerandomObject[2].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[2].transform.localScale += new Vector3(3.25f, 3.25f, 3.25f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[2].transform.localScale = new Vector3(65f, 65f, 65f);
                yield break;

            case 3: //monster04 
                if (activerandomObject[3].activeSelf)
                {
                    yield break;
                }
                activerandomObject[3].SetActive(true);
                activerandomObject[3].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[3].transform.localScale += new Vector3(3f, 3f, 3f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[3].transform.localScale = new Vector3(60f, 60f, 60f);
                yield break;

            case 4: //����ǥ 
                if (activerandomObject[4].activeSelf)
                {
                    yield break;
                }
                activerandomObject[4].SetActive(true);
                activerandomObject[4].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[4].transform.localScale += new Vector3(5.5f, 5.5f, 5.5f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[4].transform.localScale = new Vector3(110f, 110f, 110f);
                yield break;

            case 5: //����ǥ
                if (activerandomObject[5].activeSelf)
                {
                    yield break;
                }
                activerandomObject[5].SetActive(true);
                activerandomObject[5].transform.localScale = Vector3.zero;
                for (int i = 0; i < 20; i++)
                {
                    activerandomObject[5].transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                    yield return new WaitForSeconds(0.02f);
                }
                activerandomObject[5].transform.localScale = new Vector3(1f, 1f, 1f);
                yield break;
        }
    }


    //[Header("���� �̸��� Index")]
    //public List<int> randomMonsterName = new List<int>();
    //public List<int> randomMonsterIndex = new List<int>();
    ////[�̸�] randomMonsterName�� ���ڰ� ���ϴ� ���͸�
    ////��������=0, �����渶����, ����, �Ŵ����, ȩ���, �ذ񺴻�=5, ��, �ٴ��ǳ���, �����, �ذ�ȯ����, ����=10


    ////������ ���� GameObject�� �����ϱ����� ����Ʈ
    //private List<GameObject> activeMonster = new List<GameObject>(); //randomMonsterName�� randomMonsterIndex�� ���� ��ġ��


    public List<GameObject> CheckAround(int centerIndex)
    {
        List<GameObject> box = new List<GameObject>();
        List<int> closeIndex = new List<int>();

        for (int e = 0; e < randomMonsterIndex.Count; e++)
        {
            bool check = false;
            //�ֺ��� � ���Ͱ� �ֳ���
            for (int i = 0; i < 6; i++)
            {
                if (check)
                {
                    break;
                }
                for (int j = 0; j < 6; j++)
                {
                    if (randomMonsterIndex[e] == hexMapCreator.hexMembers[centerIndex].neighbors[i].neighbors[j].index && !closeIndex.Contains(randomMonsterIndex[e]))
                    {
                        closeIndex.Add(randomObjectIndex[e]);
                        box.Add(activeMonster[e]);
                        check = true;
                        break;
                    }
                }
            }
        }


        return box;
    }


    //objType 0=�ŵ��ǽĵ���, 1=���߼���, 2=������, 3=��������
    public void UseObject(int objType)
    {
        switch (objType)
        {
            case 0://���� �ǽĵ��� ��ü
                GameObject temp00 = Instantiate(forest03Use);
                temp00.transform.position = forestObj[2].transform.position;
                Destroy(forestObj[2]);
                break;

            case 1:
                GameObject temp01 = Instantiate(sanctumUse);
                temp01.transform.position = sanctumObj[0].transform.position;
                Destroy(sanctumObj[0]);
                break;
            case 2:
                GameObject temp02 = Instantiate(sanctumUse);
                temp02.transform.position = sanctumObj[1].transform.position;
                Destroy(sanctumObj[1]);
                break;
            case 3:
                GameObject temp03 = Instantiate(sanctumUse);
                temp03.transform.position = sanctumObj[2].transform.position;
                Destroy(sanctumObj[2]);
                break;
        }
    }

}
