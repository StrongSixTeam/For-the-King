using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Corpse : MonoBehaviour
{
    [SerializeField] private CaveBattleBox CaveEnemy02;

    private void Awake()
    {
        CaveEnemy02 = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>();
    }
    private void Update()
    {
        if (CaveEnemy02.enemys01.Count == 0 && CaveEnemy02.enemys02.Count == 0 && CaveEnemy02.enemys03.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (FindObjectOfType<QuestManager>() != null && FindObjectOfType<QuestManager>().questTurn > 2)
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
