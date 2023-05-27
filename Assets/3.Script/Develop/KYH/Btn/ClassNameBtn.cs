using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassNameBtn : MonoBehaviour
{
    [Header("대장장이 > 사냥꾼 > 학자")]
    [SerializeField] private GameObject[] playerPrefs;

    private Text className;

    private string class0 = "대장장이";
    private string class1 = "사냥꾼";
    private string class2 = "학자";

    private int num = 0; //순서 세기 위한 값

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
