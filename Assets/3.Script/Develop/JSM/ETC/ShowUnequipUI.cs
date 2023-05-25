using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowUnequipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.hoverImage;
        transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.hoverImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
    }
}
