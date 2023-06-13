using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Corpse : MonoBehaviour
{
    [SerializeField] private CaveBattleBox CaveEnemy02;

    private bool isEnd = false;

    private void Awake()
    {
        CaveEnemy02 = GameObject.Find("CaveEnemy02").GetComponent<CaveBattleBox>();
    }
    private void Update()
    {
        if (CaveEnemy02.enemys01.Count == 0 && CaveEnemy02.enemys02.Count == 0 && CaveEnemy02.enemys03.Count == 0)
        {
            isEnd = true;
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        EncounterManager.instance.encounter[8].isCleared = true;
        if (isEnd)
        {
            FindObjectOfType<GlowControl>().SetQuestObjectGlow(5, false);
            FindObjectOfType<ChaosControl>().RemoveChaos(false);
            GameManager.instance.isClear = true;
        }
        AudioManager.instance.BGMPlayer.Stop();
        AudioManager.instance.BGMPlayer.clip = AudioManager.instance.BGM[1].clip;
        AudioManager.instance.BGMPlayer.Play();
    }
}
