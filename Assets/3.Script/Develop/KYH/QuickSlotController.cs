using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController : MonoBehaviour
{
    public static QuickSlotController instance = null;

    /*
    1. 선입 선출한 대로 퀵슬롯에 이미지 띄우기 (사용 아이템만)
    2. 같은 아이템을 먹을 시 텍스트로 갯수 증가
    3. 아이템 사용 함수
    */

    public Image[] itemSlotImg;
    public GameObject[] itemSlot;

    public List<string> ItemList = new List<string>();

    public Sprite herb;
    public Sprite danceherb;
    public Sprite blank;

    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        ItemStack();
    }
    private void ItemStack()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList.Count <= 5)
            {
                if (ItemList[i] == "Herb")
                {
                    itemSlotImg[i].sprite = herb;
                }
                else if (ItemList[i] == "Danceherb")
                {
                    itemSlotImg[i].sprite = danceherb;
                }
            }
            else
            {
                return;
            }
        }
    }
    public void ItemUse()
    {
        GameObject Click = EventSystem.current.currentSelectedGameObject;

        int num = 0;

        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(Click == itemSlot[i])
            {
                num = i;
                Debug.Log("num 설정");
                Debug.Log(num);
            }
        }
        ItemList.RemoveAt(num);
    }
}
