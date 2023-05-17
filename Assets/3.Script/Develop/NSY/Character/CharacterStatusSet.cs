using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//��ũ���ͺ� ���� ���� �� ��������� �⺻ �̸��� �޴� ���̹�
[CreateAssetMenu(fileName ="CharacterBaseStatsData", menuName = "ScriptableObjects/CharacterBaseStatsData", order =1)  ]
public class CharacterStatusSet : ScriptableObject
{
    
    public int Hp;
    //���� (���� Ưȭ ����)
    public int intelligence;
    //�� (�������� Ưȭ ����)
    public int strength;
    //���� (��ɲ� Ưȭ ����)
    public int awareness;
    //�ӵ� (��ɲ� Ưȭ ����)
    public int speed;
    //����ġ
    public int Exp;
    public int Lv;

    



}
