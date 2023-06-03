using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUIManager : MonoBehaviour
{
    [SerializeField] private Animator PortraitAni;
    [SerializeField] private Animator TimeUIAni;
    [SerializeField] private Sprite emptyLife;
    [SerializeField] private Sprite fullLife;
    [SerializeField] private GameObject lifeUI;

    [SerializeField] private GameObject[] portrait;

    [SerializeField] private BattleLoader battleLoader;

    private void Update()
    {
        if(GameManager.instance.Players.Length > 0)
        {
            PortraitAni.SetInteger("PlayerCnt", GameManager.instance.Players.Length);
        }
        
        if (GameManager.instance.isQuestFinish)
        {
            if (GameManager.instance.nextTurn - 1 < 0)
            {
                PortraitAni.SetInteger("MainPlayer", PlayerPrefs.GetInt("PlayerCnt") - 1);
            }
            else
            {
                PortraitAni.SetInteger("MainPlayer", GameManager.instance.nextTurn - 1);
            }
        }
        if (battleLoader.isBattle)
        {
            for(int i = 0; i < portrait.Length; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    portrait[i].transform.GetChild(j).localScale = new Vector2(30, 30);
                }
            }
        }

        SetLifeUI();
    }

    private void SetLifeUI()
    {
        for (int i =0; i < GameManager.instance.maxLife; i++)
        {
            if (i < GameManager.instance.currentLife)
            {
                lifeUI.transform.GetChild(i).GetComponent<Image>().sprite = fullLife;
            }
            else
            {
                lifeUI.transform.GetChild(i).GetComponent<Image>().sprite = emptyLife;
            }
        }
    }
}
