using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController1 : MonoBehaviour
{
    //public static QuickSlotController1 instance = null;

    [Header("퀵슬롯 오브젝트")]
    public Image[] itemSlotImg; //아이템 슬롯 (5개)
    public Text[] itemCntTxt; //아이템 텍스트 (5개)

    [Header("아이템 스프라이트")]
    public Sprite blank;

    //public List<Item> ItemList = new List<Item>(); 

    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
    }

    
    public void QuickSlotShow() //아이템 보여주기
    {
        for (int i = 0; i < InventoryController1.instance.itemList.Count; i++)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = InventoryController1.instance.itemList[i].itemImage;
            itemCntTxt[i].text = InventoryController1.instance.itemList[i].itemCount.ToString();

            //Debug.Log(ItemList[i].itemName);
        }
    }
    public void QuickSlotItemUse() //아이템 사용하기
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < itemSlotImg.Length; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                if (InventoryController1.instance.itemList[i].itemName == "신의 수염")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "춤추는 쐐기풀")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "대거")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "타운가드투구")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "목걸이")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
            }
        }
    }
}