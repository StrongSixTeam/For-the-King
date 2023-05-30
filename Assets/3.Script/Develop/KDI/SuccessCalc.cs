using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessCalc : MonoBehaviour
{
    private Formula formula;

    [SerializeField] private int testMax = 3;
    [SerializeField] private int testPercent = 86;
    [SerializeField] private int testSuccess = 3;
    [SerializeField] private int testFix = 0;

    [SerializeField] private GameObject[] resultNumbers;

    private void Start()
    {
        formula = FindObjectOfType<Formula>();
        //������ ĭ, ���� �ϳ��� ���� Ȯ��, ���� ĭ�߿� ��� Ȯ���� ����ؾ� �ϴ���
    }

    public void Calculate(int maxSlot, int percent, int successLimit)
    {
        formula = FindObjectOfType<Formula>();
        //SlotController.instance.maxSlotCount = maxSlot;
        //SlotController.instance.type = SlotController.Type.attackScholar;
        testFix = SlotController.instance.fixCount;
        if (testFix == 1)
        {
            percent += 10;
        }
        else if (testFix == 2)
        {
            percent += 15;
        }
        else if (testFix == 3)
        {
            percent += 18;
        }
        //SlotController.instance.percent = percent;
        //maxSlot = maxSlot - testFix;
        if (maxSlot != 4)
        {
            for (int i = 4; i > maxSlot; i--)
            {
                resultNumbers[i].SetActive(false);
            }
        }
        for (int i = maxSlot; i >= 0; i--)
        {
            float successPercent = percent * (float)0.01;
            float failPercent = (100 - percent) * (float)0.01;
            float result = Mathf.Pow(failPercent, maxSlot - i) * Mathf.Pow(successPercent, maxSlot - (maxSlot - i)) * formula.Combination(maxSlot, i) * 100;
            result = Mathf.Round(result);
            if (maxSlot == testFix) //���� ũ�� == ���߷� ��� �϶�
            {
                result = 100;
            }

            if (i >= successLimit)
            {
                resultNumbers[i].SetActive(true);
                resultNumbers[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = "(" + result + "%) ����";
                //Debug.Log(i + testFix + " " + result + "% ����");
            }
            else
            {
                resultNumbers[i].SetActive(true);
                resultNumbers[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = "(" + result + "%) ����";
                //Debug.Log(i + testFix+ " " + result + "% ����");
            }
        }
        for (int i = testFix-1; i >= 0; i--)
        {
            if (i >= successLimit)
            {
                resultNumbers[i].SetActive(true);
                resultNumbers[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = "(0%) ����";
                //Debug.Log(i + " 0% ����");
            }
            else
            {
                resultNumbers[i].SetActive(true);
                resultNumbers[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = "(0%) ����";
                //Debug.Log(i + " 0% ����");
            }
        }
        //Debug.Log("=============");
    }

    public void FixAdd()
    {
        testFix++;
        if (testFix > testMax)
        {
            testFix = testMax;  
        }
        Calculate(testMax, testPercent, testFix);
    }

    public void FixReset()
    {
        testFix = 0;
        Calculate(testMax, testPercent, testFix);
    }
}
