using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
    public List<GameObject> Players;
    public List<GameObject> Enemys;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject fieldUI;

    Vector3 defaultPos;
    int enemyZPos = -98;

    [SerializeField] int EnemyNum;

    private void Start()
    {
        defaultPos = new Vector3(-103.5f, 0, -11f);

        Invoke("PrefsInstantiate", 1f); //테스트
    }
    private void PrefsInstantiate() //전투 돌입 시 실행할 함수
    {
        for (int i = 0; i < GameManager.instance.Players.Length; i++)
        {
            Players.Add(Instantiate(GameManager.instance.Players[i], defaultPos, Quaternion.Euler(new Vector3(0, 90, 0))));
        }
        if (GameManager.instance.Players.Length == 2)
        {
            Players[0].transform.position += Vector3.back;
            Players[1].transform.position += Vector3.forward;
        }
        if (GameManager.instance.Players.Length == 3)
        {
            Players[0].transform.position += Vector3.back * 2;
            Players[2].transform.position += Vector3.forward * 2;
        }

        battleUI.SetActive(true);
        fieldUI.SetActive(false);
    }
    private void PrefsDestroy()
    {
        for(int i = 0; i<Players.Count; i++)
        {
            Destroy(Players[i]);
        }
        Players.Clear();
    }
}
