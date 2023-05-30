using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWindmill : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 8 * Time.deltaTime); 
    }
}
