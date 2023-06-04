using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCheck : MonoBehaviour
{
    public void Ready()
    {
        gameObject.SetActive(false);
        FindObjectOfType<ReadyCnt>().readyCnt++;
    }
}
