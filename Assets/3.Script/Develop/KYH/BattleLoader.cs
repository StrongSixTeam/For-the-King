using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> Players;
    [SerializeField] private List<GameObject> Enemys;

    Vector3 defaultPos;
    int enemyZPos = -98;

    [SerializeField] int EnemyNum;

    private void Start()
    {
        defaultPos = new Vector3(-103.5f, 0, -11f);

        Invoke("PrefsInstantiate", 1f);
    }
    private void PrefsInstantiate() //���� ���� �� ������ �Լ�
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
