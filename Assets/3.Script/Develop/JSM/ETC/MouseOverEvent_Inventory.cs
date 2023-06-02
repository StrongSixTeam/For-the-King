using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverEvent_Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        if(transform.GetComponent<Button>().interactable == true)
        {
            OverInvenItem();
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        InventoryController1.instance.detailUI.gameObject.SetActive(false);
    }

    public void OverInvenItem()
    {
        InventoryController1.instance.overItemName = "";
        string[] itemNameArr = transform.GetChild(1).GetComponent<Text>().text.Split(' ');
        for (int i = 0; i < itemNameArr.Length - 1; i++)
        {
            InventoryController1.instance.overItemName += itemNameArr[i];
            if (itemNameArr.Length % 2 == 0)
            {
                if (i % 2 == 1)
                {
                    InventoryController1.instance.overItemName += " ";
                }
            }

            else
            {
                if (i % 2 == 0)
                {
                    InventoryController1.instance.overItemName += " ";
                }
            }
        }
        //Debug.Log(InventoryController1.instance.itemName);
        InventoryController1.instance.ShowDetailUI();
    }

}
