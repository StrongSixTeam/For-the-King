using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEffect : MonoBehaviour
{
    float time;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.4f) //특정 위치에서 원점으로 이동
        {
            this.transform.localPosition = new Vector3(0, 12 - 30 * time, 0);
        }
        else if (time < 0.5f) // 튕기고
        {
            this.transform.localPosition = new Vector3(0, time - 0.4f, 0) * 4;
        }
        else if (time < 0.6f) //다시 제자리로
        {
            this.transform.localPosition = new Vector3(0, 0.6f - time, 0) * 4;
        }
        else if (time < 0.7f) //튕기고
        {
            this.transform.localPosition = new Vector3(0, (time - 0.6f) / 2, 0) * 4;
        }
        else if (time < 0.8f) //다시 제자리
        {
            this.transform.localPosition = new Vector3(0, 0.05f - (time - 0.7f) / 2, 0) * 4;
        }
        else
        {
            this.transform.localPosition = Vector3.zero;
        }

        time += Time.deltaTime;

    }
    public void resetAnim()
    {
        time = 0;
    }
}
