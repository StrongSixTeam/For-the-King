using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // ������ �Դ� �̺�Ʈ �ӽ÷� ����� ���� ��ũ��Ʈ
    public Item[] EatItem; //���� ������
    public QuickSlotController1[] quick;

    public int itemTurn = 0;

    public void Eat() //��ư �̺�Ʈ, �Դ� �������� �������� ����
    {
        Item item = EatItem[Random.Range(0, EatItem.Length)];

        InventoryController1.instance.ItemStack(item, (int)InventoryController1.instance.playerNum);
        quick[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
    public Item Stack()
    {
        Item item = EatItem[Random.Range(0, EatItem.Length)];

        return item;
    }
    public void Get(Item item)
    {
        //InventoryController1.instance.playerNum = (PlayerNum)System.Enum.Parse(typeof(PlayerNum), string.Format("Player{0}", itemTurn));

        InventoryController1.instance.ItemStack(item, (int)InventoryController1.instance.playerNum);
        quick[(int)InventoryController1.instance.playerNum].QuickSlotShow((int)InventoryController1.instance.playerNum);
        InventoryController1.instance.InventoryShow((int)InventoryController1.instance.playerNum);
    }
}