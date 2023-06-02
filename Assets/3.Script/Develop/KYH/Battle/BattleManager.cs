using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private BattleOrderManager battleOrderManager;
    [SerializeField] private BattleLoader battleLoader;
    BattleCameraController battleCameraController;

    Camera battlecam;
    [SerializeField] Camera MainCam;

    [SerializeField] private GameObject bulletPrefs;

    public GameObject target;
    private bool isPlayer = false;
    public bool isEnd = false;

    public int attackDamage = 0;

    public GameObject BattleUI;

    [SerializeField] GameObject Get;

    private Camera CurrnetCam;

    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
        battleCameraController = FindObjectOfType<BattleCameraController>();
        battlecam = GameObject.FindGameObjectWithTag("BattleCamera").GetComponent<Camera>();
        CurrnetCam = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        if (isPlayer)
        {
            BattleUI.SetActive(true);

            Ray ray = battlecam.ScreenPointToRay(Input.mousePosition);
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

        if(battleLoader.Players.Count == 0 && !isEnd)
        {


        }
        if (battleLoader.Enemys.Count == 0 && !isEnd)
        {
            StartCoroutine(battleCameraController.PlayerWinCam_co());

            for (int i = 0; i < battleLoader.Players.Count; i++)
            {
                Text txt = Instantiate(Get, CurrnetCam.WorldToScreenPoint(battleLoader.Players[i].transform.position) + new Vector3(0, 300, 0), Quaternion.identity).GetComponent<Text>();
                Debug.Log(txt.text);
                txt.transform.SetParent(GameObject.Find("Canvas").transform);
                txt.text = "+" + battleLoader.totalExp;

                for (int j = 0; j < GameManager.instance.Players.Length; j++)
                {
                    if (battleLoader.Players[i].GetComponent<PlayerStat>().name.Equals(GameManager.instance.Players[j].GetComponent<PlayerStat>().name))
                    {
                        GameManager.instance.Players[j].GetComponent<PlayerStat>().nowExp += battleLoader.totalExp;
                    }
                }
            }
        }
    }
    public void RookAt()
    {
        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            isPlayer = true;
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Enemys[0].transform);
            target = battleLoader.Enemys[0];
        }
        else
        {
            int rnd = Random.Range(0, battleLoader.Players.Count);
            battleOrderManager.Order[battleOrderManager.turn].transform.LookAt(battleLoader.Players[rnd].transform);
            target = battleLoader.Players[rnd];
            DefaultAttack();
        }
    }
    public void DefaultAttack()
    {
        isPlayer = false;

        //공격 애니메이션 넣기

        if (battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out PlayerStat p))
        {
            attackDamage = (int)p.atk /* *성공확률 */; //모두 성공 시 치명타
        }
        else if(battleOrderManager.Order[battleOrderManager.turn].TryGetComponent(out EnemyStat e))
        {
            attackDamage = (int)e.atk /* *성공확률 */; //모두 성공 시 치명타 
        }

        GameObject bullet =  Instantiate(bulletPrefs, battleOrderManager.Order[battleOrderManager.turn].transform.position, Quaternion.identity);
        bullet.transform.position += new Vector3(0, 1, 0);

        BattleUI.SetActive(false);
    }
    private void OnDisable()
    {
        isPlayer = false;
        isEnd = false;
    }

    public void BattleEnd()
    {
        battleLoader.gameObject.SetActive(false);
        battlecam.gameObject.SetActive(false);
        MainCam.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
