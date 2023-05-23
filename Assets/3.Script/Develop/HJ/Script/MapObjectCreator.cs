using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapObjectCreator : MonoBehaviour
{
    HexMapCreator hexMapCreator;

    //��� ����
    public HexMember[] forestNode;
    public HexMember[] plainsNode;


    [Header("�ʼ�(����Ʈ) ������Ʈ 8��")]
    //[SerializeField] GameObject[] fixedObject;
    [SerializeField] GameObject forest01; //����ư(��������) (townForest1)
    [SerializeField] GameObject forest02; //��彺��ũ (townForest2)
    [SerializeField] GameObject forest03; //���� �ǽĵ���
    [SerializeField] GameObject forest04; //ī���� ��θӸ� - �ҿ���ϰ� ������ �������
    [SerializeField] GameObject forest05; //���ν� ���� (dungeon05)

    [SerializeField] GameObject plains01; //�и��� (townPlains1)
    [SerializeField] GameObject plains02; //������ ����� (dungeon07)
    [SerializeField] GameObject plains03; //ī������ �ð� (townPlains2)


    [Header("��ȣ�� ��")]
    [SerializeField] GameObject tree;
    [SerializeField] GameObject stoneForest;

    [Header("Ȳ�����")]
    [SerializeField] GameObject grass; //(hexPlains01_grass)
    [SerializeField] GameObject stonePlains; 


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

        Debug.Log("forestIndexNum : " + forestIndexNum);
        Debug.Log("plainsIndexNum : " + plainsIndexNum);


        //������ �� ������ ��� ������ �޾ƿ�����, ���� ������Ʈ�� ��������
        CreateMapObject();
    }

    //���� ������Ʈ (����Ʈ ������Ʈ��, ��ȣ�ǽ��� ������ ��, Ȳ������� �ܵ�� ����)
    private void CreateMapObject()
    {
        Debug.Log("CreateMapObject ����");

        //��ȣ�� �� Forest
        //���� mapType�� 1�̶��

        


        //Ȳ����� Plains
        //���� mapType�� 2���



    }



    //�÷��̾ �̵��Ҷ����� �������� ����
    private void RandomObject()
    {
        //��� Ȯ���� �� �޼ҵ�� ������Ʈ�� �����Ѵ�
        //objectPool�� ��Ƶ� ������Ʈ�� �������� ������

        //������Ʈ Ǯ������ ����

    }



}
