using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterContent", menuName = "Encounter")]
public class EncounterContent : ScriptableObject
{
    public string Name;
    public string Content;
    public string extraContent;
    public enum Type
    {
        town,
        interactiveObject,
        enemy,
        dungeon,
        sanctum
    }
    public Type type;


    public int limit; //성공 기준

    public bool isCleared = false; //클리어 여부

    public string slotType;
    public int slotCount;
    [Header("Enemy 관련")]
    public int enemyCount; //적이 몇마리인지
    public int runCount; //몰래 지나가기 카운트
}

