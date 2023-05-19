using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController : MonoBehaviour
{
    public static QuickSlotController instance = null;

    //������ UI
    private Image[] itemSlotImg; //������ ���� (5��)
    private Text[] itemCntTxt; //������ �ؽ�Ʈ (5��)

    [Header("������ ����")]
    public int herbCnt = 0;
    public int danceherbCnt = 0;

    [Header("������ ��������Ʈ => ���� ����")]
    public Sprite herb;
    public Sprite danceherb;
    public Sprite blank;

    [Header("������ ����")]
    public List<string> ItemList = new List<string>(); //ȹ���� ������ => ������ �̸����� ���� (string)

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        itemCntTxt = GetComponentsInChildren<Text>();
        itemSlotImg = GetComponentsInChildren<Image>();
    }
    private void Update()
    {
        ItemShow();
    }
    public void ItemStack(string Item) //������ �ֱ�
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i] == Item)
            {
                if (Item.Equals("Herb"))
                {
                    herbCnt++;
                    itemCntTxt[i].text = "" + herbCnt;
                    if (herbCnt > 1)
                    {
                        ItemList.RemoveAt(ItemList.Count - 1);
                    }
                }
                if (Item.Equals("Danceherb"))
                {
                    danceherbCnt++;
                    itemCntTxt[i].text = "" + danceherbCnt;
                    if (danceherbCnt > 1)
                    {
                        ItemList.RemoveAt(ItemList.Count - 1);
                    }
                }
            }
        }

    }
    private void ItemShow() //������ �����ֱ�
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList.Count <= 5)
            {
                if (ItemList[i] == "Herb")
                {
                    itemSlotImg[i].GetComponent<Image>().sprite = herb;
                    itemCntTxt[i].text = "" + herbCnt;
                }
                else if (ItemList[i] == "Danceherb")
                {
                    itemSlotImg[i].GetComponent<Image>().sprite = danceherb;
                    itemCntTxt[i].text = "" + danceherbCnt;
                }
            }
        }

        for (int i = 4; i > ItemList.Count - 1; i--)
        {
            itemSlotImg[i].GetComponent<Image>().sprite = blank;
            itemCntTxt[i].text = "" + 0;
        }
    }
    public void ItemUse() //������ ����ϱ�
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < itemSlotImg.Length; i++)
        {
            if (Click == itemSlotImg[i].gameObject && itemSlotImg[i].GetComponent<Image>().sprite != blank)
            {
                if (ItemList[i] == "Herb")
                {
                    herbCnt--;
                    if (herbCnt < 1)
                    {
                        ItemList.RemoveAt(i);
                    }
                }
                else if (ItemList[i] == "Danceherb")
                {
                    danceherbCnt--;
                    if (danceherbCnt < 1)
                    {
                        ItemList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
