using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum PlayerNum { Player0, Player1, Player2}
public class InventoryController1 : MonoBehaviour
{
    public static InventoryController1 instance = null;
    [Header("아이템 리스트 프리팹")]
    [SerializeField] private GameObject itemlistPrebs;
    public GameObject itemSelectUI;                     // 인벤토리 내 아이템 클릭 시 생성되는 UI
    public GameObject equipItemSelectUI;                // 장비창 내 착용 아이템 클릭 시 생성되는 UI
    public GameObject detailUI;                         // 마우스 오버 시 나타나는 UI

    private Vector3 poolPos;
    private Vector2 ListPos; //프리팹 생성 위치
    private Vector2 topListPos; //프리팹 생성 위치 최상단

    private Queue<GameObject> poolItemQueue = new Queue<GameObject>();

    //public List<Image> InvenImg = new List<Image>();
    //public List<Text> InvenTxt = new List<Text>();

    public PlayerNum playerNum;
    public List<Item> itemList = new List<Item>(); //획득한 아이템 정보를 담는 리스트
    public List<List<Item>> playerInventory = new List<List<Item>>(); // 플레이어 별 인벤토리
    public List<Item[]> playerEquip = new List<Item[]>();

    private GameObject item;
    [SerializeField] private GameObject category;

    [Header("카테고리별 리스트")]
    [SerializeField] private List<Item> categoryWeapon = new List<Item>();
    [SerializeField] private List<Item> categoryArmor = new List<Item>();
    [SerializeField] private List<Item> categoryScroll = new List<Item>();
    [SerializeField] private List<Item> categoryHerb = new List<Item>();
    [SerializeField] private List<Item> categoryETC = new List<Item>();

    public Sprite deactImage;
    public Sprite actImage;
    public Sprite hoverImage;
    public bool isEquip = false;
    public bool isUsed = false;

    [Header("인벤토리 장비창")]
    public Item[] equipArr = new Item[5];
    public Text[] equipItemName = new Text[5];
    public string itemName = "";

    private EquipType equipType;
    [SerializeField] Button[] equipBtn;

    [Header("퀵슬롯")]
    [SerializeField] QuickSlotController1 quickSlot;

    [Header("전체 아이템 담은 배열")]
    public ItemInputTest1 allItemArr;


