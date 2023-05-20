using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassNameBtn : MonoBehaviour
{
    [Header("�������� > ��ɲ� > ����")]
    [SerializeField] private GameObject[] playerPrefs;

    private Text className;

    private string class0 = "��������";
    private string class1 = "��ɲ�";
    private string class2 = "����";

    private int num = 0; //���� ���� ���� ��

    private void Awake()
    {
        className = GetComponentInChildren<Text>();
    }

    public void RightArrow()
    {
        num++;

        if (num > 2)
        {
            num = 0;
        }

        switch (num)
        {
            case 0:
                className.text = class0;
                break;
            case 1:
                className.text = class1;
                break;
            case 2:
                className.text = class2;
                break;
        }

        if (num - 1 >= 0)
        {
            playerPrefs[num - 1].SetActive(false);
        }
        else
        {
            playerPrefs[2].SetActive(false);
        }

        playerPrefs[num].SetActive(true);
    }
    public void LeftArrow()
    {
        num--;
        if (num < 0)
        {
            num = 2;
        }

        switch (num)
        {
            case 0:
                className.text = class0;
                break;
            case 1:
                className.text = class1;
                break;
            case 2:
                className.text = class2;
                break;
        }

        if (num + 1 < 3)
        {
            playerPrefs[num + 1].SetActive(false);
        }
        else
        {
            playerPrefs[0].SetActive(false);
        }

        playerPrefs[num].SetActive(true);
    }
}
