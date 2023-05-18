using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessCalc : MonoBehaviour
{
    private Formula formula;

    [SerializeField] private int testMax = 3;
    [SerializeField] private int testPercent = 86;
    [SerializeField] private int testSuccess = 3;

    private void Start()
    {
        formula = FindObjectOfType<Formula>();
        Calculate(testMax, testPercent, testSuccess);
        //½½·ÔÀÇ Ä­, ½½·Ô ÇÏ³ªÀÇ ¼º°ø È®·ü, ½½·Ô Ä­Áß¿¡ ¸î°³ÀÇ È®·üÀ» Åë°úÇØ¾ß ÇÏ´ÂÁö
    }

    private void Calculate(int maxSlot, int percent, int successLimit)
    {
        for (int i = maxSlot; i >= 0; i--)
        {
            float successPercent = percent * (float)0.01;
            float failPercent = (100 - percent) * (float)0.01;
            float result = Mathf.Pow(failPercent, maxSlot - i) * Mathf.Pow(successPercent, maxSlot - (maxSlot - i)) * formula.Combination(maxSlot, i) * 100;
            result = Mathf.Round(result);
            if (i >= successLimit)
            {
                Debug.Log(i + " " + result + " ¼º°ø");
            }
            else
            {
                Debug.Log(i + " " + result + " ½ÇÆÐ");
            }
        }
    }
}
