using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMapPooling : MonoBehaviour
{
    private int speed;

    private BoxCollider col;
    private Vector3 targetPos;

    private void Awake()
    {
        TryGetComponent(out col);
    }
    private void Start()
    {
        speed = 10;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MapMove_co());
        }
    }
    IEnumerator MapMove_co()
    {
        targetPos = transform.localPosition + new Vector3(0, 0, col.size.z);
        while(Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 0.01f);
            yield return null;
        }
        transform.localPosition = targetPos;

        if (transform.localPosition.z > col.size.z * 3)
        {
            transform.localPosition = new Vector3(0, 0, -col.size.z);
        }
        yield break;
    }
}
