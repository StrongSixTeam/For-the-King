using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private List<GameObject> poolItemList = new List<GameObject>();

    //public List<Image> InvenImg = new List<Image>();
    //public List<Text> InvenTxt = new List<Text>();

    public List<Item> itemList = new List<Item>(); //ȹ���� ������ ������ ��� ����Ʈ
    public int itemlistCnt = 0;

    private GameObject item;
    [SerializeField] private GameObject category;

    [Header("ī�װ��� ����Ʈ")]
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

    [Header("�κ��丮 ���â")]
    public Item[] equipArr = new Item[5];
    public Text[] equipItemName = new Text[5];
    public string itemName = "";

    private EquipType equipType;
    [SerializeField] Button[] equipBtn;

    [Header("������")]
    [SerializeField] QuickSlotController1 quickSlot;


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
        
    }

    private void Start()
    {
        poolPos = new Vector3(0, 0, -100);
        topListPos = new Vector3(0, 105);
        ListPos = topListPos;
    }

    public void ItemStack(Item Item) //������ �ֱ�
    {
        int i = itemList.IndexOf(Item);
        if (i != -1)
        {
            ++itemList[i].itemCount;
            if (itemList[i].itemCount < 1)
            {
                itemList.RemoveAt(i);
            }
        }
        else
        {
            itemList.Add(Item);
            ItemListStack(Item);
        }

    }
    public void ItemListStack(Item putitem)
    {
        if (poolItemList.Count != 0)
        {
            item = poolItemList[0];
            poolItemList.RemoveAt(0);
        }
        else
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
            switch (putitem.itemType)
            {
                case ItemType.����:
                    Weapon weapon = putitem as Weapon;
                    item.transform.tag = weapon.equipType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.��:
                    Armor armor = putitem as Armor;
                    armor = putitem as Armor;
                    item.transform.tag = armor.equipType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.���:
                    item.transform.tag = putitem.itemType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;
                case ItemType.��ũ��:
                    item.transform.tag = putitem.itemType.ToString();
                    item.transform.GetChild(0).GetComponent<Button>().tag = putitem.itemType.ToString();
                    break;

                default:
                    break;
            }
        }

        item.transform.SetParent(this.transform);
        item.transform.localPosition = ListPos;
        ListPos.y -= 25f;
        //Debug.Log(ListPos.y);

        item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = putitem.itemImage;
        item.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = putitem.itemName;

    }
    public void InventoryShow()
    {
        if (itemList.Count < 1) return;

        ListPos = topListPos;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].GetType().Name.Equals("Weapon"))
            {
                Weapon weapon = itemList[i] as Weapon;
                transform.GetChild(i).tag = weapon.equipType.ToString();
            }
            else if (itemList[i].GetType().Name.Equals("Armor"))
            {
                Armor armor = itemList[i] as Armor;
                transform.GetChild(i).tag = armor.equipType.ToString();
            }
            else if (itemList[i].GetType().Name.Equals("Used"))
            {
                transform.GetChild(i).tag = itemList[i].itemType.ToString();
            }
            transform.GetChild(i).GetChild(0).tag = itemList[i].itemType.ToString();
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().sprite = itemList[i].itemImage;
            transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<Text>().text = itemList[i].itemName + " " + itemList[i].itemCount.ToString();
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
            case ItemType.����:
                CategoryActive(categoryWeapon);
                break;

            case ItemType.��:
                CategoryActive(categoryArmor);
                break;

            case ItemType.��ũ��:
                CategoryActive(categoryScroll);
                break;

            case ItemType.���:
                CategoryActive(categoryHerb);
                break;

            case ItemType.��Ÿ:
                CategoryActive(categoryETC);
                break;

            case ItemType.ALL:
                InventoryShow();
                break;
        }

    }

    private void SetCategory()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            switch (itemList[i].itemType)
            {
                case ItemType.����:
                    categoryWeapon.Add(itemList[i]);
                    break;
                case ItemType.��:
                    categoryArmor.Add(itemList[i]);
                    break;
                case ItemType.��ũ��:
                    categoryScroll.Add(itemList[i]);
                    break;
                case ItemType.���:
                    categoryHerb.Add(itemList[i]);
                    break;
                case ItemType.��Ÿ:
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
            transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<Text>().text = category[i].itemName + " " + category[i].itemCount.ToString();
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
        string[] itemNameArr = Click.transform.GetChild(2).GetComponent<Text>().text.Split(' ');
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
        Debug.Log(itemName);
    }


    public void EquipItem()
    {
        //Debug.Log("���� ��ư ��������");
        //Debug.Log(itemName);
        Equip(itemName);
        QuitItemSelectUI();
        InventoryReset();
        InventoryShow();
    }

    public void UseItem()
    {
        ItemListUse();
        QuitItemSelectUI();
        InventoryReset();
        InventoryShow();
        quickSlot.QuickSlotShow();
    }

    private void ItemListUse()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName.Equals(itemName))
            {
                switch (itemList[i].itemType)
                {
                    case ItemType.���:
                        if(itemList[i].itemCount == 1)
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

                    case ItemType.��ũ��:
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

                    case ItemType.��Ÿ:
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
            //Debug.Log(i.ToString() + "��° ����");
            //Debug.Log(itemList[i].itemName.Equals(ItemName));

            if (itemList[i].itemName.Equals(ItemName))
            {
                switch (itemList[i].itemType)
                {
                    case ItemType.����:
                        weapon = itemList[i] as Weapon;
                        UpdateEquip(weapon, i);
                        break;
                    case ItemType.��:
                        armor = itemList[i] as Armor;
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
        if (equipArr[0] == null)             // �ش� ���â�� �ƹ��͵� ������ �Ǿ����� ���� ���
        {
            equipArr[0] = weapon;
            equipItemName[0].text = equipArr[0].itemName;
            equipBtn[0].interactable = true;
            if (itemList[i].itemCount == 1)
            {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                itemList.RemoveAt(i);
                ListPos.y += 25f;
                // Debug.Log("���� 1�� ���� �� ���� �� " + ListPos.y);
            }
            else
            {
                itemList[i].itemCount--;
            }
        }

        else if (equipArr[0].itemName.Equals(weapon.itemName))      // �ش� ���â�� �ִ� ���� �����Ϸ��� ��� ���� ���
        {
            return;
        }

        else                                // �ش� ���â�� �̹� ����� ��� �ִ� ���
        {
            int x = itemList.IndexOf(equipArr[0]); // ���� ���� ��� �κ��丮�� �ִ��� Ȯ���ϱ� ���� �ε��� ��ȣ �Ҵ�
            if (x == -1)                           // ���� ���� ��� �κ��丮�� ���ٸ�
            {
                itemList.Add(equipArr[0]);
                equipArr[0] = weapon;
                equipItemName[0].text = equipArr[0].itemName;
            }
            else
            {
                itemList[x].itemCount++;
                equipArr[0] = weapon;
                equipItemName[0].text = equipArr[0].itemName;
            }
        }
    }

    private void UpdateEquip(Armor armor, int i, int arrNum)
    {
        if (equipArr[arrNum] == null)             // �ش� ���â�� �ƹ��͵� ������ �Ǿ����� ���� ���
        {
            equipArr[arrNum] = armor;
            equipItemName[arrNum].text = equipArr[arrNum].itemName;
            equipBtn[arrNum].interactable = true;
            if (itemList[i].itemCount == 1)
            {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
                itemList.RemoveAt(i);
                ListPos.y += 25f;
                //Debug.Log("���� 1�� ���� �� ���� �� " + ListPos.y);
            }
            else
            {
                itemList[i].itemCount--;
            }
        }

        else if (equipArr[arrNum].itemName.Equals(armor.itemName))      // �ش� ���â�� �ִ� ���� �����Ϸ��� ��� ���� ���
        {
            return;
        }

        else                                // �ش� ���â�� �̹� ����� ��� �ִ� ���
        {
            int x = itemList.IndexOf(equipArr[arrNum]); // ���� ���� ��� �κ��丮�� �ִ��� Ȯ���ϱ� ���� �ε��� ��ȣ �Ҵ�
            if (x == -1)                           // ���� ���� ��� �κ��丮�� ���ٸ�
            {
                itemList.Add(equipArr[arrNum]);
                equipArr[arrNum] = armor;
                equipItemName[arrNum].text = equipArr[arrNum].itemName;
            }
            else
            {
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
    public void UnEquipItem()               // ���â�� �ִ� ��� Ŭ�� �� �����Ǵ� ���ý�Ʈ �޴����� Ż�� ��ư ������ �� ����Ǵ� �Լ�
    {
        SetUnequipUI();
        QuitItemUnequipUI();
        InventoryReset();
        InventoryShow();
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
        switch (equipType)
        {
            case EquipType.����:
                FindInInventory(0);
                equipItemName[0].text = "";
                equipBtn[0].interactable = false;
                break;
            case EquipType.���:
                FindInInventory(1);
                equipItemName[1].text = "";
                equipBtn[1].interactable = false;
                break;
            case EquipType.��:
                FindInInventory(2);
                equipItemName[2].text = "";
                equipBtn[2].interactable = false;
                break;
            case EquipType.�Ź�:
                FindInInventory(3);
                equipItemName[3].text = "";
                equipBtn[3].interactable = false;
                break;
            case EquipType.��ű�:
                FindInInventory(4);
                equipItemName[4].text = "";
                equipBtn[4].interactable = false;
                break;
        }
        ShowItemUnequipUI();
    }

    private void FindInInventory(int index)
    {
        int i = itemList.IndexOf(equipArr[index]);
        if (i == -1)
        {
            ItemListStack(equipArr[index]);
            itemList.Add(equipArr[index]);
            equipArr[index] = null;
        }

        else
        {
            itemList[i].itemCount++;
            equipArr[index] = null;
        }
    }

    public void ShowDetailUI()
    {
        for (int i = 0; i < instance.itemList.Count; i++)
        {
            if (itemList[i].itemName.Equals(itemName))
            {
                detailUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = itemList[i].itemDetailImage;
                detailUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = itemList[i].itemImage;
                detailUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = itemList[i].itemName;
                detailUI.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = itemList[i].detail_1;
                detailUI.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = itemList[i].detail_2;
                Debug.Log(detailUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.name);
                break;
            }
        }
        detailUI.gameObject.SetActive(true);
    }
}