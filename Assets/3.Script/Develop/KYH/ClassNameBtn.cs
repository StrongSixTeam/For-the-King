using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassNameBtn : MonoBehaviour
{
    [SerializeField] Text className;

    private string class0 = "대장장이";
    private string class1 = "사냥꾼";
    private string class2 = "학자";

    private int num = 1; //순서 세기 위한 값
    
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
