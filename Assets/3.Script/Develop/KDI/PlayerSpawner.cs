using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 startPos;
    public int startIndex;

    AstsrPathfinding astsrPathfinding;

    [Header("�÷��̾� �� ������")]
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
        
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++) //�÷��� �ϴ� ĳ���� ����ŭ �ݺ� ����
        {
            GameObject player;
            int x; 
            if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��������"))
            {
                player = Instantiate(blackSmith, startPos, Quaternion.identity);
                x = 0;
            }
            else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��ɲ�"))
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
            player.GetComponent<PlayerStat>().name = PlayerPrefs.GetString(string.Format("Name{0}", i)); //�̸� ����
            player.GetComponent<PlayerStat>().order = i; //���° �÷��̾�����
            player.GetComponent<PlayerStat>().SetStat(characterStatusSets[x]);

            player.GetComponent<PlayerController_Jin>().myHexNum = startIndex;
            astsrPathfinding.SetPlayer(i, player.GetComponent<PlayerController_Jin>());
        }

    }


}
