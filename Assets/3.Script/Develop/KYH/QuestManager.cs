using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("����Ʈ �˾� â ����")]
    [SerializeField] private Text NPCName;
    [SerializeField] private Text NPCInfo;
    [SerializeField] private Text sentence;
    [SerializeField] private Image NPCimg;

    [Header("����Ʈ ����Ʈ â ����")]
    [SerializeField] private Text questListTitle;
    [SerializeField] private Text questListSentence;

    [Header("����Ʈ UI")]
    [SerializeField] private GameObject questPopUpUI;
    [SerializeField] private GameObject questCheckUI;
    [SerializeField] private GameObject questListUI;

    [Header("����Ʈ �ؽ�Ʈ ����")]
    [SerializeField] private QuestText[] questText;
    [SerializeField] private QuestText[] clearQuestText;

    [Header("����Ʈ �˾� ���� ����")]
    public bool isQuest = false;
    private bool isClear = false;
    private bool isChaosLoss = false;

    private int questTurn = 0;
    private int MaxTurn = 11;
    private string questName;

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        if (!questPopUpUI.activeSelf && questTurn != 0 && questTurn != 2 && questTurn != 5 && questTurn != 8 && questTurn != 11)
        {
            if (questTurn == 1) //ù��° ����Ʈ
            {
                questListUI.SetActive(true);
                PopUp(questTurn);
            }
            if (questTurn == 3 || questTurn == 4) //�ι�° ����Ʈ
            {
                PopUp(questTurn);
            }
            if (questTurn == 6 || questTurn == 7) //����° ����Ʈ
            {
                PopUp(questTurn);
            }
            if(questTurn == 10)
            {
                PopUp(questTurn);
            }
        }
        if (!questCheckUI.activeSelf && !questPopUpUI.activeSelf && isQuest && !isChaosLoss)
        {
            if (questTurn == 2 || questTurn == 5 || questTurn == 8 || questTurn != 11)
            {
                Camera.main.transform.SetParent(cameraController.MainPlayer.transform);
                StartCoroutine(cameraController.CameraSoftMove());

                if (questTurn == 5)
                {
                    questCheckUI.GetComponentInChildren<Text>().text = "������ ������� ��ǥ �Ϸ��ϱ�";
                }
                if (questTurn == 2 || questTurn == 8 || questTurn == 11)
                {
                    questCheckUI.GetComponentInChildren<Text>().text = questText[questTurn - 1].questListSentence;
                    //questCheckUI.GetComponentInChildren<Text>().text.Replace("\n", "");
                }

                questCheckUI.SetActive(true);
            }
        }
        if (!questPopUpUI.activeSelf && isChaosLoss)
        {
            PopUp("ChaosLoss");
            questName = "";
            isChaosLoss = false;
        }
    }
    private void QuestSet(int num)
    {
        questText[num].sentence = questText[num].sentence.Replace("\\n", "\n");

        NPCName.text = questText[num].NPCName;
        NPCInfo.text = questText[num].NPCInfo;
        sentence.text = questText[num].sentence;
        NPCimg.sprite = questText[num].NPCImg;
        if (questText[num].questPos != "")
        {
            Camera.main.transform.SetParent(GameObject.FindGameObjectWithTag(questText[num].questPos).transform);
        }
    }
    private void QuestSet(string questName)
    {
        for (int i = 0; i < clearQuestText.Length; i++)
        {
            if (questName.Equals(clearQuestText[i].QuestName))
            {
                clearQuestText[i].sentence = clearQuestText[i].sentence.Replace("\\n", "\n");

                NPCName.text = clearQuestText[i].NPCName;
                NPCInfo.text = clearQuestText[i].NPCInfo;
                sentence.text = clearQuestText[i].sentence;
                NPCimg.sprite = clearQuestText[i].NPCImg;

                isChaosLoss = clearQuestText[i].isChaosLoss;
            }
        }
    }
    private void QuestListUp(int num)
    {
        if (num >= MaxTurn)
        {
            return;
        }

        questText[num].questListSentence = questText[num].questListSentence.Replace("\\n", "\n");

        if (questText[num].questListTitle != "")
        {
            questListTitle.text = questText[num].questListTitle;
            questListSentence.text = questText[num].questListSentence;
        }
        else
        {
            if (questText[num].questListSentence != "")
            {
                questListSentence.text = questListSentence.text + questText[num].questListSentence;
            }
        }
    }
    private void QuestListUp(string questName)
    {
        for (int i = 0; i < clearQuestText.Length; i++)
        {
            if (questName.Equals(clearQuestText[i].QuestName))
            {
                questListSentence.text = questListSentence.text.Replace(clearQuestText[i].questListSentence, "�Ϸ�!");
            }
        }
    }

    public void PopUp(int turn)
    {
        QuestSet(turn);
        StartCoroutine(cameraController.CameraSoftMove());
        questPopUpUI.SetActive(true);
        isQuest = true;
    }
    public void PopUp(string questName)
    {
        this.questName = questName;
        QuestSet(questName);
        StartCoroutine(cameraController.CameraSoftMove());
        questPopUpUI.SetActive(true);
        isClear = true;
        isQuest = true;
    }

    public void PopClose()
    {
        if (isClear)
        {
            if (questPopUpUI.activeSelf)
            {
                QuestListUp(questName);
                questPopUpUI.SetActive(false);
            }
            if (!isChaosLoss)
            {
                isClear = false;
                isQuest = false;
            }
            return;
        }
        else
        {
            if (questPopUpUI.activeSelf)
            {
                QuestListUp(questTurn);
                questPopUpUI.SetActive(false);
                questTurn++;
            }
            if (questCheckUI.activeSelf)
            {
                questCheckUI.SetActive(false);
                isQuest = false;
            }
        }
    }

    //�׽�Ʈ��, �Լ� ȣ�� ����

    public void TestMain()
    {
        PopUp(questTurn);
    }
    public void TestClear1()
    {
        PopUp("WoodSmoke");
    }
    public void TestClear2()
    {
        PopUp("ChaosBoss");
    }
    public void TestClear3()
    {
        PopUp("God");
    }
    public void TestClear4()
    {
        PopUp("ShinyCave");
    }
    public void TestClear5()
    {
        PopUp("Parid");
    }
    public void TestClear6()
    {
        PopUp("Corpse");
    }
}
