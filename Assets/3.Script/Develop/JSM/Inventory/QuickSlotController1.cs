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
    public List<Item> quickSlot = new List<Item>();

    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
    }


    public void QuickSlotShow() //������ �����ֱ�
    {
        for (int i = 0; i < InventoryController1.instance.itemList.Count; i++)
        {
            if (InventoryController1.instance.itemList[i].itemType == ItemType.��� ||
                InventoryController1.instance.itemList[i].itemType == ItemType.��ũ�� ||
                InventoryController1.instance.itemList[i].itemType == ItemType.��Ÿ)
            {
                if (quickSlot.Count <= 0 ||
                    (quickSlot.Count < 5 &&
                    !quickSlot.Contains(InventoryController1.instance.itemList[i])))
                {
                    quickSlot.Add(InventoryController1.instance.itemList[i]);
                }
            }
            else continue;
            //Debug.Log(ItemList[i].itemName);
        }

        for(int i = 0; i < quickSlot.Count; i++)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = quickSlot[i].itemImage;
            itemCntTxt[i].text = quickSlot[i].itemCount.ToString();
        }
    }
    public void QuickSlotItemUse() //������ ����ϱ�
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        int j;
        for(int i = 0; i < quickSlot.Count; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                j = InventoryController1.instance.itemList.IndexOf(quickSlot[i]);
                if (j != -1)
                {
                    InventoryController1.instance.itemList[j].itemCount--;
                    if (InventoryController1.instance.itemList[j].itemCount < 1)
                    {
                        InventoryController1.instance.itemList[j].itemCount++;
                        InventoryController1.instance.itemList.RemoveAt(j);
                        Destroy(InventoryController1.instance.transform.GetChild(InventoryController1.instance.transform.childCount - 1).gameObject);
                        quickSlot.RemoveAt(i);
                        for(int a = 0; a < itemSlotImg.Length; a++)
                        {
                            itemSlotImg[a].sprite = blank;
                            itemCntTxt[a].text = 0.ToString();
                        }
                    }
                }
                else return;
            }
        }
        QuickSlotShow();
        InventoryController1.instance.InventoryReset();
        InventoryController1.instance.InventoryShow();
    }
}