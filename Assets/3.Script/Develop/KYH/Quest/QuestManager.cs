using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public bool isClear = false;
    private bool isChaosLoss = false;

    [Header("����Ʈ ����")]
    public int questTurn = 0;
    public string questName;
    private int MaxTurn = 11;

    [Header("����")]
    [SerializeField] MapObjectCreator mapObjectCreator;
    [SerializeField] CloudBox cloudBox;
    private int mapHexIndex=0;
    private GlowControl glowControl;

    private CameraController cameraController;

    public int questClearCnt = 0;
    public bool isChaos = false;
    public bool isShinyCave = false;

    public bool is2nd = false;
    public bool is3rd = false;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        glowControl = FindObjectOfType<GlowControl>();
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
        if (!questPopUpUI.activeSelf && questTurn != 0 && questTurn != 2 && questTurn != 5 && questTurn != 8 && questTurn != 12)
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
            if (questTurn == 2 || questTurn == 5 || questTurn == 8 || questTurn == 9 || questTurn == 11)
            {
                cameraController.targetPos = GameManager.instance.MainPlayer.transform.position + cameraController.defaultPos;
                StartCoroutine(cameraController.CameraSoftMove());

                if (questTurn == 5)
                {
                    questCheckUI.GetComponentInChildren<Text>().text = "������ ������� ��ǥ �Ϸ��ϱ�";
                }
                if (questTurn == 2 || questTurn == 8 || questTurn == 9 || questTurn == 11)
                {
                    questCheckUI.GetComponentInChildren<Text>().text = questText[questTurn - 1].questListSentence;
                    questCheckUI.GetComponentInChildren<Text>().text.Replace(Environment.NewLine, "");
                }

                questCheckUI.SetActive(true);
            }
        }
        if (questClearCnt == 2 && !is2nd)
        {
            questTurn = 6;
            is2nd = true;
        }
        if (questClearCnt == 3 && !is3rd)
        {
            questTurn = 8;
            is3rd = true;
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
                    glowControl.SetQuestObjectGlow(0, true);
                    break;
                case 3:
                    mapHexIndex = 2;
                    mapObjectCreator.ShowObject(0);
                    //bool�� true�� �ٲٱ� -> �浹 ����
                    FindObjectOfType<EncounterManager>().encounter[2].isShowed = true;
                    glowControl.SetQuestObjectGlow(1, true);
                    break;
                case 4:
                    mapHexIndex = 3;
                    mapObjectCreator.ShowObject(1);
                    FindObjectOfType<EncounterManager>().encounter[3].isShowed = true;
                    glowControl.SetQuestObjectGlow(2, true);
                    break;
                case 7:
                    mapHexIndex = 4;
                    mapObjectCreator.ShowObject(2);
                    FindObjectOfType<EncounterManager>().encounter[4].isShowed = true;
                    glowControl.SetQuestObjectGlow(3, true);
                    break;
                case 8:
                    mapHexIndex = 5;
                    glowControl.SetQuestObjectGlow(4, true);
                    break;
                case 10:
                    mapHexIndex = 8;
                    mapObjectCreator.ShowObject(3);
                    FindObjectOfType<EncounterManager>().encounter[8].isShowed = true;
                    glowControl.SetQuestObjectGlow(5, true);
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

                if (is3rd)
                {
                    PopUp(questTurn);
                }
            }
            if (is2nd)
            {
                PopUp(questTurn);
            }
            if (is3rd)
            {
                PopUp(questTurn);
                questClearCnt++;
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
}
