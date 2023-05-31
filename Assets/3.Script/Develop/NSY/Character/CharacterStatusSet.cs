using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//��ũ���ͺ� ���� ���� �� ��������� �⺻ �̸��� �޴� ���̹�
[CreateAssetMenu(fileName ="CharacterBaseStatsData", menuName = "ScriptableObjects/CharacterBaseStatsData", order =1)  ]
public class CharacterStatusSet : ScriptableObject
{
    public string className= "Normal";
    public float atk = 10f;
    public float def = 10f;
    public float maxHp = 60f;
    public float nowHp = 60f;
    //���� (���� Ưȭ ����)
    public int intelligence=50;
    //�� (�������� Ưȭ ����)
    public int strength=50;
    //���� (��ɲ� Ưȭ ����)
    public int awareness=50;
    //�ӵ� (��ɲ� Ưȭ ����)
    public int speed=50;
    //����ġ
    public float nowExp=1;
    public float maxExp=100;
    public int Lv=1;

    public Sprite UIImage; //�ʻ�ȭ �̹���

    public int focus = 6; //���߷�
    public int coins = 5; //�ʱ� ����
    
    
}
