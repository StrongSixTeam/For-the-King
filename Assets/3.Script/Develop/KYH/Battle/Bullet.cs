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
            //txt.text = "-" + battleManager.attackDamage;

            if (other.GetComponent<PlayerStat>() != null)
            { //플레이어가 맞을때
                battleManager.attackDamage -= (int)other.GetComponent<PlayerStat>().def;
                if (battleManager.attackDamage < 0)
                {
                    battleManager.attackDamage = 0;
                }
                txt.text = "-" + battleManager.attackDamage;
                other.GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage + 100;
                other.GetComponent<Animator>().SetBool("Hit", true);

                //EffectManager.Instance.PlayEffect(other.transform.position + new Vector3(0, 1.5f, 0), null, EffectType.PlayerHit);

                for (int i = 0; i < GameManager.instance.Players.Count; i++)
                {
                    if (other.GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[i].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp -= battleManager.attackDamage + 100;

                        float currnetHP = GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp;

                        if (currnetHP <= 0)
                        {
                            GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp = 0;
                            other.GetComponent<Animator>().SetBool("Die", true);
                            isZero = true;
                        }
                    }
                }

                for (int i = 0; i < battleLoader.Enemys.Count; i++)
                {
                    battleLoader.Enemys[i].GetComponent<Animator>().SetTrigger("Idle");
                }
            }
            else //적이 맞을때
            {
                txt.text = "-" + battleManager.attackDamage;
                other.GetComponent<EnemyStat>().nowHp -= battleManager.attackDamage;
                float currnetHP = other.GetComponent<EnemyStat>().nowHp;

                //EffectManager.Instance.PlayEffect(other.transform.position + new Vector3(0, 1f, 0), null, EffectType.EnemyHit);

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

                for (int i = 0; i < battleLoader.Players.Count; i++)
                {
                    battleLoader.Players[i].GetComponent<Animator>().SetTrigger("Battle");
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
            battleLoader.Players[i].GetComponent<Animator>().SetTrigger("Battle");
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

                        for (int j = 0; j < GameManager.instance.Players.Count; j++)
                        {
                            if (GameManager.instance.Players[j].GetComponent<PlayerStat>().name.Equals(battleLoader.Players[i].GetComponent<PlayerStat>().name))
                            {
                                GameManager.instance.Players[j].GetComponent<PlayerStat>().nowHp = GameManager.instance.Players[i].GetComponent<PlayerStat>().maxHp * 0.5f;
                            }
                        }


                    }
                    else
                    {
                        for (int j = 0; j < GameManager.instance.Players.Count; j++)
                        {
                            if (GameManager.instance.Players[j].GetComponent<PlayerStat>().name.Equals(battleLoader.Players[i].GetComponent<PlayerStat>().name))
                            {
                                GameManager.instance.dieCnt++;
                                Destroy(GameManager.instance.Players[j]);
                                //GameManager.instance.Players.RemoveAt(j);
                            }
                        }

                        Destroy(battleLoader.Players[i]);
                        battleLoader.Players[i].transform.GetChild(2).gameObject.SetActive(false);
                        battleLoader.Players.RemoveAt(i);

                        battleOrderManager.turn -= 1;
                    }
                }
            }

            for (int i = 0; i < battleLoader.Enemys.Count; i++)
            {
                if (battleLoader.Enemys[i].GetComponent<EnemyStat>().nowHp <= 0)
                {
                    FindObjectOfType<MapObjectCreator>().randomMonsterIndex[FindObjectOfType<PlayerController_Jin>().monsterIndex] = 0;
                    
                    //Destroy(battleLoader.Enemys[i]);
                    battleManager.dieObj.Add(battleLoader.Enemys[i]);
                    battleLoader.Enemys[i].transform.GetChild(0).gameObject.SetActive(false);
                    battleLoader.Enemys.RemoveAt(i);
                    Destroy(battleLoader.EnemyStats[i].gameObject);
                    battleLoader.EnemyStats.RemoveAt(i);

                    if (CurrnetCam.name == "BattleCamera")
                    {
                        Destroy(battleLoader.Encounter[i].gameObject);
                        if (EncounterManager.instance.number > 0)
                        {
                            EncounterManager.instance.encounter[EncounterManager.instance.number].isCleared = true;
                        }
                        else
                        {
                            EncounterManager.instance.enemies[EncounterManager.instance.enemyNumber].isCleared = true;
                        }
                    }
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
