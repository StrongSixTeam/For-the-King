using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // 아이템 먹는 이벤트 임시로 만들어 놓은 스크립트
    public Item[] EatItem; //먹은 아이템
    public QuickSlotController1[] quick;

    public void Eat() //버튼 이벤트, 먹는 아이템은 랜덤으로 결정
    {
        Item item = EatItem[Random.Range(0, EatItem.Length)];

        InventoryController1.instance.ItemStack(item, (int)InventoryController1.instance.playerNum);
        quick[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}