using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverEvent_Equip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        InventoryController1.instance.itemName = "";
        string[] itemNameArr = transform.GetChild(1).GetComponent<Text>().text.Split(' ');
        for (int i = 0; i < itemNameArr.Length; i++)
        {
            InventoryController1.instance.itemName += itemNameArr[i];
            if (itemNameArr.Length != i + 1)
            {
                InventoryController1.instance.itemName += " ";
            }
        }
        Debug.Log(InventoryController1.instance.itemName);
        InventoryController1.instance.ShowDetailUI();
    }

}