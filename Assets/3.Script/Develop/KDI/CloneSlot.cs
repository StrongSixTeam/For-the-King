using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSlot : MonoBehaviour
{
    private bool isSuccess = false;
    public Sprite[] move; //�̵� UI �̹���
    public Sprite[] attackBlackSmith; //���� UI �̹��� - ��������
    public Sprite[] attackHunter; //���� UI �̹��� - ��ɲ�
    public Sprite[] attackScholar; //���� UI �̹��� - ����
    public Sprite empty; //�� �̹���
    public bool isShowText = true;

    public void Initialized()
    {
        isSuccess = false;
        SlotController.instance.success = 0;
        SlotController.instance.fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //��� ������Ʈ ����
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(6).gameObject.SetActive(false); //�ؽ�Ʈ ����

        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //��ü ����â ���� ���缭 Ű��
            if (SlotController.instance.type == SlotController.Type.move)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }

                if (isShowText)
                {
                    transform.GetChild(6).gameObject.SetActive(true); //�۾� �����ֱ�
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackScholar)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackHunter)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackHunter[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackHunter[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackBlackSmith)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackBlackSmith[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackBlackSmith[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.empty)
            {
                transform.GetChild(6).gameObject.SetActive(false); //�ؽ�Ʈ ����
                for (int j = 0; j < 3; j++) //show focus
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = empty;
                    if (j == 1)
                    {
                        transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
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
        if (SlotController.instance.limit <= SlotController.instance.success)
        {
            //���� ó��
            isSuccess = true;
        }
        else
        {
            isSuccess = false;
            //���� ���Ƽ ó��
        }
        Invoke("OffAll", 1f); //����
    }

    private void OffAll()
    {
        FindObjectOfType<EncounterManager>().ExitButton();
    }
}
