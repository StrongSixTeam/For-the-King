using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessCalc : MonoBehaviour
{
    private Formula formula;

    [SerializeField] private int testMax = 3;
    [SerializeField] private int testPercent = 86;
    [SerializeField] private int testSuccess = 3;
    [SerializeField] private int testFix = 0;

    private void Start()
    {
        formula = FindObjectOfType<Formula>();
        Calculate(testMax, testPercent, testSuccess);
        //슬롯의 칸, 슬롯 하나의 성공 확률, 슬롯 칸중에 몇개의 확률을 통과해야 하는지
    }

    private void Calculate(int maxSlot, int percent, int successLimit)
    {
        SlotController.instance.maxSlotCount = maxSlot;
        SlotController.instance.type = SlotController.Type.attackScholar;
        SlotController.instance.fixCount = testFix;
        Debug.Log("=============");
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
        SlotController.instance.percent = percent;
        maxSlot = maxSlot - testFix;

        for (int i = maxSlot; i >= 0; i--)
        {
            float successPercent = percent * (float)0.01;
            float failPercent = (100 - percent) * (float)0.01;
            float result = Mathf.Pow(failPercent, maxSlot - i) * Mathf.Pow(successPercent, maxSlot - (maxSlot - i)) * formula.Combination(maxSlot, i) * 100;
            result = Mathf.Round(result);
            if (maxSlot <= 0)
            {
                result = 100;
            }
            if (i >= successLimit - testFix)
            {
                Debug.Log(i + testFix + " " + result + "% 성공");
            }
            else
            {
                Debug.Log(i + testFix+ " " + result + "% 실패");
            }
        }
        for (int i = testFix-1; i >= 0; i--)
        {
            if (i >= successLimit)
            {
                Debug.Log(i + " 0% 성공");
            }
            else
            {
                Debug.Log(i + " 0% 실패");
            }
        }
        Debug.Log("=============");
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
