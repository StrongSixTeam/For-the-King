using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class BattleManager : MonoBehaviour
{
    //��Ʋ���� ���Ǵ� �Լ� ��� ����
    //������ �´� ������ Ȯ�� ����
    //
    //���� �����ϴ� ��ũ��Ʈ �����ϱ�

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

        //���ݷ� ��� : ��ų �ִ� ���ݷ� * ����Ȯ��, ����Ȯ���� �ִ��� ��� ġ��Ÿ Ȯ�� +
        //�÷��̾ ������ ���⿡ �����ؼ� ���ݷ� ��������

        Instantiate(bulletPrefs, battleOrderManager.Order[battleOrderManager.turn].transform.position, battleOrderManager.Order[battleOrderManager.turn].transform.rotation);
    }
}
