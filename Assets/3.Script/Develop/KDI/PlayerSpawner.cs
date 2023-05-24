using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;

    [Header("플레이어 모델 프리팹")]
    [SerializeField] private GameObject blackSmith;
    [SerializeField] private GameObject hunter;
    [SerializeField] private GameObject scholar;


    private void Awake()
    {
        int n = PlayerPrefs.GetInt("PlayerCnt");
        for (int i = 0; i < n; i++)
        {
            if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("대장장이"))
            {
                GameObject player = Instantiate(blackSmith, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1;  //순서 넣어주기
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("사냥꾼"))
            {
                GameObject player = Instantiate(hunter, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1; 
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("학자"))
            {
                GameObject player = Instantiate(scholar, startPos, Quaternion.identity);
                player.AddComponent<PlayerController>();
                player.GetComponent<PlayerController>().order = i+1; 
            }
            else
            {
                Debug.Log("직업 비교 실패");
            }
        }
    }
}
