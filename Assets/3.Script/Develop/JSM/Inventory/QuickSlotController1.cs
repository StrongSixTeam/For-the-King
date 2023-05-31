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
    public List<Item> quickSlot = new List<Item>();

    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
    }


    public void QuickSlotShow(int playernum) //아이템 보여주기
    {
        for (int i = 0; i < InventoryController1.instance.playerInventory[playernum].Count; i++)
        {
            if (InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.허브 ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.스크롤 ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.기타)
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
    public void QuickSlotItemUse(int playernum) //아이템 사용하기
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