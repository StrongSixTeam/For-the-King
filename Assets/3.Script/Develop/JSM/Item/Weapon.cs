using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    Weapon,
    Armor,
    Accessory
}
[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/weapon")]
public class Weapon : Item
{
    public EquipType equipType = EquipType.Weapon;
    public float maxAdDmg;        // �ִ� ������ (AD)
    public float adDmg;           // ���ݷ�
    public float maxApDmg;        // �ִ� ������ (AP)
    public float apDmg;           // �ֹ���
    public int speed;             // �ӵ�
    public int randomAbility;     // ������ ����� �� ���� �ɷ�ġ �����
}
