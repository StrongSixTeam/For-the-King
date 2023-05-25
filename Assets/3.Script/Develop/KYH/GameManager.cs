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

    private CameraController cameraController;
    private MoveSlot moveSlot;

    public bool isQuestFinish = false;
    public bool isSettingDone = false;
    public bool isTrunChange = false;
    public bool isBlock = false;

    public Button turnChageBtn;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        cameraController = FindObjectOfType<CameraController>();
        moveSlot = FindObjectOfType<MoveSlot>();
    }
    public void Setting()
    {
        Players = new GameObject[PlayerPrefs.GetInt("PlayerCnt")];

        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++)
        {
            Players[i] = GameObject.FindGameObjectsWithTag("Player")[i];
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
}
