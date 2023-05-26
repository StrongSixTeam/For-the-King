using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSlot : MonoBehaviour
{
    public Sprite[] move; //�̵� UI �̹���
    public Sprite[] attackBlackSmith; //���� UI �̹��� - ��������
    public Sprite[] attackHunter; //���� UI �̹��� - ��ɲ�
    public Sprite[] attackScholar; //���� UI �̹��� - ����
    public void Initialized()
    {
        SlotController.instance.success = 0;
        SlotController.instance.fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //��� ������Ʈ ����
            }
        }

        transform.GetChild(6).gameObject.SetActive(false); //�ؽ�Ʈ ����

        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //��ü ����â ���� ���缭 Ű��
            if (SlotController.instance.type == SlotController.Type.move)
            {
                for (int j = 0; j < 3; j++)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j]; //�̹��� �̵� �̹����� �ٲٱ�
                    if (j == 0)
                    {
                        transform.GetChild(i).GetChild(0).gameObject.SetActive(true); //�⺻ �̵� �̹��� �����ֱ�
                    }
                }
                transform.GetChild(6).gameObject.SetActive(true); //�۾� �����ֱ�
            }
            else if (SlotController.instance.type == SlotController.Type.attackScholar)
            {
                for (int j = 0; j < 3; j++)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                    if (j == 0)
                    {
                        transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Try()
    {
        StartCoroutine(TryCo());
    }
    IEnumerator TryCo() //���� �����ִ� ��
    {
        SlotController.instance.isSlot = true;
        int a = SlotController.instance.fixCount;
        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            yield return new WaitForSeconds(0.4f);
            if (SlotController.instance.fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //���� �̵�ĭ
                SlotController.instance.fixCount--;
                SlotController.instance.success++;
                continue;
            }
            else
            {
                int j = Random.Range(0, 100);
                if (j < SlotController.instance.percent)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    SlotController.instance.success++;
                }
                else
                {
                    transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    SlotController.instance.fail++;
                }
            }
        }
        SlotController.instance.fixCount = a;
        SlotController.instance.isSlot = false;
    }
}
