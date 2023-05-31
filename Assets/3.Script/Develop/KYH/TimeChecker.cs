using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] private bool Day;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1�ð� �������");

        if (!collision.GetComponent<TimeBarScrolling>().isDay && Day)
        {
            Debug.Log("������ ���̿���");
            Day = false;
        }
        if (collision.GetComponent<TimeBarScrolling>().isDay && !Day)
        {
            Debug.Log("������ ���̿���");
            Day = true;
        }
    }
}
