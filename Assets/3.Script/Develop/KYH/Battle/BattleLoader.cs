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

    Vector3 playerPos;
    Vector3 enemyPos;

    private BattleCameraController battleCameraController;

    private void Awake()
    {
        battleCameraController = FindObjectOfType<BattleCameraController>();
    }
    private void Start()
    {
        playerPos = new Vector3(-103.5f, 0, -11f);
        enemyPos = new Vector3(-98f, 0, -11f);

        Invoke("PrefsInstantiate", 1f); //�׽�Ʈ
    }
    private void PrefsInstantiate() //���� ���� �� ������ �Լ�
    {
        //�÷��̾� ��ȯ
        for (int i = 0; i < GameManager.instance.Players.Length; i++)
        {
            Players.Add(Instantiate(GameManager.instance.Players[i], playerPos, Quaternion.Euler(new Vector3(0, 90, 0))));
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<PlayerController_Jin>().enabled = false;
            Players[i].transform.GetChild(0).gameObject.SetActive(true);
            Players[i].transform.GetChild(1).gameObject.SetActive(true);
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
        //�� ��ȯ
        for (int i = 0; i < 3; i++) //���ʹ� ���� �ʿ� > enemys.count �޾ƾ���
        {
            Enemys.Add(Instantiate(Enemys[i], enemyPos, Quaternion.Euler(new Vector3(0, 270, 0)))); //���ʹ� ���� �ʿ�
        }
        Enemys.RemoveAt(0); //����
        if (Enemys.Count == 2)
        {
            Enemys[0].transform.position += Vector3.back;
            Enemys[1].transform.position += Vector3.forward;
        }
        if (Enemys.Count == 3)
        {
            Enemys[0].transform.position += Vector3.back * 2;
            Enemys[2].transform.position += Vector3.forward * 2;
        }

        battleUI.SetActive(true);
        fieldUI.SetActive(false);
    }

    private void OnDisable()
    {
        //PrefsDestroy();
    }
    private void PrefsDestroy()
    {
        for(int i = 0; i<Players.Count; i++)
        {
            Destroy(Players[i]);
            Destroy(Enemys[i]);
        }
        Players.Clear();
        Enemys.Clear();
    }
}
