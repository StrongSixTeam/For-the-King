using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    //private Queue<GameObject> PoolItemQueue = new Queue<GameObject>();
    GameObject shopItem;
    public List<Item> shopItemList = new List<Item>();
    public GameObject ShopItemSelectUI;
    public List<int> shopItemCount = new List<int>();
    public Text shopCoinText;
    Vector3 poolPos;
    Vector3 topPos;
    Vector3 listPos;
    Vector3 point;
    void Awake()
    {
        poolPos = new Vector3(0, 0, -100);
        topPos = new Vector3(0, 225);
        listPos = topPos;
    }

    private void Start()
    {
        CreateShopItem();
        ShopSetting();
    }

    public void MarketEnter()
    {
        transform.parent.parent.parent.gameObject.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        for(int i = 0; i < shopItemCount.Count; i++)
        {
            InventoryController1.instance.allItemArr.EatItem[i].stock = shopItemCount[i];
        }
    }

    void CreateShopItem()
    {
        for (int i = 0; i < InventoryController1.instance.allItemArr.EatItem.Length - 1; i++)
        {
            shopItemList.Add(InventoryController1.instance.allItemArr.EatItem[i]);
            shopItem = Instantiate(itemPrefab, poolPos, Quaternion.identity);
            shopItem.SetActive(false);
            //PoolItemQueue.Enqueue(shopItem);
            shopItem.transform.SetParent(this.transform);
            shopItem.transform.localPosition = listPos;
            listPos.y -= 40f;
            shopItemCount.Add(InventoryController1.instance.allItemArr.EatItem[i].stock);
        }
    }

    void ShopReset()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShopSetting()
    {
        ShopReset();
        for (int i = 0; i < shopItemList.Count; i++)
        {
            switch (shopItemList[i].itemType)
            {
                case ItemType.무기:
                    Weapon weapon = shopItemList[i] as Weapon;
                    transform.GetChild(i).transform.tag = weapon.equipType.ToString();
                    transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().tag = shopItemList[i].itemType.ToString();
                    break;
                case ItemType.방어구:
                    Armor armor = shopItemList[i] as Armor;
                    armor = shopItemList[i] as Armor;
                    transform.GetChild(i).transform.tag = armor.equipType.ToString();
                    transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().tag = shopItemList[i].itemType.ToString();
                    break;
                case ItemType.허브:
                    transform.GetChild(i).transform.tag = shopItemList[i].itemType.ToString();
                    transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().tag = shopItemList[i].itemType.ToString();
                    break;
                case ItemType.스크롤:
                    transform.GetChild(i).transform.tag = shopItemList[i].itemType.ToString();
                    transform.GetChild(i).transform.GetChild(0).GetComponent<Button>().tag = shopItemList[i].itemType.ToString();
                    break;

                default:
                    break;
            }

            //transform.GetChild(i).transform.localPosition = listPos;
            //listPos.y -= 40f;
            //Debug.Log(ListPos.y);

            transform.GetChild(i).transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = shopItemList[i].itemImage;
            transform.GetChild(i).transform.GetChild(0).GetChild(1).GetComponent<Text>().text = shopItemList[i].itemName;
            transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = shopItemList[i].price.ToString();
            transform.GetChild(i).transform.GetChild(0).GetChild(3).GetComponent<Text>().text = shopItemList[i].stock.ToString();
            shopCoinText.text = GameManager.instance.Players[(int)InventoryController1.instance.playerNum].GetComponent<PlayerStat>().coins.ToString();
            transform.GetChild(i).transform.gameObject.SetActive(true);
            
        }
        InventoryController1.instance.playerNum = (PlayerNum)GameManager.instance.MainPlayer.GetComponent<PlayerStat>().order;
        ComparePrice();
    }

    private void ComparePrice()
    {
        for(int i = 0; i < shopItemList.Count; i++)
        {
            if(GameManager.instance.Players[(int)InventoryController1.instance.playerNum].GetComponent<PlayerStat>().coins 
                < int.Parse(transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text))
            {
                transform.GetChild(i).transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.gray;
                transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().color = Color.gray;
                transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = false;
            }
            else
            {
                transform.GetChild(i).transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.white;
                transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().color = Color.white;
                transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ShowShopUI()
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        ItemType itemtype = (ItemType)System.Enum.Parse(typeof(ItemType), Click.transform.tag);
        InventoryController1.instance.itemName = Click.transform.GetChild(1).GetComponent<Text>().text;
        point = Input.mousePosition;
        ShopItemSelectUI.transform.position = new Vector3(point.x + 95f, point.y - 77.5f, 0f);

        switch (itemtype)
        {
            case ItemType.무기:
            case ItemType.방어구:
                InventoryController1.instance.isEquip = true;
                InventoryController1.instance.isUsed = false;
                // 구입, 구입 및 착용 버튼 활성화
                ShopItemSelectUI.transform.GetChild(0).GetComponentInChildren<Button>().interactable = true;
                ShopItemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = true;
                ShopItemSelectUI.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                ShopItemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.actImage;
                ShopItemSelectUI.transform.GetChild(1).GetChild(1).GetComponentInChildren<Text>().color = Color.white;
                ShopItemSelectUI.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().color = Color.white;

                break;
            case ItemType.스크롤:
            case ItemType.허브:
                InventoryController1.instance.isUsed = true;
                InventoryController1.instance.isEquip = false;
                // 구입 및 착용 버튼 비활성화
                ShopItemSelectUI.transform.GetChild(1).GetComponentInChildren<Button>().interactable = false;
                ShopItemSelectUI.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>().sprite =
                    InventoryController1.instance.deactImage;
                break;
        }
        ShopItemSelectUI.SetActive(true);

    }

    public void ExitShop()
    {
        transform.parent.parent.parent.gameObject.SetActive(false);
    }

    public void QuitShopUI()
    {
        InventoryController1.instance.isEquip = false;
        InventoryController1.instance.isUsed = false;
        ShopItemSelectUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        ShopItemSelectUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = InventoryController1.instance.actImage;
        ShopItemSelectUI.gameObject.SetActive(false);
        //InventoryController1.instance.itemName = "";
    }

    public void BuyItem()
    {
        Item buyItem;
        int x;
        for(int i = 0; i < shopItemList.Count; i++)
        {
            if (shopItemList[i].itemName.Equals(InventoryController1.instance.itemName))
            {
                buyItem = shopItemList[i];
                GameManager.instance.Players[(int)InventoryController1.instance.playerNum].GetComponent<PlayerStat>().coins -=
                        buyItem.price;
                if (shopItemList[i].stock <= 1) // 상점에서 마지막 하나 남은 아이템을 샀을 경우
                {
                    shopItemList.RemoveAt(i);
                    transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                }

                else                                                               // 그렇지 않을 경우
                {
                    shopItemList[i].stock--;
                }
                x = InventoryController1.instance.playerInventory[(int)InventoryController1.instance.playerNum].IndexOf(buyItem);
                if ( x == -1)   // 구입한 아이템이 플레이어의 인벤토리 내에 없을 경우
                {
                    InventoryController1.instance.ItemStack(buyItem, (int)InventoryController1.instance.playerNum);
                    InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
                    InventoryController1.instance.quickSlot[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
                }
                else            // 구입한 아이템이 플레이어의 인벤토리 내에 있을 경우
                {
                    InventoryController1.instance.itemCount[(int)InventoryController1.instance.playerNum][x]++;
                    InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
                    InventoryController1.instance.quickSlot[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
                }
                
            }

        }
        ShopSetting();
        QuitShopUI();
    }

    public void BuyAndEquipItem()
    {
        BuyItem();
        InventoryController1.instance.EquipItem();
    }
}
