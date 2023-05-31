using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUIManager : MonoBehaviour
{
    [SerializeField] private Animator PortraitAni;
    [SerializeField] private Animator TimeUIAni;

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
    }
}
