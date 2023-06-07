using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyCave : MonoBehaviour
{
    [SerializeField] private CaveBattleBox CaveEnemy01;

    private void Awake()
    {
        CaveEnemy01 = GameObject.Find("CaveEnemy01").GetComponent<CaveBattleBox>();
    }
    private void Update()
    {
        if(CaveEnemy01.enemys01.Count == 0 && CaveEnemy01.enemys02.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        EncounterManager.instance.encounter[4].isCleared = true;
        if (FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2 && !FindObjectOfType<QuestManager>().isShinyCave)
        {
            FindObjectOfType<QuestManager>().PopUp("ShinyCave");

            FindObjectOfType<QuestManager>().questClearCnt++;

            FindObjectOfType<QuestManager>().isShinyCave = true;

            FindObjectOfType<GlowControl>().SetQuestObjectGlow(3, false);

        }
    }
}
