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

    public List<int> order = new List<int>();
    public List<GameObject> Order = new List<GameObject>();
    public int turn = -1; //전투 스크립트에서 관리

    private BattleLoader battleLoader;
    private Animator UIAni;

    private BattleCameraController battleCameraController;
    GlowControl glowControl;

    private void Awake()
    {
        battleLoader = FindObjectOfType<BattleLoader>();
        glowControl = FindObjectOfType<GlowControl>();
        TryGetComponent(out UIAni);
    }
    private void OnEnable()
    {
        battleCameraController = FindObjectOfType<BattleCameraController>();

        turn = -1;

        SetOrder();
        TurnChange();
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
                if (order[i] < order[l])
                {
                    temp = Order[i];
                    int o = order[i];
                    Order[i] = Order[l];
                    order[i] = order[l];
                    Order[l] = temp;
                    order[l] = o;
                }
            }
        }
    }
    private void SetUI()
    {
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
                portrait[i - turn].sprite = Order[j].GetComponent<PlayerStat>().portrait;
                background[i - turn].sprite = PBground;
                //glowControl.SetTurnGlow(turn); //현재 턴인 플레이어를 가리켜야 합니다 두목님
            }
            else
            {
                portrait[i - turn].sprite = Order[j].GetComponent<EnemyStat>().portrait;
                background[i - turn].sprite = EBground;
                glowControl.SetTurnGlow(-1);
            }
        }
        for(int i = 0; i < battleLoader.Players.Count; i++)
        {
            battleLoader.Players[i].transform.GetChild(2).gameObject.SetActive(false);
        }
        for (int i = 0; i < battleLoader.Enemys.Count; i++)
        {
            battleLoader.Enemys[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Order[turn].GetComponent<PlayerStat>() != null)
        {
            glowControl.SetTurnGlow(Order[turn].GetComponent<PlayerStat>().order);
            Order[turn].transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            Order[turn].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void TurnChange()
    {
        StartCoroutine(Ani_co());
        SetUI();

        if (!battleLoader.isIng)
        {
            StartCoroutine(StartAni_co());
            
            if (battleCameraController.gameObject.name == "BattleCamera")
            {
                Invoke("CameraChange", 2f);
                Debug.Log("배틀 카메라 상태");
            }
            else
            {
                Invoke("CameraChange", 5f);
                Debug.Log("동굴 카메라 상태");
            }

            battleLoader.isIng = true;
        }
        else
        {
            CameraChange();
        }
    }
    private void CameraChange()
    {
        if (Order[turn].GetComponent<PlayerStat>() != null)
        {
            battleCameraController.PlayerTurnCamera();
        }
        else
        {
            battleCameraController.EnemyTurnCamera();
        }
    }
    private IEnumerator StartAni_co()
    {
        for (int i = 0; i < battleLoader.Enemys.Count; i++)
        {
            battleLoader.Enemys[i].GetComponent<Animator>().SetTrigger("Battlein");
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < battleLoader.Enemys.Count; i++)
        {
            battleLoader.Enemys[i].GetComponent<Animator>().SetTrigger("Idle");
        }
    }
    private IEnumerator Ani_co()
    {
        UIAni.SetBool("TurnOn", true);
        yield return new WaitForSeconds(2f);
        UIAni.SetBool("TurnOn", false);
    }
    public void End()
    {
        order.Clear();
        Order.Clear();
        turn = 0;
    }
}
