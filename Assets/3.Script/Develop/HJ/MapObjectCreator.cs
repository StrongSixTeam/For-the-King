using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectCreator : MonoBehaviour
{

    //��� ����� ������ ������
    HexMapCreator hexMapCreator;

    //����� ��ġ����
    Vector3[] hexNode;

    //�ʼ� ������Ʈ 8��
    GameObject[] fixedObject;

    //��ȣ�ǽ�
    GameObject tree; 
    GameObject stoneForest;

    //Ȳ�����
    GameObject grass; //����������
    GameObject stonePlains; 

    GameObject[] objectPool;

    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();

        hexNode = new Vector3[hexMapCreator.mapSize];
        fixedObject = new GameObject[8];
    }

    //���۰� ���ÿ� ������ fixedObject and ��ֹ�
    private void CreateMapObject()
    {
        //��ȣ�� �� Forest
        //���� mapType�� 1�̶��



        //Ȳ����� Plains
        //���� mapType�� 2���


    }

    //�÷��̾ �̵��Ҷ����� �������� ����
    private void RandomObject()
    {
        //��� Ȯ���� �� �޼ҵ�� ����ȴ�
        //objectPool�� ��Ƶ� ������Ʈ�� �������� ������
    }



}
