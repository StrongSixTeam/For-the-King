using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    무기,
    헬멧,
    방어구,
    신발,
    장신구
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
    public EquipType equipType = EquipType.무기;
    public float maxAdDmg;        // 최대 데미지 (AD)
    public float adDmg;           // 공격력
    public float maxApDmg;        // 최대 데미지 (AP)
    public float apDmg;           // 주문력
    public int speed;             // 속도
    public int randomAbility;     // 아이템 얻었을 때 랜덤 능력치 상승폭   
    public int maxSlot;           // 슬롯창 공격 최대 개수
    public AttackType attackType;
}
