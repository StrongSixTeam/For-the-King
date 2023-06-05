using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsingShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject currentBtn;
    [SerializeField] bool isEquip = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEquip = currentBtn.transform.GetChild(1).GetComponent<Text>().text.Equals("<color=#FFFFFF>±¸ÀÔ ¹× Âø¿ë</color>");
        //Debug.Log(isEquip);
        if (InventoryController1.instance.isUsed && isEquip)
        {
            return;
        }
        currentBtn.transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.hoverImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InventoryController1.instance.isUsed && isEquip)
        {
            return;
        }
        currentBtn.transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        isEquip = false;
    }
}
