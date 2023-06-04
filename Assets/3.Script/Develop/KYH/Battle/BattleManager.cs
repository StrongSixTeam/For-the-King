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

    [SerializeField] GameObject WinBattleBanner;
    [SerializeField] GameObject LoseBattleBanner;

    Camera activeCam;
    [SerializeField] Camera MainCam;

    [SerializeField] private GameObject bulletPrefs;
    [SerializeField] private GameObject slotUI; //슬롯

    public GameObject target;
    private bool isPlayer = false;
    public bool isEnd = false;

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
    }
    private void Update()
    {
        if (isPlayer)
        {
            BattleUI.SetActive(true);

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
            if (battleCameraController.gameObject.name == "BattleCamera")
            {
                StartCoroutine(battleCameraController.EnemyWinCam_co());
            }

            LoseBattleBanner.SetActive(true);

            Invoke("BattleEnd", 5f);

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
        }
    }
    public void CalculateAtk()
    {
        float originalAtk = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().atk;
        float resultAtk = originalAtk / battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon.maxSlot * SlotController.instance.success;
        attackDamage = (int)resultAtk;
    }
    public void RookAt()
    {
        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            isPlayer = true;
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Enemys[0].transform);
            target = battleLoader.Enemys[0];
            SlotController.instance.fixCount = 0;
        }
        else
        {
            int rnd = Random.Range(0, battleLoader.Players.Count);
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Players[rnd].transform);
            target = battleLoader.Players[rnd];
            DefaultAttack();
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
    public void PlayerAttack()
    {
        isPlayer = false;

        //공격 애니메이션 넣기

        BattleUI.SetActive(false);
        //slot 작동
        slotUI.GetComponent<CloneSlot>().Try();
    }

    public void PlayerRun()
    {
        isPlayer = false;
        BattleUI.SetActive(false);
        slotUI.GetComponent<CloneSlot>().Try();
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

    public void DefaultAttack()
    {
        isPlayer = false;

        //공격 애니메이션 넣기

        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            attackDamage = (int)p.atk /* *성공확률 */; //모두 성공 시 치명타
        }
        else if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out EnemyStat e))
        {
            attackDamage = (int)e.atk /* *성공확률 */; //모두 성공 시 치명타 
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
        InventoryController1.instance.playerNum = PlayerNum.Player0;

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
        battleLoader.PrefsDestroy();
        battleOrderManager.End();

        battleLoader.gameObject.SetActive(false);

        WinBattleBanner.SetActive(false);
        LoseBattleBanner.SetActive(false);

        isPlayer = false;
        isEnd = false;

        FindObjectOfType<MultiCamera>().ToMain();
        GameManager.instance.MainPlayer.GetComponent<PlayerController_Jin>().BeOriginalScale();
    }
}
