using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 5;

    BattleManager battleManager;
    BattleOrderManager battleOrderManager;
    BattleLoader battleLoader;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
        battleLoader = FindObjectOfType<BattleLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battleManager.target)
        {
            //공격력 만큼 피 달게 하기
            Debug.Log("맞았다!!!");

            if (other.GetComponent<PlayerStat>() != null)
            {
                for (int i = 0; i < GameManager.instance.Players.Length; i++)
                {
                    if (other.GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[i].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;

                        float currnetHP = GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp;

                        if (currnetHP < 0)
                        {
                            GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp = 0;
                        }
                    }
                }
            }
            else
            {
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage;
                float currnetHP = other.GetComponent<EnemyStat>().nowHp;

                if (currnetHP < 0)
                {
                    other.GetComponent<EnemyStat>().nowHp = 0;
                    //에너미 리스트 관리 추가 
                }
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
