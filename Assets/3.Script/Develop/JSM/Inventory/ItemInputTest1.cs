using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // 아이템 먹는 이벤트 임시로 만들어 놓은 스크립트
    public Item[] EatItem; //먹은 아이템
    public QuickSlotController1 quick;

    private void OnApplicationQuit()
    {
        for(int i = 0; i < EatItem.Length; i++)
        {
            EatItem[i].itemCount = 1;
        }
    }

    public void Eat() //버튼 이벤트, 먹는 아이템은 랜덤으로 결정
    {
        int num = Random.Range(0, EatItem.Length);

        InventoryController1.instance.ItemStack(EatItem[num], (int)InventoryController1.instance.playerNum);
        quick.QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}