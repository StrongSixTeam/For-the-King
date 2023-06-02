using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleOrderManager : MonoBehaviour
{
    [SerializeField] Image[] background;
    [SerializeField] Image[] portrait;

    [SerializeField] Sprite PBground;
    [SerializeField] Sprite EBground;

    List<int> order = new List<int>();
    public List<GameObject> Order = new List<GameObject>();
    public int turn = 0; //전투 스크립트에서 관리

    private BattleLoader battleLoader;
    private Animator UIAni;

    private BattleCameraController battleCameraController;

    private void Awake()
    {
        battleLoader = FindObjectOfType<BattleLoader>();
        battleCameraController = FindObjectOfType<BattleCameraController>();
        TryGetComponent(out UIAni);
    }
    private void Start()
    {
        SetOrder();
        SetUI();
    }
    public void SetOrder()
    {
        GameObject temp = null;

        order.Clear();
        Order.Clear();

        int j = 0;
        for (int i = 0; i < battleLoader.Players.Count + battleLoader.Enemys.Count; i++)
        {
            if (battleLoader.Players.Count - 1 < i)
            {
                order.Add(battleLoader.Enemys[j].GetComponent<EnemyStat>().speed);
                Order.Add(battleLoader.Enemys[j]);

                j++;
                continue;
            }
            order.Add(battleLoader.Players[i].GetComponent<PlayerStat>().speed);
            Order.Add(battleLoader.Players[i]);
        }

        for (int i = 0; i < order.Count; i++)
        {
            for (int l = i + 1; l < order.Count; l++)
            {
                if (order[i] <= order[l])
                {
                    temp = Order[i];
                    Order[i] = Order[l];
                    Order[l] = temp;
                }
            }
        }
    }
    private void SetUI()
    {
        //단이가 초상화 심어주면 이미지 설정하기 구현
        //적 스탯 구현 후 적 이미지 심기 설정
        turn++;

        if (Order.Count - 1 < turn)
        {
            turn = 0;
        }

        for (int i = turn; i < portrait.Length + turn; i++)
        {
            int j = i % Order.Count;

            if (Order[j].GetComponent<PlayerStat>() != null)
            {
                portrait[i-turn].sprite = Order[j].GetComponent<PlayerStat>().portrait;
                background[i-turn].sprite = PBground;
            }
            else
            {
                portrait[i-turn].sprite = Order[j].GetComponent<EnemyStat>().portrait;
                background[i- turn].sprite = EBground;
            }
        }
        //카메라 돌리기
        if (Order[turn].GetComponent<PlayerStat>() != null)
        {
            battleCameraController.PlayerTurnCamera();
        }
        else
        {
            battleCameraController.EnemyTurnCamera();
        }
    }
    public void TurnChange()
    {
        StartCoroutine(Ani_co());
        SetUI();

    }
    private IEnumerator Ani_co()
    {
        UIAni.SetBool("TurnOn", true);
        yield return new WaitForSeconds(2f);
        UIAni.SetBool("TurnOn", false);
    }
}
