using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterContent", menuName = "Encounter")]
public class EncounterContent : ScriptableObject
{
    public string Name;
    public string Content;
    public enum Type
    {
        town,
        interactiveObject,
        enemy,
        dungeon
    }
    public Type type;

    public int slotCount;

    public string slotType;
    public int limit; //성공 기준

}

