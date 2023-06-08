using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum PlayerNum { Player0, Player1, Player2}
public class InventoryController1 : MonoBehaviour
{
    public static InventoryController1 instance = null;
    [Header("������ ����Ʈ ������")]
    [SerializeField] private GameObject itemlistPrebs;
    public GameObject itemSelectUI;                     // �κ��丮 �� ������ Ŭ�� �� �����Ǵ� UI
    public GameObject equipItemSelectUI;                // ���â �� ���� ������ Ŭ�� �� �����Ǵ� UI
    public GameObject detailUI;                         // ���콺 ���� �� ��Ÿ���� UI

    private Vector3 poolPos;
    private Vector2 ListPos; //������ ���� ��ġ
    private Vector2 topListPos; //������ ���� ��ġ �ֻ��

    private Queue<GameObject> poolItemQueue = new Queue<GameObject>();

    //public List<Image> InvenImg = new List<Image>();
    //public List<Text> InvenTxt = new List<Text>();

    [Header("�÷��̾� �ĺ� ��ȣ")]
    public PlayerNum playerNum;                     // �÷��̾� ��ȣ
    public List<List<Item>> playerInventory = new List<List<Item>>(); // �÷��̾� �� �κ��丮
    public List<List<int>> itemCount = new List<List<int>>();

    private GameObject item;
    [SerializeField] private GameObject category;

    [Header("ī�װ��� ����Ʈ")]
    [SerializeField] private List<Item> categoryWeapon = new List<Item>();
    [SerializeField] private List<Item> categoryArmor = new List<Item>();
    [SerializeField] private List<Item> categoryScroll = new List<Item>();
    [SerializeField] private List<Item> categoryHerb = new List<Item>();
    [SerializeField] private List<Item> categoryETC = new List<Item>();
    [SerializeField] private List<int> categoryWeaponCnt = new List<int>();
    [SerializeField] private List<int> categoryArmorCnt = new List<int>();
    [SerializeField] private List<int> categoryScrollCnt = new List<int>();
    [SerializeField] private List<int> categoryHerbCnt = new List<int>();
    [SerializeField] private List<int> categoryETCCnt = new List<int>();

    public Sprite deactImage;
    public Sprite actImage;
    public Sprite hoverImage;
    public bool isEquip = false;
    public bool isUsed = false;

    [Header("�κ��丮 ���â")]
    public List<Item[]> playerEquip = new List<Item[]>();
    public Text[] equipItemName = new Text[5];
    public string itemName = "";
    public string overItemName = "";

    private EquipType equipType;
    [SerializeField] Button[] equipBtn;

    [Header("������")]
    public QuickSlotController1[] quickSlot;

    [Header("��ü ������ ���� �迭")]
    public ItemInputTest1 allItemArr;
    public Text coinText;

