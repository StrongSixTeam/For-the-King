using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
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

    private PlayerController_Jin playerController;

    public List<GameObject> Encounter = new List<GameObject>();

    public bool isIng = false;

    public int totalExp = 0;
    public int Gold = 0;

    private ItemInputTest1 itemInput;
    public List<Item> items = new List<Item>();

    [SerializeField] GameObject[] ItemInputUI;
    public List<GameObject> currentItemInputUI;

    private void Start()
    {
        playerController = GameManager.instance.MainPlayer.GetComponent<PlayerController_Jin>();
        itemInput = FindObjectOfType<ItemInputTest1>();

        int rand = Random.Range(1, 3);
        for (int i = 0; i < rand; i++)
        {
            items.Add(itemInput.Stack());
        }

        Encounter = playerController.CheckAroundObject();
        Encounter.Add(GameManager.instance.MainPlayer);
    }
    public void FieldBattle()
    {
        playerPos = new Vector3(-103.5f, 0, -11f);
        enemyPos = new Vector3(-98, 0, -11f);

        playerAngle = new Vector3(0, 90, 0);
        enemyAngle = new Vector3(0, 270, 0);

        moveTowards[0] = Vector3.forward;
        moveTowards[1] = Vector3.back;

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
        for (int i = 0; i < Encounter.Count; i++)
        {
            if (Encounter[i].GetComponent<PlayerStat>() != null)
            {
                Players.Add(Instantiate(Encounter[i], playerPos, Quaternion.Euler(playerAngle)));
            }
            else
            {
                Enemys.Add(Instantiate(Encounter[i], enemyPos, Quaternion.Euler(enemyAngle)));

                EnemyStats.Add(Instantiate(EnemyStatPrefs));
                EnemyStats[i].transform.SetParent(GameObject.Find("Canvas").transform);
                EnemyStats[i].transform.localPosition = new Vector2(0, 400);
                EnemyStats[i].GetComponent<EnemyUI>().enemyStat = Enemys[i].GetComponent<EnemyStat>();

                totalExp += Enemys[i].GetComponent<EnemyStat>().Exp;
                Gold += Random.Range(1, 6);
            }
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerController_Jin>().enabled = false;

            currentItemInputUI.Add(ItemInputUI[Players[i].GetComponent<PlayerStat>().order]);

            Players[i].transform.localScale = new Vector3(1, 1, 1);
        }

        if (Players.Count == 2)
        {
            Players[0].transform.position += moveTowards[0];
            Players[1].transform.position += moveTowards[1];
        }
        if (Players.Count == 3)
        {
            Players[0].transform.position += moveTowards[0] * 2;
            Players[2].transform.position += moveTowards[1] * 2;
        }


        if (Enemys.Count == 2)
        {
            Enemys[0].transform.position += moveTowards[0];
            Enemys[1].transform.position += moveTowards[1];

            EnemyStats[0].transform.position += new Vector3(-250, 0, 0);
            EnemyStats[1].transform.position += new Vector3(250, 0, 0);
        }
        if (Enemys.Count == 3)
        {
            Enemys[0].transform.position += moveTowards[0] * 2;
            Enemys[2].transform.position += moveTowards[1] * 2;

            EnemyStats[0].transform.position += new Vector3(-500, 0, 0);
            EnemyStats[2].transform.position += new Vector3(500, 0, 0);
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
        for (int i = 0; i < Players.Count; i++)
        {
            Destroy(Players[i]);
            Destroy(Enemys[i]);
            Destroy(EnemyStats[i]);
        }

        totalExp = 0;
        Gold = 0;

        Players.Clear();
        Enemys.Clear();
        EnemyStats.Clear();
        Encounter.Clear();
        items.Clear();
        currentItemInputUI.Clear();
        isIng = false;
    }
}
