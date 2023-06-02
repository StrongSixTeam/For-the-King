using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public int maxLife = 5; //생명 슬롯 창 개수
    public int currentLife = 5; //현재 생명 개수

    public GameObject[] Players;
    public GameObject MainPlayer;
    public int nextTurn = 0;

    private QuestManager questManager;
    private PlayerController_Jin playerController;
    private PlayerSpawner playerSpawner;

    private CameraController cameraController;
    private MoveSlot moveSlot;
    private TimeBarScrolling[] timeBarScrolling;

    public bool isQuestFinish = false;
    public bool isSettingDone = false;
    public bool isTrunChange = false;
    public bool isBlock = false;

    private bool isFisrtTurn = true;

    public Button turnChageBtn;
    [SerializeField] GameObject[] movingUIs;
    [SerializeField] GameObject[] portraitUIs;
    private EncounterManager encounterManager;

    private string MainPlayerName;
    public GameObject SpeedSlot;
    [SerializeField] Text Name;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        cameraController = FindObjectOfType<CameraController>();
        moveSlot = FindObjectOfType<MoveSlot>();
        timeBarScrolling = FindObjectsOfType<TimeBarScrolling>();
        encounterManager = FindObjectOfType<EncounterManager>();
    }
    public void Setting()
    {
        Players = new GameObject[PlayerPrefs.GetInt("PlayerCnt")];

        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++)
        {
            Players[i] = GameObject.FindGameObjectsWithTag("Player")[i];
            playerSpawner = FindObjectOfType<PlayerSpawner>();
            playerSpawner.movingUIs[i].Player = Players[i].transform;
            movingUIs[i] = playerSpawner.movingUIs[i].gameObject;
            movingUIs[i].transform.GetChild(0).gameObject.SetActive(false);
            portraitUIs[i].GetComponent<PortraitUI>().Player = Players[i].transform;
        }

        for (int i = 2; i > PlayerPrefs.GetInt("PlayerCnt")-1; i--)
        {
            portraitUIs[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        questManager.PopUp(questManager.questTurn);
        MainPlayer = Players[nextTurn];
        playerController = MainPlayer.GetComponent<PlayerController_Jin>();

        cameraController.PlayerChange();
        isSettingDone = true;
    }
    private void Update()
    {
        if (playerController != null)
        {
            if (questManager.isQuest || playerController.isRun || SlotController.instance.isSlot)
            {
                isBlock = true;
                turnChageBtn.interactable = false;
            }
            else if (!questManager.isQuest && !playerController.isRun)
            {
                if (encounterManager.isEncounterUI)
                { //Encounter UI가 떠있으면 바닥 누르는게 안되게
                    isBlock = true;
                }
                else if(Camera.main != null)
                {
                    isBlock = false;
                }
                else if(Camera.main == null)
                {
                    isBlock = true;
                }
                turnChageBtn.interactable = true;
            }

            if (!questManager.isQuest && !isQuestFinish && Players.Length > 0)
            {
                isQuestFinish = true;
                TurnChange();
            }
        }
    }
    public void TurnChange()
    {
        MainPlayerName = MainPlayer.GetComponent<PlayerStat>().name;

        if (nextTurn == 0 && !isFisrtTurn)
        {
            for (int i = 0; i < timeBarScrolling.Length; i++)
            {
                timeBarScrolling[i].TimeFlow();
            }
        }

        if (encounterManager.isEncounterUI) //Encounter UI가 떠있는 상태로 턴 종료하면
        {
            encounterManager.DisableButton();
        }

        ActiveMovingUI(nextTurn);

        isFisrtTurn = false;

        isTrunChange = true;

        MainPlayer = Players[nextTurn];
        playerController = MainPlayer.GetComponent<PlayerController_Jin>();

        cameraController.PlayerChange();

        moveSlot.player = MainPlayer.GetComponent<PlayerStat>();
        moveSlot.SetMove();

        MainPlayerName = MainPlayer.GetComponent<PlayerStat>().name;
        Name.text = MainPlayerName + "의 턴";

        SpeedSlot.SetActive(true);
        SlotController.instance.OnClick();

        nextTurn++;
        if (nextTurn >= Players.Length)
        {
            nextTurn = 0;
        }
    }


    private void ActiveMovingUI(int n)
    {
        movingUIs[0].GetComponent<MovingUI>().ResetText();

        StartCoroutine(test(n));
    }

    IEnumerator test(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != n)
            {
                movingUIs[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(2.05f);
        for (int i = 0; i < 3; i++)
        {
            if (i == n)
            {
                movingUIs[i].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
    }

    public void ActivePortrait()
    {
        int nowTurn = 0;
        if (PlayerPrefs.GetInt("PlayerCnt") == 1)
        {
            nowTurn = 0;
        }
        else
        {
            nowTurn = nextTurn - 1;
            if (nowTurn < 0)
            {
                nowTurn = PlayerPrefs.GetInt("PlayerCnt") -1;
            }
        }
        portraitUIs[nowTurn].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DeactivePortrait()
    {
        int nowTurn = 0;
        if (PlayerPrefs.GetInt("PlayerCnt") == 1)
        {
            nowTurn = 0;
        }
        else
        {
            nowTurn = nextTurn - 1;
            if (nowTurn < 0)
            {
                nowTurn = PlayerPrefs.GetInt("PlayerCnt") - 1;
            }
        }
        portraitUIs[nowTurn].transform.GetChild(0).gameObject.SetActive(false);
    }
}

