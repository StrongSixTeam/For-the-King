using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLine", menuName = "QuestText")]
public class QuestText : ScriptableObject
{
    public string QuestName;
    
    public string NPCName;
    public string NPCInfo;
    public Sprite NPCImg;
    public string sentence;

    public string questPos; //������Ʈ �±� �̸� ����, �÷��̾�� ��ĭ���� �α�

    public string questListTitle;
    public string questListSentence;

    public bool isChaosLoss = false;
}
