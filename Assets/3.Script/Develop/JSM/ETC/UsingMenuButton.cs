using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UsingMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject currentBtn;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((InventoryController1.instance.isEquip && currentBtn.CompareTag("¹«±â")) || currentBtn.CompareTag("´Ý±â"))
        {
            transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.hoverImage;
        }
        else if ((InventoryController1.instance.isUsed && currentBtn.CompareTag("Çãºê")) || currentBtn.CompareTag("´Ý±â"))
        {
            transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.hoverImage;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if ((InventoryController1.instance.isEquip && currentBtn.CompareTag("¹«±â")) || currentBtn.CompareTag("´Ý±â"))
        {
            transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        }
        else if ((InventoryController1.instance.isUsed && currentBtn.CompareTag("Çãºê")) || currentBtn.CompareTag("´Ý±â"))
        {
            transform.GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        }
    }
}
