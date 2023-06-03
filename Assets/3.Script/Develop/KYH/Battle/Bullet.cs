using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private int speed = 5;

    BattleManager battleManager;
    BattleOrderManager battleOrderManager;
    BattleLoader battleLoader;

    [SerializeField] GameObject Damage;

    private Camera CurrnetCam;

    private bool isZero = false;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
        battleLoader = FindObjectOfType<BattleLoader>();
        CurrnetCam = FindObjectOfType<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == battleManager.target)
        {
            Text txt = Instantiate(Damage, CurrnetCam.WorldToScreenPoint(other.transform.position) + new Vector3(0, 300, 0), Quaternion.identity).GetComponent<Text>();
            txt.transform.SetParent(GameObject.Find("Canvas").transform);
            txt.text = "-" + battleManager.attackDamage;

            if (other.GetComponent<PlayerStat>() != null)
            {
                other.GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;

                for (int i = 0; i < GameManager.instance.Players.Length; i++)
                {
                    if (other.GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[i].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;

                        float currnetHP = GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp;

                        if (currnetHP <= 0)
                        {
                            GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp = 0;
                            isZero = true;
                        }
                    }
                }
            }
            else
            {
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage + 100;
                float currnetHP = other.GetComponent<EnemyStat>().nowHp;

                if (currnetHP <= 0)
                {
                    other.GetComponent<EnemyStat>().nowHp = 0;
                    isZero = true;
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
        if (isZero)
        {
            for (int i = 0; i < battleLoader.Players.Count; i++)
            {
                if (battleLoader.Players[i].GetComponent<PlayerStat>().nowHp == 0)
                {
                    battleLoader.Players.RemoveAt(i);
                }
            }

            for (int i = 0; i < battleLoader.Enemys.Count; i++)
            {
                if (battleLoader.Enemys[i].GetComponent<EnemyStat>().nowHp == 0)
                {
                    battleLoader.Enemys.RemoveAt(i);
                    Destroy(battleLoader.EnemyStats[i].gameObject);
                    battleLoader.EnemyStats.RemoveAt(i);

                    Destroy(battleLoader.Encounter[i].gameObject);
                }
            }

            battleOrderManager.SetOrder();

            isZero = false;
        }

        if (battleLoader.Enemys.Count > 0 && battleLoader.Players.Count > 0)
        {
            battleOrderManager.TurnChange();
        }

        Destroy(gameObject);
    }
}
