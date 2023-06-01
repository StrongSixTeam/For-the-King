using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 5;

    BattleManager battleManager;
    BattleOrderManager battleOrderManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battleManager.target)
        {
            //공격력 만큼 피 달게 하기
            Debug.Log("맞았다!!!");

            if (other.GetComponent<PlayerStat>() != null)
            {
                other.GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;
                Debug.Log(0);
            }
            else
            {
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage;
            }
            Invoke("BulletDestroy", 3f);
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, battleManager.target.transform.position + new Vector3(0, 1, 0), speed * Time.deltaTime);
    }
    private void BulletDestroy()
    {
        Destroy(gameObject);

        battleOrderManager.TurnChange();
    }
}
