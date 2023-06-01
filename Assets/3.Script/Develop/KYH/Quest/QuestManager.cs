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

    [Header("����Ʈ ����")]
    public int questTurn = 0;
    public string questName;
    private int MaxTurn = 11;

    [Header("����")]
    [SerializeField] MapObjectCreator mapObjectCreator;
    [SerializeField] CloudBox cloudBox;
    private int mapHexIndex=0;

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        StartCoroutine(LateFindObjectCo());
    }

    IEnumerator LateFindObjectCo()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        mapObjectCreator = FindObjectOfType<MapObjectCreator>();
        cloudBox = FindObjectOfType<CloudBox>();
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
                cameraController.targetPos = GameManager.instance.MainPlayer.transform.position + cameraController.defaultPos;
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
            cameraController.targetPos = GameObject.FindGameObjectWithTag(questText[num].questPos).transform.position + cameraController.defaultPos;
            StartCoroutine(cameraController.CameraSoftMove());
            //���� ��Ȱ��ȭ
            switch (questTurn)
            {
                //1=��彺��ũ, 3=�����ǽĵ���, 4=ī������θӸ�, 7=���νű���, 8=�и���, 10=��ü�����Ͻ�
                case 1:
                    mapHexIndex = 1;
                    break;
                case 3:
                    mapHexIndex = 2;
                    mapObjectCreator.ShowObject(0);
                    //bool�� true�� �ٲٱ� -> �浹 ����
                    break;
                case 4:
                    mapHexIndex = 3;
                    mapObjectCreator.ShowObject(1);
                    break;
                case 7:
                    mapHexIndex = 4;
                    mapObjectCreator.ShowObject(2);
                    break;
                case 8:
                    mapHexIndex = 5;
                    break;
                case 10:
                    mapHexIndex = 8;
                    mapObjectCreator.ShowObject(3);
                    break;

            }
            //����Ʈ ������Ʈ�� Ȱ��ȭ�ϰ� ������ ġ������
            cloudBox.CloudActiveFalse(mapObjectCreator.objectIndex[mapHexIndex]);
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
        questPopUpUI.SetActive(true);
        isQuest = true;
    }
    public void PopUp(string questName)
    {
        this.questName = questName;
        QuestSet(questName);
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

    //public void TestMain()
    //{
    //    PopUp(questTurn);
    //}
    //public void TestClear1()
    //{
    //    PopUp("WoodSmoke");
    //}
    //public void TestClear2()
    //{
    //    PopUp("ChaosBoss");
    //}
    //public void TestClear3()
    //{
    //    PopUp("God");
    //}
    //public void TestClear4()
    //{
    //    PopUp("ShinyCave");
    //}
    //public void TestClear5()
    //{
    //    PopUp("Parid");
    //}
    //public void TestClear6()
    //{
    //    PopUp("Corpse");
    //}
}
