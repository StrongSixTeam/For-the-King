using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject originalBtn;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite colorChangeSprite;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        originalBtn.GetComponent<Image>().sprite = colorChangeSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        originalBtn.GetComponent<Image>().sprite = originalSprite;
    }
}
