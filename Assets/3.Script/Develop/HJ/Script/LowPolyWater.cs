using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPolyWater : MonoBehaviour
{
    public bool down = true;

    float angleValue = 0;
    float speed = 2;

    float time=0;

    //노드의 x와 z값에따라 y값을 조절해주면 되것다!!!
    //아래 코드 좀 배껴서 구현해보자

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= 5)
        {
            switch (down)
            {
                case true:
                    down = false;
                    time = 0;
                    break;

                case false:
                    down = true;
                    time = 0;
                    break;
            }
        }

        switch (down)
        {
            case true:
                gameObject.transform.Rotate(angleValue + Time.deltaTime, 0.0f, 0.0f, Space.Self);
                break;
            case false:
                gameObject.transform.Rotate(angleValue - Time.deltaTime, 0.0f, 0.0f, Space.Self);
                break;
        }
    }

}
