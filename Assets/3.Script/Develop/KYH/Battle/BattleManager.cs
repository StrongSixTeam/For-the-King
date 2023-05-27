using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class BattleManager : MonoBehaviour
{
    //배틀에서 사용되는 함수 모두 관리
    //순서에 맞는 무기의 확률 띄우기
    //
    //순서 결정하는 스크립트 참조하기

    private BattleOrderManager battleOrderManager;
    private BattleLoader battleLoader;

    [SerializeField] private GameObject bulletPrefs;

    public GameObject target;
    private bool isPlayer = false;

    public int attackDamage = 0;

    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }
    private void Update()
    {
        if (isPlayer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);

                for (int i = 0; i < battleLoader.Enemys.Count; i++)
                {
                    if (hit.transform.gameObject == battleLoader.Enemys[i])
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
        }
    }
    public void Attack()
    {
        isPlayer = false;

        //공격력 계산 : 스킬 최대 공격력 * 성공확률, 성공확률이 최대일 경우 치명타 확률 +
        //플레이어가 장착한 무기에 접근해서 공격력 가져오기

        Instantiate(bulletPrefs, battleOrderManager.Order[battleOrderManager.turn].transform.position, battleOrderManager.Order[battleOrderManager.turn].transform.rotation);
    }
}
