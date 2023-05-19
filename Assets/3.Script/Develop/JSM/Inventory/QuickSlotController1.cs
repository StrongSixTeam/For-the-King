using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController1 : MonoBehaviour
{
    //public static QuickSlotController1 instance = null;

    [Header("������ ������Ʈ")]
    public Image[] itemSlotImg; //������ ���� (5��)
    public Text[] itemCntTxt; //������ �ؽ�Ʈ (5��)

    [Header("������ ��������Ʈ")]
    public Sprite blank;

    //public List<Item> ItemList = new List<Item>(); 

    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
    }

    
    public void QuickSlotShow() //������ �����ֱ�
    {
        for (int i = 0; i < InventoryController1.instance.itemList.Count; i++)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = InventoryController1.instance.itemList[i].itemImage;
            itemCntTxt[i].text = InventoryController1.instance.itemList[i].itemCount.ToString();

            //Debug.Log(ItemList[i].itemName);
        }
    }
    public void QuickSlotItemUse() //������ ����ϱ�
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < itemSlotImg.Length; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                if (InventoryController1.instance.itemList[i].itemName == "���� ����")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "���ߴ� ����Ǯ")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "���")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "Ÿ�������")
                {
                    InventoryController1.instance.itemList[i].itemCount--;
                    if (InventoryController1.instance.itemList[i].itemCount < 1)
                    {
                        InventoryController1.instance.itemList.RemoveAt(i);
                    }
                }
                else if (InventoryController1.instance.itemList[i].itemName == "�����")
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