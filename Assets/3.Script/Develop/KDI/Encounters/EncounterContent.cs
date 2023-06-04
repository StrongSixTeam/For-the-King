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
    public bool isShowed = true; //���̴� ����

    public string slotType;
    public int slotCount;

    public Sprite preview; //������ �� �ߴ� UI �̹���

    [Header("Enemy ����")]
    public int enemyCount; //���� �������
    public int runCount; //���� �������� ī��Ʈ
    public int level;
}

