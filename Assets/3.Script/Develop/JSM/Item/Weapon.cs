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
    public float maxAdDmg;        // 최대 데미지 (AD)
    public float adDmg;           // 공격력
    public float maxApDmg;        // 최대 데미지 (AP)
    public float apDmg;           // 주문력
    public int speed;             // 속도
    public int randomAbility;     // 아이템 얻었을 때 랜덤 능력치 상승폭
}
