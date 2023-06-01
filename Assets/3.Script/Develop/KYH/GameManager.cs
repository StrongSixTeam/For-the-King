using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
    }
    #endregion

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


    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        cameraController = FindObjectOfType<CameraController>();
        moveSlot = FindObjectOfType<MoveSlot>();
        timeBarScrolling = FindObjectsOfType<TimeBarScrolling>();
        
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
                isBlock = false;
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
        
        if (nextTurn == 0 && !isFisrtTurn)
        {
            for (int i = 0; i < timeBarScrolling.Length; i++)
            {
                timeBarScrolling[i].TimeFlow();
            }
        }

        ActiveMovingUI(nextTurn);

        isFisrtTurn = false;

        isTrunChange = true;

        MainPlayer = Players[nextTurn];
        playerController = MainPlayer.GetComponent<PlayerController_Jin>();

        cameraController.PlayerChange();

        moveSlot.player = MainPlayer.GetComponent<PlayerStat>();
        moveSlot.SetMove();

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

}

