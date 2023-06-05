using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBoss : MonoBehaviour
{
    private void OnDisable()
    {
        if (FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2)
        {
            Debug.Log("카오스 보스 깼다~");

            FindObjectOfType<QuestManager>().PopUp("ChaosBoss");

            FindObjectOfType<QuestManager>().questClearCnt++;

            if (FindObjectOfType<QuestManager>().questClearCnt == 2)
            {
                FindObjectOfType<QuestManager>().questTurn = 6;
            }
        }
    }
}
