using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private int speed = 7;

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
                other.GetComponent<Animator>().SetBool("Hit", true);

                for (int i = 0; i < GameManager.instance.Players.Length; i++)
                {
                    if (other.GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[i].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage;

                        float currnetHP = GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp;

                        if (currnetHP <= 0)
                        {
                            GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp = 0;
                            other.GetComponent<Animator>().SetBool("Die", true);
                            isZero = true;
                        }
                    }
                }
            }
            else
            {
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage;
                float currnetHP = other.GetComponent<EnemyStat>().nowHp;

                if (currnetHP <= 0)
                {
                    other.GetComponent<Animator>().SetTrigger("Die");
                    other.GetComponent<EnemyStat>().nowHp = 0;
                    isZero = true;
                }
                else
                {
                    other.GetComponent<Animator>().SetTrigger("Hit");
                }

                if (currnetHP > 0)
                {
                    for (int i = 0; i < battleLoader.Enemys.Count; i++)
                    {
                        battleLoader.Enemys[i].GetComponent<Animator>().SetTrigger("Idle");
                    }
                }
            }

            Invoke("BulletDestroy", 1.5f);
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, battleManager.target.transform.position + new Vector3(0, 1, 0), speed * Time.deltaTime);
    }
    private void BulletDestroy()
    {
        for (int i = 0; i < battleLoader.Players.Count; i++)
        {
            battleLoader.Players[i].GetComponent<Animator>().SetBool("Hit", false);
            battleLoader.Players[i].GetComponent<Animator>().SetBool("Attack", false);
        }

        if (isZero)
        {
            for (int i = 0; i < battleLoader.Players.Count; i++)
            {
                if (battleLoader.Players[i].GetComponent<PlayerStat>().nowHp <= 0)
                {
                    if (GameManager.instance.currentLife > 0)
                    {
                        GameManager.instance.currentLife--;

                        battleLoader.Players[i].GetComponent<PlayerStat>().nowHp = battleLoader.Players[i].GetComponent<PlayerStat>().maxHp * 0.5f;
                        battleLoader.Players[i].GetComponent<Animator>().SetBool("Die", false);
                        battleLoader.Players[i].GetComponent<Animator>().SetTrigger("Revive");
                        battleLoader.Players[i].GetComponent<Animator>().SetTrigger("Battle");

                        for (int j = 0; j < GameManager.instance.Players.Length; j++)
                        {
                            if (GameManager.instance.Players[j].GetComponent<PlayerStat>().name.Equals(battleLoader.Players[i].GetComponent<PlayerStat>().name))
                            {
                                GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp = GameManager.instance.Players[i].GetComponent<PlayerStat>().maxHp * 0.5f;
                            }
                        }


                    }
                    else
                    {
                        Destroy(battleLoader.Players[i]);
                        battleLoader.Players.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < battleLoader.Enemys.Count; i++)
            {
                if (battleLoader.Enemys[i].GetComponent<EnemyStat>().nowHp <= 0)
                {
                    FindObjectOfType<MapObjectCreator>().randomMonsterIndex[FindObjectOfType<PlayerController_Jin>().monsterIndex] = 0;
                    Destroy(battleLoader.Enemys[i]);
                    battleLoader.Enemys.RemoveAt(i);
                    Destroy(battleLoader.EnemyStats[i].gameObject);
                    battleLoader.EnemyStats.RemoveAt(i);

                    if (CurrnetCam.name == "BattleCamera")
                    {
                        Destroy(battleLoader.Encounter[i].gameObject);
                        //EncounterManager.instance.enemies[EncounterManager.instance.enemyNumber].isCleared = true;
                    }

                    //EncounterManager.instance.enemies[EncounterManager.instance.enemyNumber].isCleared = true;
                    GameManager.instance.DeactivePortrait();
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
