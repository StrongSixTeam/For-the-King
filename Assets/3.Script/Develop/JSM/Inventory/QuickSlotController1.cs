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


    public void QuickSlotShow(int playernum) //������ �����ֱ�
    {
        for (int i = 0; i < InventoryController1.instance.playerInventory[playernum].Count; i++)
        {
            if (InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��� ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��ũ�� ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��Ÿ)
            {
                if (quickSlot.Count <= 0 ||
                    (quickSlot.Count < 5 &&
                    !quickSlot.Contains(InventoryController1.instance.playerInventory[playernum][i])))
                {
                    quickSlot.Add(InventoryController1.instance.playerInventory[playernum][i]);
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
    public void QuickSlotItemUse(int playernum) //������ ����ϱ�
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        //GameObject Hover = EventSystem.current.IsPointerOverGameObject;
        int j;
        for (int i = 0; i < quickSlot.Count; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                j = InventoryController1.instance.playerInventory[playernum].IndexOf(quickSlot[i]);
                if (j != -1)
                {
                    InventoryController1.instance.playerInventory[playernum][j].itemCount--;
                    if (InventoryController1.instance.playerInventory[playernum][j].itemCount < 1)
                    {
                        InventoryController1.instance.playerInventory[playernum][j].itemCount++;
                        InventoryController1.instance.playerInventory[playernum].RemoveAt(j);
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
        QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryReset();
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}