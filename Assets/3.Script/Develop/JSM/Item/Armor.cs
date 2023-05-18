using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item/armor")]
public class Armor : Item
{
    public EquipType equipType;
    public float additionalHP;
    public float atk;
    public float def;
    public int strength;
    public int intelligence;
    public int awareness;
    public int speed;
}
