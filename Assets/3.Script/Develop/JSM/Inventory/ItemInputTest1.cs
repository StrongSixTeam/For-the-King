using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputTest1 : MonoBehaviour
{
    // ������ �Դ� �̺�Ʈ �ӽ÷� ����� ���� ��ũ��Ʈ
    public Item[] EatItem; //���� ������

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

        QuickSlotController1.instance.ItemStack(EatItem[num]);
        QuickSlotController1.instance.ItemShow();
    }
}