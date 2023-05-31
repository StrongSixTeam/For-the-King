using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarScrolling : MonoBehaviour
{
    private int speed;

    private RectTransform Pos;
    private Vector3 targetPos;

    public bool isDay;

    private void Awake()
    {
        TryGetComponent(out Pos);
    }
    public void TimeFlow() //시간이 흐를 때 부를 메소드
    {
        StartCoroutine(MapMove_co());
    }
    IEnumerator MapMove_co()
    {
        targetPos = Pos.localPosition + new Vector3(-80, 0, 0);
        while (Vector3.Distance(Pos.localPosition, targetPos) > 0.01f)
        {
            Pos.localPosition = Vector3.MoveTowards(Pos.localPosition, targetPos, 2f);
            yield return null;
        }
        Pos.localPosition = targetPos;

        if (Pos.localPosition.x <= -80)
        {
            Pos.localPosition = new Vector3(1120, 35, 0);
        }
        yield break;
    }
}
