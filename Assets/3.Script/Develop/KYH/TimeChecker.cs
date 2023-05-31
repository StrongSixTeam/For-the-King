using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] private bool Day;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1시간 지났어요");

        if (!collision.GetComponent<TimeBarScrolling>().isDay && Day)
        {
            Debug.Log("지금은 밤이에요");
            Day = false;
        }
        if (collision.GetComponent<TimeBarScrolling>().isDay && !Day)
        {
            Debug.Log("지금은 낮이에요");
            Day = true;
        }
    }
}
