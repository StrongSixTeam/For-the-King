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


    public int limit; //���� ����

    public bool isCleared = false; //Ŭ���� ����

    public string slotType;
    public int slotCount;
    [Header("Enemy ����")]
    public int enemyCount; //���� �������
    public int runCount; //���� �������� ī��Ʈ
}

