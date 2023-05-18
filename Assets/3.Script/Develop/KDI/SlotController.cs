using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public static SlotController instance = null;

    public Sprite[] move; //�̵� UI �̹���
    public Sprite[] attackScholar; //���� UI �̹��� - ����
    public Sprite[] attack; //��

    [Header("���⼭ ��ġ�� ���� ���� + �ٸ� ��ũ��Ʈ������ ���� ����")]
    public int maxSlotCount; //������ ���� ����
    public int fixCount; //���߷� ��������� 
    public int success = 0; //���� �
    public int fail = 0; //���� �
    public int percent = 40; //Ȯ�� ����

    private void Awake()
    {
        instance = this;
    }
    public enum Type
    {
        move,
        attackScholar,
        attackBlackSmith,
        attackHunter
    }
    public Type type;

    private void Initialized() //�ʱ�ȭ
    {
        success = 0;
        fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //��� ������Ʈ ����
            }
        }
        transform.GetChild(6).gameObject.SetActive(false);

        for (int i = 0; i < maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //��ü ����â ���� ���缭 Ű��
            if (type == Type.move)
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
            else if (type == Type.attackScholar)
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

    public void OnClick()
    {
        StopAllCoroutines(); //�̹� �������̸� ���߰����
        Initialized();
        StartCoroutine(MakeMove());
    }

    IEnumerator MakeMove() //���� �����ִ� ��(only)
    {
        for (int i = 0; i < maxSlotCount; i++)
        {
            transform.GetChild(6).GetComponent<Text>().text = "�̵� ���� : " + success;
            yield return new WaitForSeconds(0.4f);
            if (fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //���� �̵�ĭ
                fixCount--;
                success++;
                continue;
            }
            else
            {
                int j = Random.Range(0, 100);
                if (j < percent)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    success++;
                }
                else
                {
                    transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    fail++;
                }
            }
        }
            transform.GetChild(6).GetComponent<Text>().text = "�̵� ���� : " + success;
    }

}
