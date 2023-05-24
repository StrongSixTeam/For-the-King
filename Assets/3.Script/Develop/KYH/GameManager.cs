using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int turnNum = 0;

    private QuestManager questManager;

    private CameraController cameraController;
    private MoveSlot moveSlot;

    public bool isMoveSlot = false;

    private void Start()
    {
        Players = new GameObject[PlayerPrefs.GetInt("PlayerCnt")];
        for (int i = 0; i < PlayerPrefs.GetInt("PlayerCnt"); i++)
        {
            Players[i] = GameObject.FindGameObjectsWithTag("Player")[i];
        }

        questManager = FindObjectOfType<QuestManager>();
        cameraController = FindObjectOfType<CameraController>();
        moveSlot = FindObjectOfType<MoveSlot>();

        questManager.PopUp(questManager.questTurn);

        MainPlayer = Players[turnNum];
    }

    private void Update()
    {
        if (questManager.isQuest)
        {
            isMoveSlot = false;
        }
        if (!questManager.isQuest && !isMoveSlot)
        {
            isMoveSlot = true;
            TurnChange();
        }
    }

    public void TurnChange()
    {
        MainPlayer = Players[turnNum];
        cameraController.PlayerChange();

        moveSlot.player = MainPlayer.GetComponent<PlayerStat>();
        moveSlot.SetMove();

        SlotController.instance.OnClick();

        turnNum++;
        if(turnNum >= Players.Length)
        {
            turnNum = 0;
        }
    }
}
