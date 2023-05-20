using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOnOffBtn : MonoBehaviour
{
    [Header("�÷��̾� ���� UI")]
    [SerializeField] private GameObject[] playerInfo;

    [Header("���� ��ư")]
    [SerializeField] private GameObject[] CreateBtn;
    
    [Header("���� ��ư")]
    [SerializeField] private GameObject DeleteBtn;

    private int num = 1; //��ȯ ��ġ üũ��

    private UnderBarBtn underBarBtn;

    private void Awake()
    {
        underBarBtn = FindObjectOfType<UnderBarBtn>();
    }
    public void PlayerSelect()
    {
        playerInfo[num].SetActive(true);
        CreateBtn[num - 1].SetActive(false);
        if (num == 1)
        {
            CreateBtn[num].SetActive(true);
        }
        underBarBtn.playerNames.Add(playerInfo[num].GetComponentInChildren<Text>());
        underBarBtn.playerClass.Add(playerInfo[num].GetComponentsInChildren<Text>()[2]);

        num++;

        if(num == 3)
        {
            DeleteBtn.SetActive(false);
        }

    }
    public void PlayerDelete()
    {
        num--;

        playerInfo[num].SetActive(false);
        CreateBtn[num - 1].SetActive(true);
        if (num == 1)
        {
            CreateBtn[num].SetActive(false);
        }
        underBarBtn.playerNames.RemoveAt(num);
        underBarBtn.playerClass.RemoveAt(num);

        if(num != 3)
        {
            DeleteBtn.SetActive(true);
        }
    }
}
