using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
    //전투에 돌입한 플레이어, 에너미 리스트
    public List<GameObject> Players;
    public List<GameObject> Enemys;
    public List<GameObject> EnemyStats;

    [SerializeField] GameObject EnemyStatPrefs;

    //전투 시작 > 배틀은 키고 필드는 끔
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject fieldUI;
    [SerializeField] GameObject QuestUI;
    [SerializeField] GameObject SlotUI;

    //소환 위치 벡터값
    Vector3 playerPos;
    Vector3 enemyPos;

    Vector3 playerAngle;
    Vector3 enemyAngle;

    Vector3[] moveTowards = new Vector3[2];

    private PlayerController_Jin playerController;

    //전투에 돌입할 주변 플레이어, 적 리스트
    public List<GameObject> Encounter = new List<GameObject>();

    public bool isIng = false;
    public bool isBattle = false;

    //먹을 경험치, 골드
    public int totalExp = 0;
    public int Gold = 0;

    //먹을 아이템 리스트
    private ItemInputTest1 itemInput;
    public List<Item> items = new List<Item>();

    //아이템 먹기 UI 리스트
    [SerializeField] GameObject[] ItemInputUI;
    public List<GameObject> currentItemInputUI;

    public int caveBattleTurn = 1;
    private int scrollMap = 1;

    private void OnEnable()
    {
        playerController = GameManager.instance.MainPlayer.GetComponent<PlayerController_Jin>();
        itemInput = FindObjectOfType<ItemInputTest1>();

        int rand = Random.Range(1, 3);
        for (int i = 0; i < rand; i++)
        {
            items.Add(itemInput.Stack());
        }
    }
    public void FieldBattle() //필드 배틀 초기값
    {
        Encounter = playerController.CheckAroundObject();
        Encounter.Add(GameManager.instance.MainPlayer);

        playerPos = new Vector3(-103.5f, 0, -11f);
        enemyPos = new Vector3(-98, 0, -11f);

        playerAngle = new Vector3(0, 90, 0);
        enemyAngle = new Vector3(0, 270, 0);

        moveTowards[0] = Vector3.forward;
        moveTowards[1] = Vector3.back;

        PrefsInstantiate();
    }
    public void CaveBattle() //동굴 배틀 초기값
    {
        switch (caveBattleTurn)
        {
            case 1:
                scrollMap = 1;
                Encounter = GameObject.Find("CaveEnemy01").GetComponent<CaveBattleBox>().enemys01;
                break;
            case 2:
                scrollMap = 2;
                Encounter = GameObject.Find("CaveEnemy01").GetComponent<CaveBattleBox>().enemys02;
                break;
            case 3:
                scrollMap = 3;
                Encounter = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>().enemys01;
                break;
            case 4:
                scrollMap = 0;
                Encounter = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>().enemys02;
                break;
            case 5:
                scrollMap = 1;
                Encounter = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>().enemys03;
                break;
        }

        for (int i = 0; i < GameManager.instance.Players.Length; i++)
        {
            Encounter.Add(GameManager.instance.Players[i]);
        }

        playerPos = new Vector3(-200f, 0, -38);
        enemyPos = new Vector3(0, 0, -1);

        playerAngle = new Vector3(0, 0, 0);
        enemyAngle = new Vector3(0, 180, 0);

        moveTowards[0] = Vector3.left;
        moveTowards[1] = Vector3.right;

        for (int i = 0; i < FindObjectsOfType<CaveMapPooling>().Length; i++)
        {
            FindObjectsOfType<CaveMapPooling>()[i].CameraSet();
        }

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

        if (enemyPos == new Vector3(0, 0, -1))
        {
            for (int i = 0; i < Enemys.Count; i++)
            {
                Enemys[i].transform.SetParent(GameObject.Find("CaveObj").transform.GetChild(scrollMap));
                Enemys[i].transform.localPosition = enemyPos;
                Enemys[i].SetActive(true);
            }
        }

        GameObject temp = null;

        for (int i = 0; i < Players.Count; i++)
        {
            for (int l = i + 1; l < Players.Count; l++)
            {
                if (Players[i].GetComponent<PlayerStat>().order > Players[l].GetComponent<PlayerStat>().order)
                {
                    temp = Players[i];
                    Players[i] = Players[l];
                    Players[l] = temp;
                }
            }                 
            
            Players[i].GetComponent<PlayerController_Jin>().enabled = false;
            Players[i].transform.localScale = new Vector3(1, 1, 1);

           
            Players[i].GetComponent<Animator>().SetTrigger("Battle");

            if(playerPos == new Vector3(-200f, 0, -38))
            {
                Players[i].SetActive(false);
            }

            currentItemInputUI.Add(ItemInputUI[Players[i].GetComponent<PlayerStat>().order]);
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
        QuestUI.SetActive(false);
        isBattle = true;

        BattleOrderManager battleOrderManager = FindObjectOfType<BattleOrderManager>();
        //SlotController.instance.maxSlotCount = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon.maxSlot;
        //SlotController.instance.type = FindObjectOfType<BattleManager>().AttackTypeToType(battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon);
        SlotController.instance.hasLimit = false;
        Invoke("PlayerSetActive", 4f);
    }
    private void PlayerSetActive()
    {
        for(int i = 0; i< Players.Count; i++)
        {
            Players[i].SetActive(true);
        }
    }
    public void PrefsDestroy() //배틀 끝나면 불러오는 함수
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Destroy(Players[i]);
        }
        for (int i = 0; i < Enemys.Count; i++)
        {
            Destroy(Enemys[i]);
            Destroy(EnemyStats[i]);
        }

        totalExp = 0;
        Gold = 0;

        battleUI.SetActive(false);
        SlotUI.SetActive(false);
        fieldUI.SetActive(true);
        QuestUI.SetActive(true);
        isBattle = false;

        Players.Clear();
        Enemys.Clear();
        EnemyStats.Clear();
        Encounter.Clear();
        items.Clear();
        isIng = false;
        currentItemInputUI.Clear();
        SlotController.instance.hasLimit = true;
        SlotUI.GetComponent<CloneSlot>().isShowText = true;
    }
}
