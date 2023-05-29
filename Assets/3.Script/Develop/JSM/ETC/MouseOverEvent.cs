using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        OverInvenItem();
    }

    public void OnPointerExit(PointerEventData data)
    {
        InventoryController1.instance.detailUI.gameObject.SetActive(false);
    }

    public void OverInvenItem()
    {
        InventoryController1.instance.itemName = "";
        string[] itemNameArr = transform.GetChild(0).GetChild(2).GetComponent<Text>().text.Split(' ');
        for (int i = 0; i < itemNameArr.Length - 1; i++)
        {
            InventoryController1.instance.itemName += itemNameArr[i];
            if (itemNameArr.Length % 2 == 0)
            {
                if (i % 2 == 1)
                {
                    InventoryController1.instance.itemName += " ";
                }
            }

            else
            {
                if (i % 2 == 0)
                {
                    InventoryController1.instance.itemName += " ";
                }
            }
        }
        //Debug.Log(InventoryController1.instance.itemName);
        InventoryController1.instance.ShowDetailUI();
    }

}