    private void Awake()
    {
        instance = this;
        Debug.Log("�κ��丮 �Ͼ�ǿ� �ѿ�");
        for(int i = 0; i < equipBtn.Length; i++)
        {
            if (equipItemName[i].text.Equals(""))
            {
                equipBtn[i].interactable = false;
            }
        }
        
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++)
        {
            playerInventory.Add(new List<Item>());
            playerEquip.Add(new Item[5]);
            itemCount.Add(new List<int>());
        }
        poolPos = new Vector3(0, 0, -100);
        topListPos = new Vector3(0, 105);
        CreatePoolItem();
        ListPos = topListPos;
        Debug.Log("�����ũ");
    }

    /*private void Start()
    {
    }

    private void SetItemList()
    {
        
    }*/

    private void CreatePoolItem() // ������Ʈ Ǯ���� ����Ʈ �ʱ�ȭ
    {
        ListPos = topListPos;
        for(int i = 0; i < allItemArr.EatItem.Length; i++)
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
            item.SetActive(false);
            item.transform.SetParent(this.transform);
            item.transform.localPosition = ListPos;
            ListPos.y -= 25f;
            poolItemQueue.Enqueue(item);
        }
    }

    public void ItemStack(Item Item, int playerNum) //������ �ֱ�
    {
        int i = playerInventory[playerNum].IndexOf(Item);
        if (i != -1)
        {
            itemCount[playerNum][i]++;
            //Debug.Log(playerInventory[playerNum][i] + ": " + itemCount[playerNum][i]);
            if (itemCount[playerNum][i] < 1)
            {
                Debug.Log("���� �ɸ��� �� ��.");
                playerInventory[playerNum].RemoveAt(i);
                itemCount[playerNum].RemoveAt(i);
            }
        }
        else
        {
            playerInventory[playerNum].Add(Item);
            itemCount[playerNum].Add(1);
            PoolItemListStack(Item);
            //Debug.Log(Item + ": 1������ ��");
        }
        for (int j = 0; j < itemCount[playerNum].Count; j++)
        {
            //Debug.Log("itemCount�� " + j + "��° �ε����� ��: " + itemCount[playerNum][j]);
        }
        
    }
    public void PoolItemListStack(Item putitem)         // ������Ʈ Ǯ�� �Լ�
    {
        if (poolItemQueue.Count < 1) return;
        GameObject newitem = poolItemQueue.Dequeue();
        //Debug.Log(this.transform.childCount);
        //newitem.transform.SetParent(this.transform);
        //Debug.Log(this.transform.childCount);
        switch (putitem.itemType)
        {
            case ItemType.����:
                Weapon weapon = putitem as Weapon;
                newitem.transform.tag = weapon.equipType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.��:
                Armor armor = putitem as Armor;
                armor = putitem as Armor;
                newitem.transform.tag = armor.equipType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.���:
                newitem.transform.tag = putitem.itemType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.��ũ��:
                newitem.transform.tag = putitem.itemType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;

            default:
                break;
        }

        //newitem.transform.localPosition = ListPos;
        //ListPos.y -= 25f;
        //Debug.Log(ListPos.y);

        newitem.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = putitem.itemImage;
        newitem.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = putitem.itemName;

    }

    public void InventoryShow(int playernum)          // �÷��̾� ���� ���ҵ� �κ��丮 �����ֱ�
    {
        InventoryReset();
        coinText.text = GameManager.instance.Players[playernum].GetComponent<PlayerStat>().coins.ToString();
        
        if (playerInventory[playernum].Count < 1) return;

        ListPos = topListPos;
        for (int i = 0; i < playerInventory[playernum].Count; i++)
        {
            if (playerInventory[playernum][i].GetType().Name.Equals("Weapon"))
            {
                Weapon weapon = playerInventory[playernum][i] as Weapon;
                //Debug.Log(transform.childCount);
                //Debug.Log(itemList.Count);
                transform.GetChild(i).tag = weapon.equipType.ToString();
            }
            else if (playerInventory[playernum][i].GetType().Name.Equals("Armor"))
            {
                Armor armor = playerInventory[playernum][i] as Armor;
                transform.GetChild(i).tag = armor.equipType.ToString();
            }
            else if (playerInventory[playernum][i].GetType().Name.Equals("Used"))
            {
                transform.GetChild(i).tag = playerInventory[playernum][i].itemType.ToString();
            }
            transform.GetChild(i).GetChild(0).tag = playerInventory[playernum][i].itemType.ToString();
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = playerInventory[playernum][i].itemImage;
            transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>().text = playerInventory[playernum][i].itemName + " " + itemCount[playernum][i].ToString();
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).localPosition = ListPos;

            ListPos.y -= 25f;
        }
    }

    public void InventoryReset()
    {
        /*if(transform.childCount > playerInventory[(int)playerNum].Count)
        {
            for (int i = 0; i < playerInventory[(int)playerNum].Count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }*/
        //transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void ResetCategory()
    {
        categoryWeapon.Clear();
        categoryArmor.Clear();
        categoryScroll.Clear();
        categoryHerb.Clear();
        categoryETC.Clear();
    }

    public void ShowCategory()
    {
        ResetCategory();
        SetCategory();
        GameObject clickbtn = EventSystem.current.currentSelectedGameObject;
        Text btnText = clickbtn.GetComponentInChildren<Text>();
        ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), btnText.text);
        switch (type)
        {
            case ItemType.����:
                CategoryActive(categoryWeapon, categoryWeaponCnt);
                break;

            case ItemType.��:
                CategoryActive(categoryArmor, categoryArmorCnt);
                break;

            case ItemType.��ũ��:
                CategoryActive(categoryScroll, categoryScrollCnt);
                break;

            case ItemType.���:
                CategoryActive(categoryHerb, categoryHerbCnt);
                break;

            case ItemType.��Ÿ:
                CategoryActive(categoryETC, categoryETCCnt);
                break;

            case ItemType.ALL:
                InventoryShow((int)playerNum);
                break;
        }

    }

    private void SetCategory()
    {
        for (int i = 0; i < playerInventory[(int)playerNum].Count; i++)
        {
            switch (playerInventory[(int)playerNum][i].itemType)
            {
                case ItemType.����:
                    categoryWeapon.Add(playerInventory[(int)playerNum][i]);
                    categoryWeaponCnt.Add(itemCount[(int)playerNum][i]);
                    break;
                case ItemType.��:
                    categoryArmor.Add(playerInventory[(int)playerNum][i]);
                    categoryArmorCnt.Add(itemCount[(int)playerNum][i]);
                    break;
                case ItemType.��ũ��:
                    categoryScroll.Add(playerInventory[(int)playerNum][i]);
                    categoryScrollCnt.Add(itemCount[(int)playerNum][i]);
                    break;
                case ItemType.���:
                    categoryHerb.Add(playerInventory[(int)playerNum][i]);
                    categoryHerbCnt.Add(itemCount[(int)playerNum][i]);
                    break;
                case ItemType.��Ÿ:
                    categoryETC.Add(playerInventory[(int)playerNum][i]);
                    categoryETCCnt.Add(itemCount[(int)playerNum][i]);
                    break;
            }
        }
    }

    private void CategoryActive(List<Item> category, List<int> categoryCnt)
    {
        if (category.Count < 1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (category.Count < transform.childCount)
        {
            for (int i = transform.childCount - 1; i >= category.Count; i--)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < category.Count; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = category[i].itemImage;
            transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>().text = category[i].itemName + " " + categoryCnt[i].ToString();
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ShowItemSelectUI()
    {
        itemSelectUI.gameObject.SetActive(true);
    }

    public void QuitItemSelectUI()
    {
        isEquip = false;
        isUsed = false;
        itemSelectUI.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = actImage;
        itemSelectUI.gameObject.SetActive(false);
        itemName = "";
    }

    public void SelectInvenEquipItem()              // �κ��丮�� �ִ� ������ Ŭ�� �� �������� �̸��� �޾ƿ��� �Լ�
    {
        itemName = "";
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        string[] itemNameArr = Click.transform.GetChild(1).GetComponent<Text>().text.Split(' ');
        for (int i = 0; i < itemNameArr.Length - 1; i++)
        {
            itemName += itemNameArr[i];
            if(itemNameArr.Length % 2 == 0)
            {
                if (i % 2 == 1)
                {
                    itemName += " ";
                }
            }
            
            else
            {
                if (i % 2 == 0)
                {
                    itemName += " ";
                }
            }
        }
        //Debug.Log(itemName);
    }


    public void EquipItem()
    {
        //Debug.Log("���� ��ư ��������");
        //Debug.Log(itemName);
        Equip(itemName);
        QuitItemSelectUI();
        InventoryShow((int)playerNum);
    }

    public void UseItem()
    {
        ItemListUse();
        QuitItemSelectUI();
        InventoryShow((int)playerNum);
        quickSlot[(int)playerNum].QuickSlotShow((int)playerNum);
    }

    private void ItemListUse()
    {
        for (int i = 0; i < playerInventory[(int)playerNum].Count; i++)
        {
            if (playerInventory[(int)playerNum][i].itemName.Equals(itemName))
            {
                Used used = playerInventory[(int)playerNum][i] as Used;
                switch (playerInventory[(int)playerNum][i].itemType)
                {
                    case ItemType.���:
                        if(itemCount[(int)playerNum][i] <= 1)
                        {
                            //Destroy();
                            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                            poolItemQueue.Enqueue(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot[(int)playerNum].quickSlot[(int)playerNum].Remove(playerInventory[(int)playerNum][i]);
                            playerInventory[(int)playerNum].RemoveAt(i);
                            itemCount[(int)playerNum].RemoveAt(i);
                            for (int a = 0; a < quickSlot[(int)playerNum].itemSlotImg.Length; a++)
                            {
                                quickSlot[(int)playerNum].itemSlotImg[a].sprite = quickSlot[(int)playerNum].blank;
                                quickSlot[(int)playerNum].itemCntTxt[a].text = "";
                            }
                            
                        }

                        else
                        {
                            itemCount[(int)playerNum][i]--;
                        }
                        if(used.itemName.Equals("���� ����"))
                        {
                            if (GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowHp + used.recoveryStat > GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().maxHp)
                            {
                                GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowHp = GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().maxHp;
                            }
                            else
                            {
                                GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowHp += used.recoveryStat;
                            }
                        }
                        else if (used.itemName.Equals("Ȳ�� �Ѹ�"))
                        {
                            if (GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowFocus + used.recoveryStat > GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().maxFocus)
                            {
                                GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowFocus = GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().maxFocus;
                            }
                            else
                            {
                                GameManager.instance.Players[(int)playerNum].GetComponent<PlayerStat>().nowFocus += (int)used.recoveryStat;
                            }
                        }
                        
                        break;

                    case ItemType.��ũ��:
                        if (itemCount[(int)playerNum][i] <= 1)
                        {
                            //Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                            poolItemQueue.Enqueue(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot[(int)playerNum].quickSlot[(int)playerNum].Remove(playerInventory[(int)playerNum][i]);
                            playerInventory[(int)playerNum].RemoveAt(i);
                            for (int a = 0; a < quickSlot[(int)playerNum].itemSlotImg.Length; a++)
                            {
                                quickSlot[(int)playerNum].itemSlotImg[a].sprite = quickSlot[(int)playerNum].blank;
                                quickSlot[(int)playerNum].itemCntTxt[a].text = 0.ToString();
                            }
                        }

                        else
                        {
                            itemCount[(int)playerNum][i]--;
                        }
                        break;

                    case ItemType.��Ÿ:
                        if (itemCount[(int)playerNum][i] <= 1)
                        {
                            //Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                            poolItemQueue.Enqueue(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot[(int)playerNum].quickSlot[(int)playerNum].Remove(playerInventory[(int)playerNum][i]);
                            playerInventory[(int)playerNum].RemoveAt(i);
                            for (int a = 0; a < quickSlot[(int)playerNum].itemSlotImg.Length; a++)
                            {
                                quickSlot[(int)playerNum].itemSlotImg[a].sprite = quickSlot[(int)playerNum].blank;
                                quickSlot[(int)playerNum].itemCntTxt[a].text = 0.ToString();
                            }
                        }

                        else
                        {
                            itemCount[(int)playerNum][i]--;
                        }
                        break;
                }
            }
        }
    }

    private void Equip(string ItemName)
    {
        Weapon weapon;
        Armor armor;
        for (int i = 0; i < playerInventory[(int)playerNum].Count; i++)
        {
            //Debug.Log(i.ToString() + "��° ����");
            //Debug.Log(itemList[i].itemName.Equals(ItemName));

            if (playerInventory[(int)playerNum][i].itemName.Equals(ItemName))
            {
                switch (playerInventory[(int)playerNum][i].itemType)
                {
                    case ItemType.����:
                        weapon = playerInventory[(int)playerNum][i] as Weapon;
                        UpdateEquip(weapon, i);
                        break;
                    case ItemType.��:
                        armor = playerInventory[(int)playerNum][i] as Armor;
                        switch (armor.equipType)
                        {
                            case EquipType.���:
                                UpdateEquip(armor, i, 1);
                                break;

                            case EquipType.��:
                                UpdateEquip(armor, i, 2);
                                break;

                            case EquipType.�Ź�:
                                UpdateEquip(armor, i, 3);
                                break;

                            case EquipType.��ű�:
                                UpdateEquip(armor, i, 4);
                                break;
                        }
                        break;
                    default:
                        return;
                }
                break;
            }

        }
    }

    private void UpdateEquip(Weapon weapon, int i)
    {
        if (playerEquip[(int)playerNum][0] == null)             // �ش� ���â�� �ƹ��͵� ������ �Ǿ����� ���� ���
        {
            playerEquip[(int)playerNum][0] = weapon;
            equipItemName[0].text = weapon.itemName;
            equipBtn[0].interactable = true;
            int num = 0;
            if (playerNum == PlayerNum.Player0)
            {
                num = 0;
            }
            else if (playerNum == PlayerNum.Player1)
            {
                num = 1;
            }
            else if (playerNum == PlayerNum.Player2)
            {
                num = 2;
            }
            GameManager.instance.Players[num].GetComponent<PlayerStat>().weapon = weapon;
        }

        else if (playerEquip[(int)playerNum][0].itemName.Equals(weapon.itemName))      // �ش� ���â�� �ִ� ���� �����Ϸ��� ��� ���� ���
        {
            return;
        }

        else                                // �ش� ���â�� �̹� ����� ��� �ִ� ���
        {
            int num = 0;
            if (playerNum == PlayerNum.Player0)
            {
                num = 0;
            }
            else if (playerNum == PlayerNum.Player1)
            {
                num = 1;
            }
            else if (playerNum == PlayerNum.Player2)
            {
                num = 2;
            }
            GameManager.instance.Players[num].GetComponent<PlayerStat>().weapon = weapon; //weapon�� �ݿ�
            int x = playerInventory[(int)playerNum].IndexOf(playerEquip[(int)playerNum][0]); // ���� ���� ��� �κ��丮�� �ִ��� Ȯ���ϱ� ���� �ε��� ��ȣ �Ҵ�
            if (x == -1)                           // ���� ���� ��� �κ��丮�� ���ٸ�
            {
                ItemStack(playerEquip[(int)playerNum][0], (int)playerNum);
                playerEquip[(int)playerNum][0] = weapon;
                equipItemName[0].text = weapon.itemName;
            }
            else
            {                                       // ���� ���̴� ��� �κ��丮�� �ִٸ�
                itemCount[(int)playerNum][x]++;
                playerEquip[(int)playerNum][0] = weapon;
                equipItemName[0].text = weapon.itemName;
            }
        }

        if (itemCount[(int)playerNum][i] <= 1)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            poolItemQueue.Enqueue(transform.GetChild(transform.childCount - 1).gameObject);
            playerInventory[(int)playerNum].RemoveAt(i);
            itemCount[(int)playerNum].RemoveAt(i);
            ListPos.y += 25f;
            // Debug.Log("���� 1�� ���� �� ���� �� " + ListPos.y);
        }
        else
        {
            itemCount[(int)playerNum][i]--;
        }
    }

    private void UpdateEquip(Armor armor, int i, int arrNum)
    {
        int num = 0;
        if (playerNum == PlayerNum.Player0)
        {
            num = 0;
        }
        else if (playerNum == PlayerNum.Player1)
        {
            num = 1;
        }
        else if (playerNum == PlayerNum.Player2)
        {
            num = 2;
        }
        if (playerEquip[(int)playerNum][arrNum] == null)             // �ش� ���â�� �ƹ��͵� ������ �Ǿ����� ���� ���
        {
            playerEquip[(int)playerNum][arrNum] = armor;
            equipItemName[arrNum].text = armor.itemName;
            equipBtn[arrNum].interactable = true;
            if (armor.equipType == EquipType.���)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorHelmet = armor;
            }
            else if (armor.equipType == EquipType.��)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armor = armor;
            }
            else if (armor.equipType == EquipType.�Ź�)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorBoots = armor;
            }
            else if (armor.equipType == EquipType.��ű�)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorNecklace = armor;
            }
        }

        else if (playerEquip[(int)playerNum][arrNum].itemName.Equals(armor.itemName))      // �ش� ���â�� �ִ� ���� �����Ϸ��� ��� ���� ���
        {
            return;
        }

        else                                // �ش� ���â�� �̹� ����� ��� �ִ� ���
        {
            int x = playerInventory[(int)playerNum].IndexOf(playerEquip[(int)playerNum][arrNum]); // ���� ���� ��� �κ��丮�� �ִ��� Ȯ���ϱ� ���� �ε��� ��ȣ �Ҵ�
            if (x == -1)                           // ���� ���� ��� �κ��丮�� ���ٸ�
            {
                ItemStack(playerEquip[(int)playerNum][arrNum], (int)playerNum);
                playerEquip[(int)playerNum][arrNum] = armor;
                equipItemName[arrNum].text = armor.itemName;
            }
            else
            {                                           // ���� ���� ��� �κ��丮�� �ִٸ�
                itemCount[(int)playerNum][x]++;
                playerEquip[(int)playerNum][arrNum] = armor;
                equipItemName[arrNum].text = armor.itemName;
            }
            if (armor.equipType == EquipType.���)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorHelmet = armor;
            }
            else if (armor.equipType == EquipType.��)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armor = armor;
            }
            else if (armor.equipType == EquipType.�Ź�)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorBoots = armor;
            }
            else if (armor.equipType == EquipType.��ű�)
            {
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorNecklace = armor;
            }
        }

        if (itemCount[(int)playerNum][i] <= 1)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            poolItemQueue.Enqueue(transform.GetChild(transform.childCount - 1).gameObject);
            playerInventory[(int)playerNum].RemoveAt(i);
            itemCount[(int)playerNum].RemoveAt(i);
            ListPos.y += 25f;
            //Debug.Log("���� 1�� ���� �� ���� �� " + ListPos.y);
        }
        else
        {
            itemCount[(int)playerNum][i]--;
        }
    }

    public void ShowEquip() 
    {
        ResetEquip();
        for (int i = 0; i < playerEquip[(int)playerNum].Length; i++)
        {
            if(playerEquip[(int)playerNum][i] == null)
            {
                continue;
            }
            else
            {
                equipItemName[i].text = playerEquip[(int)playerNum][i].itemName;
                equipBtn[i].interactable = true;
            }
        }
    }

    private void ResetEquip()
    {
        for(int i = 0; i < equipItemName.Length; i++)
        {
            equipItemName[i].text = "";
            equipBtn[i].interactable = false;
        }
    }

    public void ShowItemUnequipUI()
    {
        equipItemSelectUI.gameObject.SetActive(true);
    }

    public void QuitItemUnequipUI()
    {
        itemSelectUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = actImage;
        itemSelectUI.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = actImage;
        equipItemSelectUI.gameObject.SetActive(false);
    }
    public void UnEquipItem()               // ���â�� �ִ� ��� Ŭ�� �� �����Ǵ� ���ý�Ʈ �޴����� Ż�� ��ư ������ �� ����Ǵ� �Լ�
    {
        SetUnequipUI();
        QuitItemUnequipUI();
        InventoryShow((int)playerNum);
    }

    public void SelectItemInEquip()         // ���â�� �ִ� �� ��ư�� ������ �� ����Ǵ� �Լ�
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        Vector3 point = Input.mousePosition;
        equipType = (EquipType)System.Enum.Parse(typeof(EquipType), Click.transform.tag);
        equipItemSelectUI.transform.position = new Vector3(point.x + 95f, point.y - 53.5f, 0f);
        ShowItemUnequipUI();
    }
    private void SetUnequipUI()
    {
        int num = 0;
        if (playerNum == PlayerNum.Player0)
        {
            num = 0;
        }
        else if (playerNum == PlayerNum.Player1)
        {
            num = 1;
        }
        else if (playerNum == PlayerNum.Player2)
        {
            num = 2;
        }
        switch (equipType)
        {
            case EquipType.����:
                FindInInventory(0);
                equipItemName[0].text = "";
                equipBtn[0].interactable = false;
                GameManager.instance.Players[num].GetComponent<PlayerStat>().weapon = null;
                break;
            case EquipType.���:
                FindInInventory(1);
                equipItemName[1].text = "";
                equipBtn[1].interactable = false;
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorHelmet = null;
                break;
            case EquipType.��:
                FindInInventory(2);
                equipItemName[2].text = "";
                equipBtn[2].interactable = false;
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armor = null;
                break;
            case EquipType.�Ź�:
                FindInInventory(3);
                equipItemName[3].text = "";
                equipBtn[3].interactable = false;
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorBoots = null;
                break;
            case EquipType.��ű�:
                FindInInventory(4);
                equipItemName[4].text = "";
                equipBtn[4].interactable = false;
                GameManager.instance.Players[num].GetComponent<PlayerStat>().armorNecklace = null;
                break;
        }
        ShowItemUnequipUI();
    }

    private void FindInInventory(int index)
    {
        int i = playerInventory[(int)playerNum].IndexOf(playerEquip[(int)playerNum][index]);
        if (i == -1)
        {
            ItemStack(playerEquip[(int)playerNum][index], (int)playerNum);
            playerEquip[(int)playerNum][index] = null;
        }

        else
        {
            itemCount[(int)playerNum][i]++;
            playerEquip[(int)playerNum][index] = null;
        }
    }

    public void ShowDetailUI()
    {
        for (int i = 0; i < allItemArr.EatItem.Length; i++)
        {
            if (allItemArr.EatItem[i].itemName.Equals(overItemName))
            {
                detailUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = allItemArr.EatItem[i].itemDetailImage;
                detailUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = allItemArr.EatItem[i].itemImage;
                detailUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = allItemArr.EatItem[i].itemName;
                detailUI.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = allItemArr.EatItem[i].detail_1;
                detailUI.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = allItemArr.EatItem[i].detail_2;
                //Debug.Log(detailUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.name);
                break;
            }
        }
        detailUI.gameObject.SetActive(true);
    }

    public void PlayerDefaultWeaponSet()
    {
        for(int i = 0; i < playerEquip.Count; i++)
        {
            playerEquip[i][0] = GameManager.instance.Players[i].GetComponent<PlayerStat>().weapon;
        }
    }
}