    private void Awake()
    {
        instance = this;
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
        }
    }

    private void Start()
    {
        CreatePoolItem();
        poolPos = new Vector3(0, 0, -100);
        topListPos = new Vector3(0, 105);
        ListPos = topListPos;
    }

    private void CreatePoolItem() // 오브젝트 풀링할 리스트 초기화
    {
        for(int i = 0; i < allItemArr.EatItem.Length; i++)
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
            item.SetActive(false);
            poolItemQueue.Enqueue(item);
        }
    }

    public void ItemStack(Item Item, int playerNum) //아이템 넣기
    {
        int i = playerInventory[playerNum].IndexOf(Item);
        if (i != -1)
        {
            playerInventory[playerNum][i].itemCount++;
            if (playerInventory[playerNum][i].itemCount < 1)
            {
                Debug.Log("여긴 걸리면 안 돼.");
                playerInventory[playerNum].RemoveAt(i);
            }
        }
        else
        {
            Debug.Log("여긴 걸려야 해.");
            playerInventory[playerNum].Add(Item);
            PoolItemListStack(Item);
        }
        
    }
    /*public void ItemListStack(Item putitem)
    {
        if (poolItem.Count != 0)
        {
            item = poolItemList[0];
            poolItemList.RemoveAt(0);
        }
        else
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
            Debug.Log(this.transform.childCount);
            item.transform.SetParent(this.transform);
            Debug.Log(this.transform.childCount);
            switch (putitem.itemType)
            {
                case ItemType.무기:
                    Weapon weapon = putitem as Weapon;
                    item.transform.tag = weapon.equipType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.방어구:
                    Armor armor = putitem as Armor;
                    armor = putitem as Armor;
                    item.transform.tag = armor.equipType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.허브:
                    item.transform.tag = putitem.itemType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.스크롤:
                    item.transform.tag = putitem.itemType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;

                default:
                    break;
            }
        }

        item.transform.localPosition = ListPos;
        ListPos.y -= 25f;
        //Debug.Log(ListPos.y);

        item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = putitem.itemImage;
        item.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = putitem.itemName;

    }*/

    public void PoolItemListStack(Item putitem)         // 오브젝트 풀링 함수
    {

        GameObject newitem = poolItemQueue.Dequeue();
        //Debug.Log(this.transform.childCount);
        newitem.transform.SetParent(this.transform);
        Debug.Log(this.transform.childCount);
        switch (putitem.itemType)
        {
            case ItemType.무기:
                Weapon weapon = putitem as Weapon;
                newitem.transform.tag = weapon.equipType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.방어구:
                Armor armor = putitem as Armor;
                armor = putitem as Armor;
                newitem.transform.tag = armor.equipType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.허브:
                newitem.transform.tag = putitem.itemType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;
            case ItemType.스크롤:
                newitem.transform.tag = putitem.itemType.ToString();
                newitem.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                break;

            default:
                break;
        }

        newitem.transform.localPosition = ListPos;
        ListPos.y -= 25f;
        //Debug.Log(ListPos.y);

        newitem.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = putitem.itemImage;
        newitem.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = putitem.itemName;

    }

    public void InventoryShow(int playernum)          // 플레이어 별로 분할된 인벤토리 보여주기
    {
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
            transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>().text = playerInventory[playernum][i].itemName + " " + playerInventory[playernum][i].itemCount.ToString();
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).localPosition = ListPos;

            ListPos.y -= 25f;
        }
    }

    public void InventoryReset()
    {
        if(transform.childCount > itemList.Count)
        {
            for (int i = 0; i < itemList.Count; i++)
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
        }
        //transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
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
            case ItemType.무기:
                CategoryActive(categoryWeapon);
                break;

            case ItemType.방어구:
                CategoryActive(categoryArmor);
                break;

            case ItemType.스크롤:
                CategoryActive(categoryScroll);
                break;

            case ItemType.허브:
                CategoryActive(categoryHerb);
                break;

            case ItemType.기타:
                CategoryActive(categoryETC);
                break;

            case ItemType.ALL:
                InventoryShow((int)playerNum);
                break;
        }

    }

    private void SetCategory()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            switch (itemList[i].itemType)
            {
                case ItemType.무기:
                    categoryWeapon.Add(itemList[i]);
                    break;
                case ItemType.방어구:
                    categoryArmor.Add(itemList[i]);
                    break;
                case ItemType.스크롤:
                    categoryScroll.Add(itemList[i]);
                    break;
                case ItemType.허브:
                    categoryHerb.Add(itemList[i]);
                    break;
                case ItemType.기타:
                    categoryETC.Add(itemList[i]);
                    break;
            }
        }
    }

    private void CategoryActive(List<Item> category)
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
            transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>().text = category[i].itemName + " " + category[i].itemCount.ToString();
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

    public void SelectInvenEquipItem()              // 인벤토리에 있는 아이템 클릭 시 아이템의 이름을 받아오는 함수
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
        //Debug.Log("장착 버튼 눌렀다잉");
        //Debug.Log(itemName);
        Equip(itemName);
        QuitItemSelectUI();
        InventoryReset();
        InventoryShow((int)playerNum);
    }

    public void UseItem()
    {
        ItemListUse();
        QuitItemSelectUI();
        InventoryReset();
        InventoryShow((int)playerNum);
        quickSlot.QuickSlotShow((int)playerNum);
    }

    private void ItemListUse()
    {
        for (int i = 0; i < playerInventory[(int)playerNum].Count; i++)
        {
            if (playerInventory[(int)playerNum][i].itemName.Equals(itemName))
            {
                switch (playerInventory[(int)playerNum][i].itemType)
                {
                    case ItemType.허브:
                        if(playerInventory[(int)playerNum][i].itemCount == 1)
                        {
                            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot.quickSlot.Remove(playerInventory[(int)playerNum][i]);
                            playerInventory[(int)playerNum].RemoveAt(i);
                            for (int a = 0; a < quickSlot.itemSlotImg.Length; a++)
                            {
                                quickSlot.itemSlotImg[a].sprite = quickSlot.blank;
                                quickSlot.itemCntTxt[a].text = 0.ToString();
                            }
                        }

                        else
                        {
                            playerInventory[(int)playerNum][i].itemCount--;
                        }
                        break;

                    case ItemType.스크롤:
                        if (itemList[i].itemCount == 1)
                        {
                            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot.quickSlot.Remove(playerInventory[(int)playerNum][i]);
                            playerInventory[(int)playerNum].RemoveAt(i);
                            for (int a = 0; a < quickSlot.itemSlotImg.Length; a++)
                            {
                                quickSlot.itemSlotImg[a].sprite = quickSlot.blank;
                                quickSlot.itemCntTxt[a].text = 0.ToString();
                            }
                        }

                        else
                        {
                            playerInventory[(int)playerNum][i].itemCount--;
                        }
                        break;

                    case ItemType.기타:
                        if (itemList[i].itemCount == 1)
                        {
                            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                            quickSlot.quickSlot.Remove(itemList[i]);
                            itemList.RemoveAt(i);
                            for (int a = 0; a < quickSlot.itemSlotImg.Length; a++)
                            {
                                quickSlot.itemSlotImg[a].sprite = quickSlot.blank;
                                quickSlot.itemCntTxt[a].text = 0.ToString();
                            }
                        }

                        else
                        {
                            itemList[i].itemCount--;
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
        for (int i = 0; i < itemList.Count; i++)
        {
            //Debug.Log(i.ToString() + "번째 루프");
            //Debug.Log(itemList[i].itemName.Equals(ItemName));

            if (itemList[i].itemName.Equals(ItemName))
            {
                switch (itemList[i].itemType)
                {
                    case ItemType.무기:
                        weapon = itemList[i] as Weapon;
                        UpdateEquip(weapon, i);
                        break;
                    case ItemType.방어구:
                        armor = itemList[i] as Armor;
                        switch (armor.equipType)
                        {
                            case EquipType.헬멧:
                                UpdateEquip(armor, i, 1);
                                break;

                            case EquipType.방어구:
                                UpdateEquip(armor, i, 2);
                                break;

                            case EquipType.신발:
                                UpdateEquip(armor, i, 3);
                                break;

                            case EquipType.장신구:
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
        itemList[i].itemCount--;
        if (itemList[i].itemCount < 1)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            itemList[i].itemCount = 1;
            itemList.RemoveAt(i);
            ListPos.y += 25f;
            // Debug.Log("무기 1개 남은 거 장착 후 " + ListPos.y);
        }

        if (equipArr[0] == null)             // 해당 장비창에 아무것도 착용이 되어있지 않은 경우
        {
            equipArr[0] = weapon;
            equipItemName[0].text = equipArr[0].itemName;
            equipBtn[0].interactable = true;
            
        }

        else if (equipArr[0].itemName.Equals(weapon.itemName))      // 해당 장비창에 있는 장비와 착용하려는 장비가 같은 경우
        {
            return;
        }

        else                                // 해당 장비창에 이미 착용된 장비가 있는 경우
        {
            int x = itemList.IndexOf(equipArr[0]); // 착용 중인 장비가 인벤토리에 있는지 확인하기 위한 인덱스 번호 할당
            if (x == -1)                           // 착용 중인 장비가 인벤토리에 없다면
            {
                ItemStack(equipArr[0], (int)playerNum);
                equipArr[0] = weapon;
                equipItemName[0].text = equipArr[0].itemName;
            }
            else
            {                                       // 착용 중이던 장비가 인벤토리에 있다면
                itemList[x].itemCount++;
                equipArr[0] = weapon;
                equipItemName[0].text = equipArr[0].itemName;
            }
        }
    }

    private void UpdateEquip(Armor armor, int i, int arrNum)
    {
        itemList[i].itemCount--;
        if (itemList[i].itemCount < 1)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            itemList[i].itemCount = 1;
            itemList.RemoveAt(i);
            ListPos.y += 25f;
            //Debug.Log("무기 1개 남은 거 장착 후 " + ListPos.y);
        }
        if (equipArr[arrNum] == null)             // 해당 장비창에 아무것도 착용이 되어있지 않은 경우
        {
            equipArr[arrNum] = armor;
            equipItemName[arrNum].text = equipArr[arrNum].itemName;
            equipBtn[arrNum].interactable = true;
            
        }

        else if (equipArr[arrNum].itemName.Equals(armor.itemName))      // 해당 장비창에 있는 장비와 착용하려는 장비가 같은 경우
        {
            return;
        }

        else                                // 해당 장비창에 이미 착용된 장비가 있는 경우
        {
            int x = itemList.IndexOf(equipArr[arrNum]); // 착용 중인 장비가 인벤토리에 있는지 확인하기 위한 인덱스 번호 할당
            if (x == -1)                           // 착용 중인 장비가 인벤토리에 없다면
            {
                ItemStack(equipArr[arrNum], (int)playerNum);
                equipArr[arrNum] = armor;
                equipItemName[arrNum].text = equipArr[arrNum].itemName;
            }
            else
            {                                           // 착용 중인 장비가 인벤토리에 있다면
                itemList[x].itemCount++;
                equipArr[arrNum] = armor;
                equipItemName[arrNum].text = equipArr[arrNum].itemName;
            }
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
    public void UnEquipItem()               // 장비창에 있는 장비 클릭 시 생성되는 컨택스트 메뉴에서 탈착 버튼 눌렀을 때 실행되는 함수
    {
        SetUnequipUI();
        QuitItemUnequipUI();
        InventoryReset();
        InventoryShow((int)playerNum);
    }

    public void SelectItemInEquip()         // 장비창에 있는 각 버튼을 눌렀을 때 실행되는 함수
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;
        Vector3 point = Input.mousePosition;
        equipType = (EquipType)System.Enum.Parse(typeof(EquipType), Click.transform.tag);
        equipItemSelectUI.transform.position = new Vector3(point.x + 95f, point.y - 53.5f, 0f);
        ShowItemUnequipUI();
    }
    private void SetUnequipUI()
    {
        switch (equipType)
        {
            case EquipType.무기:
                FindInInventory(0);
                equipItemName[0].text = "";
                equipBtn[0].interactable = false;
                break;
            case EquipType.헬멧:
                FindInInventory(1);
                equipItemName[1].text = "";
                equipBtn[1].interactable = false;
                break;
            case EquipType.방어구:
                FindInInventory(2);
                equipItemName[2].text = "";
                equipBtn[2].interactable = false;
                break;
            case EquipType.신발:
                FindInInventory(3);
                equipItemName[3].text = "";
                equipBtn[3].interactable = false;
                break;
            case EquipType.장신구:
                FindInInventory(4);
                equipItemName[4].text = "";
                equipBtn[4].interactable = false;
                break;
        }
        ShowItemUnequipUI();
    }

    private void FindInInventory(int index)
    {
        int i = playerInventory[(int)playerNum].IndexOf(equipArr[index]);
        if (i == -1)
        {
            ItemStack(equipArr[index], (int)playerNum);
            equipArr[index] = null;
        }

        else
        {
            playerInventory[(int)playerNum][i].itemCount++;
            equipArr[index] = null;
        }
    }

    public void ShowDetailUI()
    {
        for (int i = 0; i < allItemArr.EatItem.Length; i++)
        {
            if (allItemArr.EatItem[i].itemName.Equals(itemName))
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
}