using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;

    [Header("�÷��̾� �� ������")]
    [SerializeField] private GameObject blackSmith;
    [SerializeField] private GameObject hunter;
    [SerializeField] private GameObject scholar;


    private void Awake()
    {
        int n = PlayerPrefs.GetInt("PlayerCnt");
        for (int i = 0; i < n; i++)
        {
            if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��������"))
            {
                GameObject player = Instantiate(blackSmith, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1;  //���� �־��ֱ�
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��ɲ�"))
            {
                GameObject player = Instantiate(hunter, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1; 
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("����"))
            {
                GameObject player = Instantiate(scholar, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1; 
            }
            else
            {
                Debug.Log("���� �� ����");
            }
        }
    }
}
