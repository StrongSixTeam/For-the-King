using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest : MonoBehaviour
{
    public string herb = "Herb";
    public string danceherb = "Danceherb";

    public string EatItem;

    public void Eat()
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
    }
}
