using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // ������ �Դ� �̺�Ʈ �ӽ÷� ����� ���� ��ũ��Ʈ
    public Item[] EatItem; //���� ������
    public QuickSlotController1 quick;

    private void OnApplicationQuit()
    {
        for(int i = 0; i < EatItem.Length; i++)
        {
            EatItem[i].itemCount = 1;
        }
    }

    public void Eat() //��ư �̺�Ʈ, �Դ� �������� �������� ����
    {
        int num = Random.Range(0, EatItem.Length);

        InventoryController1.instance.ItemStack(EatItem[num]);
        quick.QuickSlotShow();
        if (InventoryController1.instance.itemlistCnt < InventoryController1.instance.itemList.Count)
        {
            InventoryController1.instance.itemlistCnt++;
        }
        InventoryController1.instance.InventoryShow();
    }
}