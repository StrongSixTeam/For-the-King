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
        Debug.Log(other.gameObject.transform.name);
        if(other.gameObject == battleManager.target)
        {
            //공격력 만큼 피 달게 하기
            Debug.Log("맞았다!!!");
            Debug.Log(battleManager.attackDamage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.localPosition += Vector3.forward * speed * Time.deltaTime;
    }
}
