using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private Vector3 target;

    private Text damageTxt;

    private bool isCo = false;

    private void Awake()
    {
        TryGetComponent(out damageTxt);
    }
    private void Start()
    {
        target = transform.position + new Vector3(0, 50, 0);
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 1f * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) < 10 && !isCo)
        {
            StartCoroutine(ColorChange_co());
        }
    }

    IEnumerator ColorChange_co()
    {
        isCo = true;

        Color c = damageTxt.color;
        c.a -= 0.05f;
        damageTxt.color = c;

        if (c.a < 0.01f)
        {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.01f);

        isCo = false;
    }
}
