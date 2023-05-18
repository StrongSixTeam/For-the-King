using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType                // ������ Ÿ�� ������
    {
        Equip,  // ��������
        Used,   // �Ҹ������
        ETC     // ��Ÿ������
    }
    public string itemName;             // ������ �̸�
    public Sprite itemImage;            // ������ �̹���
    public ItemType itemType;           // ������ Ÿ��
    public GameObject itemPrefab;       // ������ ������
    public int itemCount;               // ������ ����

}
