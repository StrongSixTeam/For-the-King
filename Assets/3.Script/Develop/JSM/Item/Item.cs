using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType                // 아이템 타입 열거형
    {
        Equip,  // 장비아이템
        Used,   // 소모아이템
        ETC     // 기타아이템
    }
    public string itemName;             // 아이템 이름
    public Sprite itemImage;            // 아이템 이미지
    public ItemType itemType;           // 아이템 타입
    public GameObject itemPrefab;       // 아이템 프리팹
    public int itemCount;               // 아이템 개수

}
