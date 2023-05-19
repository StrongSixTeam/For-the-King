using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest : MonoBehaviour
{
    // 아이템 먹는 이벤트 임시로 만들어 놓은 스크립트
    
    public string herb = "Herb";
    public string danceherb = "Danceherb";

    public string EatItem; //먹은 아이템

    public void Eat() //버튼 이벤트, 먹는 아이템은 랜덤으로 결정
    {
        int num = Random.Range(0, 2);

        switch (num)
        {
            case 0:
                EatItem = herb;
                break;
            case 1:
                EatItem = danceherb;
                break;
        }

        QuickSlotController.instance.ItemList.Add(EatItem);
        QuickSlotController.instance.ItemStack(EatItem);
    }
}
