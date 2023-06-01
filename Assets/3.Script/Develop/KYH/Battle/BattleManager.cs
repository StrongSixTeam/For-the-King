using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class BattleManager : MonoBehaviour
{
    //배틀에서 사용되는 함수 모두 관리
    //순서에 맞는 무기의 확률 띄우기
    //순서 결정하는 스크립트 참조하기

    private BattleOrderManager battleOrderManager;
    [SerializeField] private BattleLoader battleLoader;

    Camera battlecam;

    [SerializeField] private GameObject bulletPrefs;

    public GameObject target;
    private bool isPlayer = false;

    public int attackDamage = 0;

    public GameObject BattleUI;

    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
        battlecam = GameObject.FindGameObjectWithTag("BattleCamera").GetComponent<Camera>();
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
}
