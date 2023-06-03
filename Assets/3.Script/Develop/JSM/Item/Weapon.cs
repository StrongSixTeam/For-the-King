using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    ����,
    ���,
    ��,
    �Ź�,
    ��ű�
}

public enum AttackType
{
    attackBlackSmith, //strength
    attackHunter,//awareness
    attackScholar //intelligence
}
[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/weapon")]
public class Weapon : Item
{
    public EquipType equipType = EquipType.����;
    public float maxAdDmg;        // �ִ� ������ (AD)
    public float adDmg;           // ���ݷ�
    public float maxApDmg;        // �ִ� ������ (AP)
    public float apDmg;           // �ֹ���
    public int speed;             // �ӵ�
    public int randomAbility;     // ������ ����� �� ���� �ɷ�ġ �����   
    public int maxSlot;           // ����â ���� �ִ� ����
    public AttackType attackType;
}
