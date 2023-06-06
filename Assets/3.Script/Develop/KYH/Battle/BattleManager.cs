using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private BattleOrderManager battleOrderManager;
    [SerializeField] private BattleLoader battleLoader;
    BattleCameraController battleCameraController;
    ItemInputTest1 itemInput;

    public List<GameObject> dieObj;

    [SerializeField] GameObject WinBattleBanner;
    [SerializeField] GameObject LoseBattleBanner;

    Camera activeCam;
    [SerializeField] Camera MainCam;

    [SerializeField] private GameObject bulletPrefs;
    [SerializeField] private GameObject slotUI; //����
    [SerializeField] private GameObject enemySlotUI; //���� ����

    [SerializeField] private GameObject[] readyCheckUI;
    [SerializeField] private GameObject readyChecker;

    public GameObject target;
    public bool isPlayer = false;
    public bool isEnd = false;
    private bool isCave = false;

    public int attackDamage = 0;

    public GameObject BattleUI;

    [SerializeField] GameObject Get;

    private Camera CurrnetCam;

    private void OnEnable()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
        battleCameraController = FindObjectOfType<BattleCameraController>();

        if (GameObject.FindGameObjectWithTag("BattleCamera") != null)
        {
            activeCam = GameObject.FindGameObjectWithTag("BattleCamera").GetComponent<Camera>();
        }
        else
        {
            activeCam = GameObject.FindGameObjectWithTag("CaveCamera").GetComponent<Camera>();
        }

        CurrnetCam = FindObjectOfType<Camera>();
        itemInput = FindObjectOfType<ItemInputTest1>();

        isEnd = false;
    }
    private void Update()
    {
        enemySlotUI.GetComponent<CloneSlot>().playerTurn = slotUI.GetComponent<CloneSlot>().playerTurn;

        if (isPlayer)
        {
            BattleUI.SetActive(true);
            for (int i = 0; i < enemySlotUI.transform.childCount; i++)
            {
                enemySlotUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            Ray ray = activeCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < battleLoader.Enemys.Count; i++)
                {
                    if (hit.transform.gameObject == battleLoader.Enemys[i] && Input.GetMouseButtonDown(0))
                    {
                        battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Enemys[i].transform);
                        target = battleLoader.Enemys[i];
                    }
                }
            }
        }

        if (battleLoader.Players.Count == 0 && !isEnd)
        {
            LoseBattleBanner.SetActive(true);

            isCave = false;

            Invoke("BattleEnd", 3f);

            isEnd = true;

        }
        if (battleLoader.Enemys.Count == 0 && !isEnd)
        {
            if (battleCameraController.gameObject.name == "BattleCamera")
            {
                StartCoroutine(battleCameraController.PlayerWinCam_co());
            }

            WinBattleBanner.SetActive(true);

            for (int i = 0; i < battleLoader.Players.Count; i++)
            {
                Text txt = Instantiate(Get, CurrnetCam.WorldToScreenPoint(battleLoader.Players[i].transform.position) + new Vector3(0, 300, 0), Quaternion.identity).GetComponent<Text>();
                txt.transform.SetParent(GameObject.Find("Canvas").transform);
                txt.text = "+" + battleLoader.totalExp + "Exp";

                for (int j = 0; j < GameManager.instance.Players.Length; j++)
                {
                    if (battleLoader.Players[i].GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[j].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[j].GetComponent<PlayerStat>().nowExp += battleLoader.totalExp;
                    }
                }

                if (itemInput.itemTurn > battleLoader.Players.Count - 1)
                {
                    itemInput.itemTurn = 0;
                }

                battleLoader.currentItemInputUI[itemInput.itemTurn].SetActive(true);
            }

            FindObjectOfType<LevelUpStatus>().LevelUp();

            isEnd = true;
        }
    }
    public void RookAt()
    {
        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            isPlayer = true;
            slotUI.GetComponent<CloneSlot>().playerTurn = true;
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Enemys[0].transform);
            target = battleLoader.Enemys[0];
            SlotController.instance.fixCount = 0;
        }
        else
        {
            slotUI.GetComponent<CloneSlot>().playerTurn = false;
            int rnd = Random.Range(0, battleLoader.Players.Count);
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Players[rnd].transform);
            target = battleLoader.Players[rnd];
            //DefaultAttack();
            EnemyAttack();
        }
    }

    public GameObject FindPlayer(int x)
    {
        for (int i = 0; i < GameManager.instance.Players.Length; i++)
        {
            if (x == GameManager.instance.Players[i].GetComponent<PlayerStat>().order)
            {
                return GameManager.instance.Players[i];
            }
        }
        return GameManager.instance.MainPlayer;
    }

    public SlotController.Type AttackTypeToType(Weapon weapon)
    {
        if (weapon.attackType.ToString() == "attackBlackSmith")
        {
            return SlotController.Type.attackBlackSmith;
        }
        else if (weapon.attackType.ToString() == "attackHunter")
        {
            return SlotController.Type.attackHunter;
        }
        else if (weapon.attackType.ToString() == "attackScholar")
        {
            return SlotController.Type.attackScholar;
        }
        else
        {
            return SlotController.Type.empty;
        }
    }

    public SlotController.Type StringToType(string sentence)
    {
        if (sentence == "attackBlackSmith")
        {
            return SlotController.Type.attackBlackSmith;
        }
        else if (sentence == "attackHunter")
        {
            return SlotController.Type.attackHunter;
        }
        else if (sentence == "attackScholar")
        {
            return SlotController.Type.attackScholar;
        }
        else
        {
            return SlotController.Type.empty;
        }
    }
    public void PlayerAttack() //�÷��̾� ������
    {
        isPlayer = false;

        battleOrderManager.Order[battleOrderManager.turn].GetComponent<Animator>().SetTrigger("Attack");

        BattleUI.SetActive(false);
        //slot �۵�
        slotUI.GetComponent<CloneSlot>().Try();
    }
    public void CalculateAtk()
    {
        float originalAtk = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().atk;
        float resultAtk = originalAtk / battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon.maxSlot * SlotController.instance.success;
        attackDamage = (int)resultAtk;
    }


    public void CalculateEnemyAtk()
    {
        float originalAtk = battleOrderManager.Order[battleOrderManager.turn].GetComponent<EnemyStat>().atk;
        float resultAtk = originalAtk / battleOrderManager.Order[battleOrderManager.turn].GetComponent<EnemyStat>().maxSlot * SlotController.instance.success;
        attackDamage = (int)resultAtk;
        MakeBullet();
    }

    public void PlayerRun()
    {
        isPlayer = false;
        BattleUI.SetActive(false);
        slotUI.GetComponent<CloneSlot>().Try();
    }
    public void RunFalse()
    {
        Invoke("TurnChange", 1f);
    }
    private void TurnChange()
    {
        battleOrderManager.TurnChange();
    }

    public void MakeBullet()
    {
        GameObject bullet = Instantiate(bulletPrefs, battleOrderManager.Order[battleOrderManager.turn].transform.position, Quaternion.identity);
        bullet.transform.position += new Vector3(0, 1, 0);
    }

    public void EnemyAttack()
    {
        isPlayer = false;

        battleOrderManager.Order[battleOrderManager.turn].GetComponent<Animator>().SetTrigger("Attack");

        BattleUI.SetActive(false);
        slotUI.SetActive(false);

        SlotController.instance.maxSlotCount = battleOrderManager.Order[battleOrderManager.turn].GetComponent<EnemyStat>().maxSlot;
        SlotController.instance.percent = (int)battleOrderManager.Order[battleOrderManager.turn].GetComponent<EnemyStat>().percent;
        SlotController.instance.fixCount = 0;
        SlotController.instance.type = StringToType(battleOrderManager.Order[battleOrderManager.turn].GetComponent<EnemyStat>().attackType);
        enemySlotUI.GetComponent<CloneSlot>().Initialized();
        //slot �۵�
        enemySlotUI.GetComponent<CloneSlot>().Try();
    }

    public void DefaultAttack()
    {
        isPlayer = false;

        //���� �ִϸ��̼� �ֱ�

        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            attackDamage = (int)p.atk /* *����Ȯ�� */; //��� ���� �� ġ��Ÿ
        }
        else if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out EnemyStat e))
        {
            attackDamage = (int)e.atk /* *����Ȯ�� */; //��� ���� �� ġ��Ÿ 
        }

        GameObject bullet = Instantiate(bulletPrefs, battleOrderManager.Order[battleOrderManager.turn].transform.position, Quaternion.identity);
        bullet.transform.position += new Vector3(0, 1, 0);

        BattleUI.SetActive(false);
    }
    public void Pass()
    {
        battleLoader.currentItemInputUI[itemInput.itemTurn].SetActive(false);

        itemInput.itemTurn++;

        if (itemInput.itemTurn > battleLoader.Players.Count - 1)
        {
            itemInput.itemTurn = 0;
        }

        battleLoader.currentItemInputUI[itemInput.itemTurn].SetActive(true);
    }
    public void ItemGet()
    {
        Text txt = Instantiate(Get, CurrnetCam.WorldToScreenPoint(battleLoader.Players[itemInput.itemTurn].transform.position) + new Vector3(0, 300, 0), Quaternion.identity).GetComponent<Text>();
        txt.transform.SetParent(GameObject.Find("Canvas").transform);
        txt.text = "+" + battleLoader.items[0].itemName;

        InventoryController1.instance.playerNum = (PlayerNum)System.Enum.Parse(typeof(PlayerNum), string.Format("Player{0}", battleLoader.Players[itemInput.itemTurn].GetComponent<PlayerStat>().order));

        itemInput.Get(battleLoader.items[0]);
        battleLoader.items.RemoveAt(0);

        if (battleLoader.items.Count > 0)
        {
            Pass();
        }
        else
        {
            battleLoader.currentItemInputUI[itemInput.itemTurn].SetActive(false);
            Invoke("BattleEnd", 2f);
        }
    }
    public void BattleEnd()
    {
        if (battleCameraController.gameObject.name == "CaveCamera" && battleLoader.Players.Count > 0)
        {
            EndCheck();
        }

        for(int i = 0; i < dieObj.Count; i++)
        {
            Destroy(dieObj[i]);
        }

        dieObj.Clear();

        battleLoader.PrefsDestroy();
        battleOrderManager.End();

        battleLoader.gameObject.SetActive(false);

        WinBattleBanner.SetActive(false);
        LoseBattleBanner.SetActive(false);

        isPlayer = false;

        if (!isCave)
        {
            FindObjectOfType<MultiCamera>().ToMain();
            GameManager.instance.MainPlayer.GetComponent<PlayerController_Jin>().BeOriginalScale();
        }

        gameObject.SetActive(false);
    }
    private void EndCheck()
    {
        if (battleLoader.caveBattleTurn == 1 || battleLoader.caveBattleTurn == 3 || battleLoader.caveBattleTurn == 4)
        {
            battleLoader.caveBattleTurn++;
            isCave = true;
            readyChecker.SetActive(true);

            for (int i = 0; i < battleLoader.Players.Count; i++)
            {
                readyCheckUI[i].SetActive(true);
            }
        }
        else if (battleLoader.caveBattleTurn == 2 || battleLoader.caveBattleTurn == 5)
        {
            battleLoader.caveBattleTurn++;
            isCave = false;
        }
    }
}
