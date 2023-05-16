using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotController : MonoBehaviour
{
    public static QuickSlotController instance = null;

    /*
    1. ���� ������ ��� �����Կ� �̹��� ���� (��� �����۸�)
    2. ���� �������� ���� �� �ؽ�Ʈ�� ���� ����
    3. ������ ��� �Լ�
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
                Debug.Log("num ����");
                Debug.Log(num);
            }
        }
        ItemList.RemoveAt(num);
    }
}
