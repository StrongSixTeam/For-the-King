using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 5;

    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battleManager.target)
        {
            //���ݷ� ��ŭ �� �ް� �ϱ�
            Debug.Log("�¾Ҵ�!!!");

            if (other.GetComponent<PlayerStat>() != null)
            {
                other.GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;
            }
            else
            {
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage;
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.localPosition += Vector3.forward * speed * Time.deltaTime;
    }
}
