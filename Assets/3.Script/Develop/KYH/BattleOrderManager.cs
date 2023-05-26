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

    private BattleLoader battleLoader;

    private void Awake()
    {
        battleLoader = FindObjectOfType<BattleLoader>();
    }
    private void OnEnable()
    {
        SetUI();
    }
    private void SetOrder()
    {
        GameObject temp = null;

        int j = 0;
        for (int i = 0; i < battleLoader.Players.Count/* + battleLoader.Enemys.Count */; i++)
        {
            if (battleLoader.Players.Count - 1 < i)
            {
                //order.Add(battleLoader.Enemys[j].GetComponent<EnemyStat>().speed);
                //Order.Add(battleLoader.Enemys[j]);

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
                if (order[i] < order[l])
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
        //���̰� �ʻ�ȭ �ɾ��ָ� �̹��� �����ϱ� ����
        //�� ���� ���� �� �� �̹��� �ɱ� ����

        SetOrder();

        for (int i = 0; i < portrait.Length; i++)
        {
            int temp = i;
            if (Order.Count - 1 < temp)
            {
                temp = 0;
            }
            if (Order[temp].GetComponent<PlayerStat>() != null)
            {
                //portrait[i].sprite = Order[temp].GetComponent<PlayerStat>().�ʻ�ȭ����;
                background[i].sprite = PBground;
            }
            else
            {
                //portrait[i].sprite = Order[tempj].GetComponent<EnemyStat>().�ʻ�ȭ����;
                background[i].sprite = EBground;
            }
        }
    }
}
