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
    public List<List<Item>> quickSlot = new List<List<Item>>();

    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
        for(int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++)
        {
            quickSlot.Add(new List<Item>());
        }
    }


    public void QuickSlotShow(int playernum) //아이템 보여주기
    {
        for (int i = 0; i < InventoryController1.instance.playerInventory[playernum].Count; i++)
        {
            if (InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.허브 ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.스크롤 ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.기타)
            {
                if (quickSlot[playernum].Count <= 0 ||
                    (quickSlot[playernum].Count < 5 &&
                    !quickSlot[playernum].Contains(InventoryController1.instance.playerInventory[playernum][i])))
                {
                    quickSlot[playernum].Add(InventoryController1.instance.playerInventory[playernum][i]);
                }
                /*else
                {
                    InventoryController1.instance.itemCount[playernum][i]++;
                }*/
            }
            else continue;
            //Debug.Log(ItemList[i].itemName);
        }
        for(int i = 0; i < InventoryController1.instance.playerInventory[playernum].Count; i++)
        {
            int j = quickSlot[playernum].IndexOf(InventoryController1.instance.playerInventory[playernum][i]);
            if (j != -1)
            {
                itemSlotImg[j].GetComponent<Image>().sprite = quickSlot[playernum][j].itemImage;
                itemCntTxt[j].text = InventoryController1.instance.itemCount[playernum][i].ToString();
                if (itemCntTxt[j].text.Equals("0")) itemCntTxt[j].text = "";
            }
            else continue;
        }
        
        //Debug.Log(quickSlot[playernum].Count);
    }

    public void OnClickQuickSlotItemUse()
    {
        QuickSlotItemUse((int)InventoryController1.instance.playerNum);
    }

    public void QuickSlotItemUse(int playernum) //아이템 사용하기
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        InventoryController1.instance.playerNum = (PlayerNum)System.Enum.Parse(typeof(PlayerNum), Click.transform.parent.parent.tag);
        //GameObject Hover = EventSystem.current.IsPointerOverGameObject;
        int j;
        for (int i = 0; i < quickSlot[playernum].Count; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                j = InventoryController1.instance.playerInventory[playernum].IndexOf(quickSlot[playernum][i]);
                if (j != -1)
                {
                    Used used = InventoryController1.instance.playerInventory[playernum][j] as Used;
                    InventoryController1.instance.itemCount[playernum][j]--;
                    if (GameManager.instance.Players[playernum].GetComponent<PlayerStat>().nowHp + used.recoveryStat > GameManager.instance.Players[playernum].GetComponent<PlayerStat>().maxHp)
                    {
                        GameManager.instance.Players[playernum].GetComponent<PlayerStat>().nowHp = GameManager.instance.Players[playernum].GetComponent<PlayerStat>().maxHp;
                    }
                    else
                    {
                        GameManager.instance.Players[playernum].GetComponent<PlayerStat>().nowHp += used.recoveryStat;
                    }
                    if (InventoryController1.instance.itemCount[playernum][j] < 1)
                    {
                        InventoryController1.instance.itemCount[playernum].RemoveAt(j);
                        InventoryController1.instance.playerInventory[playernum].RemoveAt(j);
                        InventoryController1.instance.transform.GetChild(InventoryController1.instance.transform.childCount - 1).gameObject.SetActive(false);
                        quickSlot[playernum].RemoveAt(i);
                        for(int a = 0; a < itemSlotImg.Length; a++)
                        {
                            itemSlotImg[a].sprite = blank;
                            itemCntTxt[a].text = "";
                        }
                    }
                }
                else return;
            }
        }
        QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}