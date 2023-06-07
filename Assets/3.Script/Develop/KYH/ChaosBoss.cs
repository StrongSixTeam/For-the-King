using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBoss : MonoBehaviour
{
    private void OnDisable()
    {
        if (FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2 && FindObjectOfType<QuestManager>().isChaos)
        {
            FindObjectOfType<QuestManager>().PopUp("ChaosBoss");

            FindObjectOfType<QuestManager>().questClearCnt++;

            FindObjectOfType<QuestManager>().isChaos = false;

            FindObjectOfType<GlowControl>().SetQuestObjectGlow(2, false);

        }
        else if(FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2)
        {
            FindObjectOfType<QuestManager>().isChaos = true;
        }
    }
}
