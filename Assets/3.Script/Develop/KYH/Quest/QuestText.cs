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

    public string questPos; //오브젝트 태그 이름 적기, 플레이어면 빈칸으로 두기

    public string questListTitle;
    public string questListSentence;

    public bool isChaosLoss = false;
}
