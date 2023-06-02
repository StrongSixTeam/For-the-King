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

    private void Start()
    {
        PortraitAni.SetInteger("PlayerCnt", PlayerPrefs.GetInt("PlayerCnt"));
    }
    private void Update()
    {
        if (GameManager.instance.isQuestFinish && PlayerPrefs.GetInt("PlayerCnt") > 1)
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
        else if(PlayerPrefs.GetInt("PlayerCnt") <= 1)
        {

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
