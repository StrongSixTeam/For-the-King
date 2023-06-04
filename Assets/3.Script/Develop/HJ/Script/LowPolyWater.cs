using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPolyWater : MonoBehaviour
{
    public bool down = true;

    float angleValue = 0;
    float speed = 2;

    float time=0;

    //����� x�� z�������� y���� �������ָ� �ǰʹ�!!!
    //�Ʒ� �ڵ� �� �貸�� �����غ���

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
