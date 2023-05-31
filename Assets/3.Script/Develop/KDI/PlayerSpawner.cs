using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 startPos;
    public int startIndex;

    AstsrPathfinding astsrPathfinding;

    [Header("플레이어 모델 프리팹")]
    [SerializeField] private GameObject blackSmith;
    [SerializeField] private GameObject hunter;
    [SerializeField] private GameObject scholar;

    [Header("플레이어 UI")]
    [SerializeField] private PlayerUI[] playerUI;
    private PlayerUI[] playerUIs;

    [Header("탑바 Portrait")]
    [SerializeField] private Image[] TopbarPortraits;

    [Header("캐릭터 스탯")]
    [SerializeField] private List<CharacterStatusSet> characterStatusSets;

    private void Awake()
    {
        startIndex = FindObjectOfType<MapObjectCreator>().objectIndex[0];
        startPos = FindObjectOfType<HexMapCreator>().hexMembers[startIndex].transform.position + new Vector3(0f, 0.1f, 0f);

        astsrPathfinding = FindObjectOfType<AstsrPathfinding>();
        astsrPathfinding.SetPlayerCount(PlayerPrefs.GetInt("PlayerCnt"));

        playerUI = FindObjectsOfType<PlayerUI>();
        playerUIs = FindObjectsOfType<PlayerUI>();

        TopbarPortraits = new Image[3];

        for (int i = 0; i < 3; i++)
        {
            if (playerUI[i].order == 0)
            {
                playerUIs[0] = playerUI[i];
            }
            else if (playerUI[i].order == 1)
            {
                playerUIs[1] = playerUI[i];
            }
            else if (playerUI[i].order == 2)
            {
                playerUIs[2] = playerUI[i];
            }
        }
        playerUIs[0].gameObject.SetActive(false);
        playerUIs[1].gameObject.SetActive(false);
        playerUIs[2].gameObject.SetActive(false);

        TopbarPortraits[0] = GameObject.Find("First").GetComponentsInChildren<Image>()[1];
        TopbarPortraits[1] = GameObject.Find("Second").GetComponentsInChildren<Image>()[1];
        TopbarPortraits[2] = GameObject.Find("Third").GetComponentsInChildren<Image>()[1];

        TopbarPortraits[0].transform.parent.gameObject.SetActive(false);
        TopbarPortraits[1].transform.parent.gameObject.SetActive(false);
        TopbarPortraits[2].transform.parent.gameObject.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++) //플레이 하는 캐릭터 수만큼 모델 반복 생성
        {
            playerUIs[i].gameObject.SetActive(true); //해당 UI도 켜주기
            TopbarPortraits[2 - i].transform.parent.gameObject.SetActive(true);

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

            TopbarPortraits[2 - (PlayerPrefs.GetInt("PlayerCnt") - 1) + i].sprite = player.GetComponent<PlayerStat>().portrait;

            player.GetComponent<PlayerController_Jin>().myHexNum = startIndex;
            astsrPathfinding.SetPlayer(i, player.GetComponent<PlayerController_Jin>());
        }
        GameManager.instance.Setting();
    }


}
