using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNumSetPos : MonoBehaviour
{

    public Vector3 setPos = Vector3.zero;


    private void OnEnable()
    {
        transform.position = setPos;
    }


}
