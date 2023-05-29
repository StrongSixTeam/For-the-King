using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType                // ������ Ÿ�� ������
{
    ALL,
    ����,  // ��������
    ��,   // ��������
    ��ũ��,  // ��ũ�� ������
    ���,    // �Ҹ������
    ��Ÿ      // ��Ÿ������
}
[CreateAssetMenu(fileName = "new Item", menuName = "Item/item")]
public class Item : ScriptableObject
{
    
    public string itemName;             // ������ �̸�
    public Sprite itemImage;            // ������ ������ �̹���
    public Sprite itemDetailImage;      // ������ �� �̹���
    public ItemType itemType;           // ������ Ÿ��
    public GameObject itemPrefab;       // ������ ������
    public int itemCount;               // ������ ����
    public string detail_1;               // ������ ���� 1
    public string detail_2;               // ������ ���� 2

}
