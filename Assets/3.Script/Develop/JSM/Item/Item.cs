using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType                // 아이템 타입 열거형
{
    ALL,
    무기,  // 장비아이템
    방어구,   // 방어구아이템
    스크롤,  // 스크롤 아이템
    허브,    // 소모아이템
    기타      // 기타아이템
}
[CreateAssetMenu(fileName = "new Item", menuName = "Item/item")]
public class Item : ScriptableObject
{
    
    public string itemName;             // 아이템 이름
    public Sprite itemImage;            // 아이템 아이콘 이미지
    public Sprite itemDetailImage;      // 아이템 상세 이미지
    public ItemType itemType;           // 아이템 타입
    public GameObject itemPrefab;       // 아이템 프리팹
    public int itemCount;               // 아이템 개수
    public string detail_1;               // 아이템 설명 1
    public string detail_2;               // 아이템 설명 2

}
