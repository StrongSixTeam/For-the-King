using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
    public GameObject enemy; // 테스트용
    
    public List<GameObject> Players;
    public List<GameObject> Enemys;
    public List<GameObject> EnemyStats;

    [SerializeField] GameObject EnemyStatPrefs;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject fieldUI;

    Vector3 playerPos;
    Vector3 enemyPos;

    Vector3 playerAngle;
    Vector3 enemyAngle;

    Vector3[] moveTowards = new Vector3[2];

    public void FieldBattle()
    {
        playerPos = new Vector3(-103.5f, 0, -11f);
        enemyPos = new Vector3(-98, 0, -11f);

        playerAngle = new Vector3(0, 90, 0);
        enemyAngle = new Vector3(0, 270, 0);

        moveTowards[0] = Vector3.back;
        moveTowards[1] = Vector3.forward;

        PrefsInstantiate();
    }
    public void CaveBattle()
    {
        playerPos = new Vector3(-199.7f, 0, -38);
        enemyPos = new Vector3(-199.7f, 0, -33);

        playerAngle = new Vector3(0, 0, 0);
        enemyAngle = new Vector3(0, 180, 0);

        moveTowards[0] = Vector3.left;
        moveTowards[1] = Vector3.right;

        PrefsInstantiate();
    }

    private void PrefsInstantiate() //전투 돌입 시 실행할 함수
    {
        //플레이어 소환
        for (int i = 0; i < GameManager.instance.Players.Length; i++)
        {
            Players.Add(Instantiate(GameManager.instance.Players[i], playerPos, Quaternion.Euler(playerAngle)));
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerController_Jin>().enabled = false;
            Players[i].transform.GetChild(0).gameObject.SetActive(true);
            Players[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameManager.instance.Players.Length == 2)
        {
            Players[0].transform.position += moveTowards[0];
            Players[1].transform.position += moveTowards[1];
        }
        if (GameManager.instance.Players.Length == 3)
        {
            Players[0].transform.position += moveTowards[0] * 2;
            Players[2].transform.position += moveTowards[1] * 2;
        }
        //적 소환
        for (int i = 0; i < 3; i++) //에너미 수정 필요 > enemys.count 받아야함
        {
            Enemys.Add(Instantiate(enemy, enemyPos, Quaternion.Euler(enemyAngle))); //에너미 수정 필요
            EnemyStats.Add(Instantiate(EnemyStatPrefs));
            EnemyStats[i].transform.SetParent(GameObject.Find("Canvas").transform);
            EnemyStats[i].transform.localPosition = new Vector2(0, 400);
            EnemyStats[i].GetComponent<EnemyUI>().enemyStat = Enemys[i].GetComponent<EnemyStat>();
        }

        if (Enemys.Count == 2)
        {
            Enemys[0].transform.position += moveTowards[0];
            Enemys[1].transform.position += moveTowards[1];

            EnemyStats[0].transform.position += new Vector3(250, 0, 0);
            EnemyStats[1].transform.position += new Vector3(-250, 0, 0);
        }
        if (Enemys.Count == 3)
        {
            Enemys[0].transform.position += moveTowards[0] * 2;
            Enemys[2].transform.position += moveTowards[1] * 2;

            EnemyStats[0].transform.position += new Vector3(500, 0, 0);
            EnemyStats[2].transform.position += new Vector3(-500, 0, 0);
        }

        battleUI.SetActive(true);
        fieldUI.SetActive(false);
    }

    private void OnDisable()
    {
        PrefsDestroy();
    }
    private void PrefsDestroy()
    {
        for(int i = 0; i<Players.Count; i++)
        {
            Destroy(Players[i]);
            Destroy(Enemys[i]);
            Destroy(EnemyStats[i]);
        }
        Players.Clear();
        Enemys.Clear();
        EnemyStats.Clear();
    }
}
