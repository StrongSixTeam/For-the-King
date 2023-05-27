using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 10;

    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == battleManager.target)
        {
            //���ݷ� ��ŭ �� �ް� �ϱ�
            Debug.Log("�¾Ҵ�!!!");
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
