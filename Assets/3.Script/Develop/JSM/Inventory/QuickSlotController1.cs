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


    public void QuickSlotShow(int playernum) //������ �����ֱ�
    {
        for (int i = 0; i < InventoryController1.instance.playerInventory[playernum].Count; i++)
        {
            if (InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��� ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��ũ�� ||
                InventoryController1.instance.playerInventory[playernum][i].itemType == ItemType.��Ÿ)
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
            }
            else continue;
        }
        
        //Debug.Log(quickSlot[playernum].Count);
    }

    public void OnClickQuickSlotItemUse()
    {
        QuickSlotItemUse((int)InventoryController1.instance.playerNum);
    }

    public void QuickSlotItemUse(int playernum) //������ ����ϱ�
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
                    InventoryController1.instance.itemCount[playernum][j]--;
                    if (InventoryController1.instance.itemCount[playernum][j] < 1)
                    {
                        InventoryController1.instance.itemCount[playernum].RemoveAt(j);
                        InventoryController1.instance.playerInventory[playernum].RemoveAt(j);
                        InventoryController1.instance.transform.GetChild(InventoryController1.instance.transform.childCount - 1).gameObject.SetActive(false);
                        quickSlot[playernum].RemoveAt(i);
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
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}