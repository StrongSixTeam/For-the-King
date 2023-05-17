using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectCreator : MonoBehaviour
{

    //모든 노드의 정보를 들고오자
    HexMapCreator hexMapCreator;

    //노드의 위치정보
    Vector3[] hexNode;

    //필수 오브젝트 8개
    GameObject[] fixedObject;

    //수호의숲
    GameObject tree; 
    GameObject stoneForest;

    //황금평원
    GameObject grass; //밟을수있음
    GameObject stonePlains; 

    GameObject[] objectPool;

    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();

        hexNode = new Vector3[hexMapCreator.mapSize];
        fixedObject = new GameObject[8];
    }

    //시작과 동시에 생성될 fixedObject and 장애물
    private void CreateMapObject()
    {
        //수호의 숲 Forest
        //맵의 mapType이 1이라면



        //황금평원 Plains
        //맵의 mapType이 2라면


    }

    //플레이어가 이동할때마다 랜덤으로 생성
    private void RandomObject()
    {
        //어떠한 확률로 이 메소드는 실행된다
        //objectPool에 담아둔 오브젝트를 랜덤으로 꺼낸다
    }



}
