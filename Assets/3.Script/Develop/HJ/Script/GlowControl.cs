using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowControl : MonoBehaviour
{

    //컨트롤할 글로우를 담자
    [SerializeField] GameObject[] playerGlowBox = new GameObject[3];


    private void Start()
    {
        for(int i=0; i<3; i++)
        {
            playerGlowBox[i].SetActive(false);
        }
    }

    public void SetTurnGlow(int player)
    {

        switch (player)
        {
            case -1: //모두 끄기
                for (int i = 0; i < 3; i++)
                {
                    if (playerGlowBox[i].activeSelf)
                    {
                        playerGlowBox[i].SetActive(false);
                    }
                }
                break;

            case 0:
                for (int i = 1; i < 3; i++)
                {
                    if (playerGlowBox[i].activeSelf)
                    {
                        playerGlowBox[i].SetActive(false);
                    }
                }
                playerGlowBox[0].SetActive(true);
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    if (playerGlowBox[i].activeSelf)
                    {
                        playerGlowBox[i].SetActive(false);
                    }
                }
                playerGlowBox[1].SetActive(true);
                break;

            case 2:
                for (int i = 0; i < 2; i++)
                {
                    if (playerGlowBox[i].activeSelf)
                    {
                        playerGlowBox[i].SetActive(false);
                    }
                }
                playerGlowBox[2].SetActive(true);
                break;
        }
    }


    public void BattleTurnGlow()
    {

    }


}
