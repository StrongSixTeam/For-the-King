using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // ������ �Դ� �̺�Ʈ �ӽ÷� ����� ���� ��ũ��Ʈ
    public Item[] EatItem; //���� ������
    public QuickSlotController1[] quick;

    public void Eat() //��ư �̺�Ʈ, �Դ� �������� �������� ����
    {
        Item item = EatItem[Random.Range(0, EatItem.Length)];

        InventoryController1.instance.ItemStack(item, (int)InventoryController1.instance.playerNum);
        quick[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}