using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 startPos;
    public int startIndex;

    AstsrPathfinding astsrPathfinding;

    [Header("플레이어 모델 프리팹")]
    [SerializeField] private GameObject blackSmith;
    [SerializeField] private GameObject hunter;
    [SerializeField] private GameObject scholar;

    [SerializeField] private List<CharacterStatusSet> characterStatusSets;

    private void Awake()
    {
        startIndex = FindObjectOfType<MapObjectCreator>().objectIndex[0];
        startPos = FindObjectOfType<HexMapCreator>().hexMembers[startIndex].transform.position + new Vector3(0f, 0.5f, 0f);

        astsrPathfinding = FindObjectOfType<AstsrPathfinding>();
        astsrPathfinding.SetPlayerCount(PlayerPrefs.GetInt("PlayerCnt"));
    }

    private void Start()
    {
        
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++) //플레이 하는 캐릭터 수만큼 반복 생성
        {
            GameObject player;
            int x; 
            if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("대장장이"))
            {
                player = Instantiate(blackSmith, startPos, Quaternion.identity);
                x = 0;
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("사냥꾼"))
            {
                player = Instantiate(hunter, startPos, Quaternion.identity);
                x = 1;
            }
            else
            {
                player = Instantiate(scholar, startPos, Quaternion.identity);
                x = 2;
            }
            player.AddComponent<PlayerStat>();
            player.GetComponent<PlayerStat>().name = PlayerPrefs.GetString(string.Format("Name{0}", i)); //이름 연결
            player.GetComponent<PlayerStat>().order = i; //몇번째 플레이어인지
            player.GetComponent<PlayerStat>().SetStat(characterStatusSets[x]);

            player.GetComponent<PlayerController_Jin>().myHexNum = startIndex;
            astsrPathfinding.SetPlayer(i, player.GetComponent<PlayerController_Jin>());
        }

    }


}
