using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController1 : MonoBehaviour
{
    public static QuickSlotController1 instance = null;

    [Header("퀵슬롯 오브젝트")]
    public Image[] itemSlotImg; //아이템 슬롯 (5개)
    public Text[] itemCntTxt; //아이템 텍스트 (5개)

    [Header("아이템 스프라이트")]
    public Sprite blank;

    public List<Item> ItemList = new List<Item>(); //획득한 아이템 정보 => 아이템 이름으로 받음 (string)

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
    }
    /*private void Update()
    {
        ItemShow();
    }*/
    public void ItemStack(Item Item) //아이템 넣기
    {
        int i = ItemList.IndexOf(Item);
        if (i != -1)
        {
            ++ItemList[i].itemCount;
            if (ItemList[i].itemCount < 1)
            {
                ItemList.RemoveAt(i);
            }
        }
        else
        {
            ItemList.Add(Item);
        }

    }
    public void ItemShow() //아이템 보여주기
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = ItemList[i].itemImage;
            itemCntTxt[i].text = ItemList[i].itemCount.ToString();

            Debug.Log(ItemList[i].itemName);
        }
    }
    public void ItemUse() //아이템 사용하기
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < itemSlotImg.Length; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                ItemList[i].itemCount--;
                itemCntTxt[i].text = "" + ItemList[i].itemCount;
                if (ItemList[i].itemCount < 1)
                {
                    ItemList.RemoveAt(i);
                }

            }
        }
    }
}