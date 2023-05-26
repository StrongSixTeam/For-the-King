using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapObjectCreator : MonoBehaviour
{
    HexMapCreator hexMapCreator;
    [SerializeField] GameObject playerSpawner;

    //��� ����
    public HexMember[] forestNode;
    public HexMember[] plainsNode;

    [Header("Index9 : ����ư, ��彺��ũ, �����ǽĵ���, ī������θӸ�, ���νű���, �и���, �����������, ī�����ǽð�, ��ü�����Ͻ�")]
    public List<int> objectIndex = new List<int>();

    //������ �ε�����ȣ�� �߰��� (����, ����, ����

    [Header("�ʼ�(����Ʈ) ������Ʈ 8��")]
    private GameObject[] forestObj = new GameObject[5];
    [SerializeField] GameObject forest01; //����ư(��������) (townForest1)
    [SerializeField] GameObject forest02; //��彺��ũ (townForest2)
    [SerializeField] GameObject forest03; //���� �ǽĵ��� (sealMovingParts)
    [SerializeField] GameObject forest04; //ī���� ��θӸ� (enCultistA3)
    [SerializeField] GameObject forest05; //���ν� ���� (dungeon05)

    private GameObject[] plainsObj = new GameObject[4];
    [SerializeField] GameObject plains01; //�и��� (townPlains1)
    [SerializeField] GameObject plains02; //������ ����� (dungeon07)
    [SerializeField] GameObject plains03; //ī������ �ð� (townPlains2)
    [SerializeField] GameObject plains04; //��ü�� ���Ͻ� (licheCrypt_0)

    [Header("���� : ����, ����, ����")]
    [SerializeField] GameObject[] sanctumObj = new GameObject[3];


    [Header("��ȣ�� ��")]
    [SerializeField] GameObject tree; //(Tree_Fir)
    [SerializeField] GameObject[] stoneForest = new GameObject[3]; //(Stone_(n)F)

    [Header("Ȳ�����")]
    [SerializeField] GameObject grass; //(hexPlains01_grass)
    [SerializeField] GameObject[] stonePlains = new GameObject[3]; //(Stone_(n)P)


    [Header("���� ���� 4")]
    [SerializeField] GameObject[] monstarObj = new GameObject[4];

    [Header("���� ������Ʈ 2")]
    [SerializeField] GameObject[] randomObj = new GameObject[2];

    List<int> closeList = new List<int>();


    //HexMapCreator�� �� ������ �Ϸ�� �Ŀ�, �� ��ũ��Ʈ�� ���� ������Ʈ�� �����ɰ���
    private void Start() //�� ������ �ʰ� ����
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();

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

        //������ �� ������ ��� ������ �޾ƿ�����, ���� ������Ʈ�� ��������
        CreateMapObject();
    }


    //���� ������Ʈ (����Ʈ ������Ʈ��, ��ȣ�ǽ��� ������ ��, Ȳ������� �ܵ�� ����)
    private void CreateMapObject()
    {

        //��ȣ�� �� Forest
        ForestMapObject();


        //Ȳ����� Plains
        PlainsMapObject();



        //�� ������ �Ϸ�Ǿ����� �÷��̾���ʸ� ����������
        StartCoroutine(PlayerSpawner_co());

    }


    IEnumerator PlayerSpawner_co()
    {
        yield return null;
        Instantiate(playerSpawner);
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
                            forestNode[random].eventType = 1;
                            forestNode[random].neighbors[5].eventType = 1;
                            forestNode[random].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].neighbors[0].eventType = 1;
                            forestNode[random].neighbors[5].neighbors[0].ispass = false;
                            forestNode[random].neighbors[5].neighbors[1].eventType = 1;
                            forestNode[random].neighbors[5].neighbors[1].ispass = false;
                            break;
                        case 1: //��彺��ũ 2
                            forestNode[random].eventType = 2;
                            forestNode[random].neighbors[0].eventType = 2;
                            forestNode[random].neighbors[0].ispass = false;
                            forestNode[random].neighbors[5].eventType = 2;
                            forestNode[random].neighbors[5].ispass = false;
                            forestNode[random].neighbors[5].neighbors[0].eventType = 2;
                            forestNode[random].neighbors[5].neighbors[0].ispass = false;
                            forestNode[random].neighbors[5].neighbors[5].eventType = 2;
                            forestNode[random].neighbors[5].neighbors[5].ispass = false;
                            forestNode[random].neighbors[0].neighbors[0].eventType = 2;
                            forestNode[random].neighbors[0].neighbors[0].ispass = false;
                            break;

                        case 3: //���ν� ���� 5
                            forestNode[random].eventType = 5;
                            break;
                    }


                    objectIndex.Add(forestNode[random].index);
                    GameObject temp = Instantiate(forestObj[i]);
                    temp.transform.position = forestNode[random].transform.position + new Vector3(0, 0.2f, 0);


                    break;
                }
            }
            closeList.Clear();

        }
    }

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
                            plainsNode[random].eventType = 6;
                            plainsNode[random].neighbors[5].eventType = 6;
                            plainsNode[random].neighbors[5].ispass = false;
                            plainsNode[random].neighbors[0].eventType = 6;
                            plainsNode[random].neighbors[0].ispass = false;
                            break;
                        case 1: //����������� 7
                            plainsNode[random].eventType = 7;
                            break;

                        case 2: //ī������ �ð� 8
                            plainsNode[random].eventType = 8;
                            plainsNode[random].neighbors[0].eventType = 8;
                            plainsNode[random].neighbors[0].ispass = false;
                            break;
                        case 3:
                            plainsNode[random].eventType = 9;
                            break;

                    }


                    objectIndex.Add(plainsNode[random].index);
                    GameObject tamp = Instantiate(plainsObj[i]);
                    tamp.transform.position = plainsNode[random].transform.position + new Vector3(0, 0.2f, 0);

                    break;
                }
            }
            closeList.Clear();
        }
    }

    //���� 3�� ���� 
    private void CreateSanctum()
    {
        for (int i = 0; i < 3; i++)
        {

            random = Random.Range(0, 2);
            if (random == 1)
            {
                while (true)
                {
                    random = Random.Range(0, forestNode.Length);
                    if (!forestNode[random].doNotUse)
                    {
                        GameObject sanctum = Instantiate(sanctumObj[i]);
                        sanctum.transform.position = forestNode[random].transform.position;
                    }
                }
                


            }


        }


    }




    //��ֹ� ����
    private void CreateObstacle()
    {
        //�����ϰ� �迭�� ����
        //���� ��忡 ��ֹ��� �����ϰ�
        //ispass�� false�� ����




    }





    //�÷��̾ �̵��Ҷ����� �������� ����
    private void RandomObject()
    {
        //��� Ȯ���� �� �޼ҵ�� ������Ʈ�� �����Ѵ�
        //objectPool�� ��Ƶ� ������Ʈ�� �������� ������

        //������Ʈ Ǯ������ ����

    }



}
