using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] private bool Day;
    [SerializeField] private MapObjectCreator mapObjectCreator;

    private void Start()
    {
        StartCoroutine(SetMapObjectCreatorCo());
    }

    IEnumerator SetMapObjectCreatorCo()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        mapObjectCreator = FindObjectOfType<MapObjectCreator>();
        mapObjectCreator.timeMonsterSpawn(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1�ð� �������");

        if (!collision.GetComponent<TimeBarScrolling>().isDay && Day)
        {
            Debug.Log("������ ���̿���");
            Day = false;
            mapObjectCreator.timeMonsterSpawn(Day);
        }
        if (collision.GetComponent<TimeBarScrolling>().isDay && !Day)
        {
            Debug.Log("������ ���̿���");
            Day = true;
            mapObjectCreator.timeMonsterSpawn(Day);
        }
    }
}
