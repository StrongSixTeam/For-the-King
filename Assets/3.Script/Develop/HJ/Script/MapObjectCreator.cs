using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapObjectCreator : MonoBehaviour
{
    HexMapCreator hexMapCreator;
    [SerializeField] GameObject playerSpawner;

    //노드 정보
    public HexMember[] forestNode;
    public HexMember[] plainsNode;

    [Header("Index6 : 오아튼, 우드스모크, 눈부신광산, 패리드, 잊혀진저장고, 카젤리의시계")]
    public List<int> objectIndex = new List<int>();


    [Header("필수(퀘스트) 오브젝트 8개")]
    private GameObject[] forestObj = new GameObject[3];
    //[SerializeField] GameObject[] fixedObject;
    [SerializeField] GameObject forest01; //오아튼(시작지점) (townForest1)
    [SerializeField] GameObject forest02; //우드스모크 (townForest2)
    //[SerializeField] GameObject forest03; //신의 의식도구
    //[SerializeField] GameObject forest04; //카오스 우두머리 - 소영언니가 프리팹 만들어줌
    [SerializeField] GameObject forest05; //눈부신 광산 (dungeon05)

    private GameObject[] plainsObj = new GameObject[3];
    [SerializeField] GameObject plains01; //패리드 (townPlains1)
    [SerializeField] GameObject plains02; //잊혀진 저장고 (dungeon07)
    [SerializeField] GameObject plains03; //카젤리의 시계 (townPlains2)


    [Header("수호의 숲")]
    //[SerializeField] GameObject tree;
    //[SerializeField] GameObject stoneForest;

    [Header("황금평원")]
    [SerializeField] GameObject grass; //(hexPlains01_grass)
    //[SerializeField] GameObject stonePlains; 



    List<int> closeList = new List<int>();


    //HexMapCreator가 맵 생성을 완료된 후에, 이 스크립트를 담은 오브젝트가 생성될거임
    private void Start() //한 프레임 늦게 실행
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

        //위에서 각 지역별 노드 정보를 받아왔으니, 이제 오브젝트를 세워볼까
        CreateMapObject();
    }


    //고정 오브젝트 (퀘스트 오브젝트와, 수호의숲의 나무와 돌, 황금평원의 잔디와 바위)
    private void CreateMapObject()
    {

        //수호의 숲 Forest
        ForestMapObject();


        //황금평원 Plains
        PlainsMapObject();



        //맵 생성이 완료되었으니 플레이어스포너를 생성해주자
        StartCoroutine(PlayerSpawner_co());
        
    }


    IEnumerator PlayerSpawner_co()
    {
        yield return null;
        Instantiate(playerSpawner);
        yield break;
    }




    int random = 0;
    bool checkPlace = true; //false가 되면 장소 다시 찾음

    private void ForestMapObject()
    {
        forestObj[0] = forest01;
        forestObj[1] = forest02;
        forestObj[2] = forest05;

        for (int i = 0; i < forestObj.Length; i++)
        {
            int loopNum = 0;
            while (true)
            {
                random = Random.Range(0, forestNode.Length);

                if (!objectIndex.Contains(forestNode[random].index) && !closeList.Contains(random))
                {

                    //오브젝트 주변 공간 확보
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

                    if (loopNum++ > 3000)
                    {
                        throw new System.Exception("아 설마 숲");
                    }

                    if (!checkPlace)
                    {
                        checkPlace = true;
                        continue;
                    }


                    //설치된 오브젝트 주위에 다른 오브젝트가 붙어서 설치되지 않도록
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


        for (int i = 0; i < plainsObj.Length; i++)
        {
            int loopNum = 0;
            while (true)
            {
                random = Random.Range(0, plainsNode.Length);

                if (!objectIndex.Contains(plainsNode[random].index) && !closeList.Contains(random))
                {

                    //오브젝트 주변 공간 확보
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

                    if (loopNum++ > 3000)
                    {
                        throw new System.Exception("아 설마 평원");
                    }


                    if (!checkPlace)
                    {
                        checkPlace = true;
                        continue;
                    }


                    //설치된 오브젝트 주위에 다른 오브젝트가 붙어서 설치되지 않도록
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


                    objectIndex.Add(plainsNode[random].index);
                    GameObject tamp = Instantiate(plainsObj[i]);
                    tamp.transform.position = plainsNode[random].transform.position + new Vector3(0, 0.2f, 0);

                    break;
                }
            }
            closeList.Clear();
        }
    }


    //플레이어가 이동할때마다 랜덤으로 생성
    private void RandomObject()
    {
        //어떠한 확률로 이 메소드는 오브젝트를 생성한다
        //objectPool에 담아둔 오브젝트를 랜덤으로 꺼낸다

        //오브젝트 풀링으로 관리

    }



}
