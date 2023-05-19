using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController1 : MonoBehaviour
{
    public static InventoryController1 instance = null;
    [Header("아이템 리스트 프리팹")]
    [SerializeField] private GameObject itemlistPrebs;

    private Vector3 poolPos;
    private Vector2 ListPos; //프리팹 생성 위치

    private List<GameObject> poolItemList = new List<GameObject>();

    public List<Image> InvenImg = new List<Image>();
    public List<Text> InvenTxt = new List<Text>();

    public List<Item> itemList = new List<Item>(); //획득한 아이템 정보를 담는 리스트

    private GameObject item;
    [SerializeField] private GameObject category;

    [Header("카테고리별 리스트")]
    [SerializeField] private List<Item> categoryWeapon = new List<Item>();
    [SerializeField] private List<Item> categoryArmor = new List<Item>();
    [SerializeField] private List<Item> categoryScroll = new List<Item>();
    [SerializeField] private List<Item> categoryHerb = new List<Item>();
    [SerializeField] private List<Item> categoryETC = new List<Item>();

    public int itemlistCnt = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolPos = new Vector3(0, 0, -100);
        ListPos = new Vector3(0, 105);
    }

    public void ItemStack(Item Item) //아이템 넣기
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
        }

    }
    public void ItemListStack()
    {
        if (poolItemList.Count != 0)
        {
            item = poolItemList[0];
            poolItemList.RemoveAt(0);
        }
        else
        {
            item = Instantiate(itemlistPrebs, poolPos, Quaternion.identity);
        }

        item.transform.SetParent(this.transform);
        item.transform.localPosition = ListPos;

        InvenImg.Add(item.GetComponentInChildren<Image>());
        InvenTxt.Add(item.GetComponentInChildren<Text>());

        ListPos = new Vector2(0, ListPos.y - 25);
    }
    public void InventoryShow()
    {
        if (itemList.Count < 1) return;
        for (int i = 0; i < itemList.Count; i++)
        {
            InvenImg[i].sprite = itemList[i].itemImage;
            InvenTxt[i].text = itemList[i].itemName + " " + itemList[i].itemCount.ToString();
            InvenImg[i].transform.parent.parent.gameObject.SetActive(true);
        }
    }
    private void ItemListUse()
    {

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
            for (int i = 0; i < InvenImg.Count; i++)
            {
                InvenImg[i].transform.parent.parent.gameObject.SetActive(false);
            }
        }
        else if (category.Count < InvenImg.Count)
        {
            for (int i = InvenImg.Count - 1; i >= category.Count; i--)
            {
                InvenImg[i].transform.parent.parent.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < category.Count; i++)
        {
            InvenImg[i].sprite = category[i].itemImage;
            InvenTxt[i].text = category[i].itemName + " " + category[i].itemCount.ToString();
            InvenImg[i].transform.parent.parent.gameObject.SetActive(true);
        }
    }
}
