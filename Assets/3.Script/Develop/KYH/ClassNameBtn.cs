using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassNameBtn : MonoBehaviour
{
    [SerializeField] Text className;

    private string class0 = "��������";
    private string class1 = "��ɲ�";
    private string class2 = "����";

    private int num = 1; //���� ���� ���� ��
    
    public void RightArrow()
    {
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
        num++;
        if(num > 2)
        {
            num = 0;
        }
    }
    public void LeftArrow()
    {
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
        num--;
        if (num < 0)
        {
            num = 2;
        }
    }
}
