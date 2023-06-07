using System.Collections;
using UnityEngine;

public class GlowControl : MonoBehaviour
{

    //컨트롤할 글로우를 담자
    [SerializeField] GameObject[] playerGlowBox = new GameObject[3];

    [SerializeField] GameObject[] objectGlowBox = new GameObject[5];
    [SerializeField] GameObject GodGlow;
    int[] saveobjectGlowIndex = new int[6];

    [SerializeField] MapObjectCreator mapObjectCreator;
    [SerializeField] HexMapCreator hexMapCreator;

    private void Start()
    {
        StartCoroutine(FindMapObjectCreatorCo());
        hexMapCreator = FindObjectOfType<HexMapCreator>();
        GodGlow.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            playerGlowBox[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            objectGlowBox[i].SetActive(false);
        }
    }

    IEnumerator FindMapObjectCreatorCo()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        mapObjectCreator = FindObjectOfType<MapObjectCreator>();
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

    public void SetQuestObjectGlow(int objNum, bool on)
    {
        //objNum : 우드스모크(0), 신도의식도구(1), 카오스우두머리(2), 눈부신광산(3), 패리드(4), 시체의지하실(5)
        //on : 글로우켜기(true), 끄기(false)

        switch (objNum)
        {
            case 0:
                if (on)
                {
                    for (int i = 0; i < objectGlowBox.Length; i++)
                    {
                        if (!objectGlowBox[i].activeSelf)
                        {
                            objectGlowBox[i].SetActive(true);
                            objectGlowBox[i].transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[1]].transform.position + new Vector3(0f, 1f, 0f);
                            saveobjectGlowIndex[0] = i;
                            return;
                        }
                    }
                }
                else
                {
                    objectGlowBox[saveobjectGlowIndex[0]].SetActive(false);
                    return;
                }
                break;
            case 1:
                if (on)
                {
                    if (!GodGlow.activeSelf)
                    {
                        GodGlow.SetActive(true);
                        GodGlow.transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[2]].transform.position + new Vector3(0f, 1f, 0f);
                        return;
                    }

                }
                else
                {
                    GodGlow.SetActive(false);
                    return;
                }
                break;
            case 2:
                if (on)
                {
                    for (int i = 0; i < objectGlowBox.Length; i++)
                    {
                        if (!objectGlowBox[i].activeSelf)
                        {
                            objectGlowBox[i].SetActive(true);
                            objectGlowBox[i].transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[3]].transform.position + new Vector3(0f, 1f, 0f);
                            saveobjectGlowIndex[2] = i;
                            return;
                        }
                    }
                }
                else
                {
                    objectGlowBox[saveobjectGlowIndex[2]].SetActive(false);
                    return;
                }
                break;
            case 3:
                if (on)
                {
                    for (int i = 0; i < objectGlowBox.Length; i++)
                    {
                        if (!objectGlowBox[i].activeSelf)
                        {
                            objectGlowBox[i].SetActive(true);
                            objectGlowBox[i].transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[4]].transform.position + new Vector3(0f, 1f, 0f);
                            saveobjectGlowIndex[3] = i;
                            return;
                        }
                    }
                }
                else
                {
                    objectGlowBox[saveobjectGlowIndex[3]].SetActive(false);
                    return;
                }
                break;
            case 4:
                if (on)
                {
                    for (int i = 0; i < objectGlowBox.Length; i++)
                    {
                        if (!objectGlowBox[i].activeSelf)
                        {
                            objectGlowBox[i].SetActive(true);
                            objectGlowBox[i].transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[5]].transform.position + new Vector3(0f, 1f, 0f);
                            saveobjectGlowIndex[4] = i;
                            return;
                        }
                    }
                }
                else
                {
                    objectGlowBox[saveobjectGlowIndex[4]].SetActive(false);
                    return;
                }
                break;
            case 5:
                if (on)
                {
                    for (int i = 0; i < objectGlowBox.Length; i++)
                    {
                        if (!objectGlowBox[i].activeSelf)
                        {
                            objectGlowBox[i].SetActive(true);
                            objectGlowBox[i].transform.position = hexMapCreator.hexMembers[mapObjectCreator.objectIndex[8]].transform.position + new Vector3(0f, 1f, 0f);
                            saveobjectGlowIndex[5] = i;
                            return;
                        }
                    }
                }
                else
                {
                    objectGlowBox[saveobjectGlowIndex[5]].SetActive(false);
                    return;
                }
                break;
        }


    }


}
