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

    private Vector2[] defaultPos = new Vector2[3];

    private bool isCheck = false;

    private void Start()
    {
        defaultPos[0] = portrait[0].transform.GetChild(0).GetComponent<RectTransform>().position;
        defaultPos[1] = portrait[1].transform.GetChild(1).GetComponent<RectTransform>().position;
        defaultPos[2] = portrait[2].transform.GetChild(2).GetComponent<RectTransform>().position;
    }
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
        if (battleLoader.isBattle && !isCheck)
        {
            for(int i = 0; i < portrait.Length; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    portrait[i].transform.GetChild(j).GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                    portrait[i].transform.GetChild(j).GetComponent<RectTransform>().position = defaultPos[i];
                }
            }

            isCheck = true;
        }
        if (!battleLoader.isBattle)
        {
            isCheck = false;
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
