using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Corpse : MonoBehaviour
{
    [SerializeField] private CaveBattleBox CaveEnemy02;
    [SerializeField] ChaosControl chaosControl;

    private bool isEnd = false;

    private void Awake()
    {
        CaveEnemy02 = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>();
    }

    private void Start()
    {
        chaosControl = FindObjectOfType<ChaosControl>();
    }
    private void Update()
    {
        if (CaveEnemy02.enemys01.Count == 0 && CaveEnemy02.enemys02.Count == 0 && CaveEnemy02.enemys03.Count == 0)
        {
            Destroy(gameObject);
            isEnd = true;
        }
    }

    private void OnDisable()
    {
        EncounterManager.instance.encounter[7].isCleared = true;
        if (FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2 && isEnd)
        {
            FindObjectOfType<GlowControl>().SetQuestObjectGlow(5, false);
            chaosControl.RemoveChaos(false);
            GameManager.instance.isClear = true;
        }
    }
}
