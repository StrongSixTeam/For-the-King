using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapObjectCreator : MonoBehaviour
{
    HexMapCreator hexMapCreator;

    //노드 정보
    public HexMember[] forestNode;
    public HexMember[] plainsNode;


    [Header("필수(퀘스트) 오브젝트 8개")]
    //[SerializeField] GameObject[] fixedObject;
    [SerializeField] GameObject forest01; //오아튼(시작지점) (townForest1)
    [SerializeField] GameObject forest02; //우드스모크 (townForest2)
    [SerializeField] GameObject forest03; //신의 의식도구
    [SerializeField] GameObject forest04; //카오스 우두머리 - 소영언니가 프리팹 만들어줌
    [SerializeField] GameObject forest05; //눈부신 광산 (dungeon05)

    [SerializeField] GameObject plains01; //패리드 (townPlains1)
    [SerializeField] GameObject plains02; //잊혀진 저장고 (dungeon07)
    [SerializeField] GameObject plains03; //카젤리의 시계 (townPlains2)


    [Header("수호의 숲")]
    [SerializeField] GameObject tree;
    [SerializeField] GameObject stoneForest;

    [Header("황금평원")]
    [SerializeField] GameObject grass; //(hexPlains01_grass)
    [SerializeField] GameObject stonePlains; 


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

        Debug.Log("forestIndexNum : " + forestIndexNum);
        Debug.Log("plainsIndexNum : " + plainsIndexNum);


        //위에서 각 지역별 노드 정보를 받아왔으니, 이제 오브젝트를 세워볼까
        CreateMapObject();
    }

    //고정 오브젝트 (퀘스트 오브젝트와, 수호의숲의 나무와 돌, 황금평원의 잔디와 바위)
    private void CreateMapObject()
    {
        Debug.Log("CreateMapObject 실행");

        //수호의 숲 Forest
        //맵의 mapType이 1이라면

        


        //황금평원 Plains
        //맵의 mapType이 2라면



    }



    //플레이어가 이동할때마다 랜덤으로 생성
    private void RandomObject()
    {
        //어떠한 확률로 이 메소드는 오브젝트를 생성한다
        //objectPool에 담아둔 오브젝트를 랜덤으로 꺼낸다

        //오브젝트 풀링으로 관리

    }



}
