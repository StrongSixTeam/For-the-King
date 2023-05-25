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
}
