using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController1 : MonoBehaviour
{
    public static QuickSlotController1 instance = null;

    [Header("������ ������Ʈ")]
    public Image[] itemSlotImg; //������ ���� (5��)
    public Text[] itemCntTxt; //������ �ؽ�Ʈ (5��)

    [Header("������ ��������Ʈ")]
    public Sprite blank;

    public List<Item> ItemList = new List<Item>(); //ȹ���� ������ ���� => ������ �̸����� ���� (string)

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
    public void ItemStack(Item Item) //������ �ֱ�
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
    public void ItemShow() //������ �����ֱ�
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = ItemList[i].itemImage;
            itemCntTxt[i].text = ItemList[i].itemCount.ToString();

            Debug.Log(ItemList[i].itemName);
        }
    }
    public void ItemUse() //������ ����ϱ�
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