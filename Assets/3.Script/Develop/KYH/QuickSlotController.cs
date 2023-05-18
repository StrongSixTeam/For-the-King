using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController : MonoBehaviour
{
    public static QuickSlotController instance = null;

    //퀵슬롯 UI
    private Image[] itemSlotImg; //아이템 슬롯 (5개)
    private Text[] itemCntTxt; //아이템 텍스트 (5개)

    [Header("아이템 갯수")]
    public int herbCnt = 0;
    public int danceherbCnt = 0;

    [Header("아이템 스프라이트 => 직접 설정")]
    public Sprite herb;
    public Sprite danceherb;
    public Sprite blank;

    [Header("아이템 정보")]
    public List<string> ItemList = new List<string>(); //획득한 아이템 => 아이템 이름으로 받음 (string)

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
    public void ItemStack(string Item) //아이템 넣기
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
    private void ItemShow() //아이템 보여주기
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
    public void ItemUse() //아이템 사용하기
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
