using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula : MonoBehaviour
{
    private int Factorial(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    public int Combination(int a, int b)
    {
        return Factorial(a) / Factorial(b) / Factorial(a - b);
    }
}
