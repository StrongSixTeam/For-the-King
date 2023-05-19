using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnderBarBtn : MonoBehaviour
{
    [Header("on/off UI ����")]
    [SerializeField] private GameObject LobbyMenuUI;
    [SerializeField] private GameObject CharacterSelectUI;

    [Header("�÷��̾� �Է� �ʵ�")]
    [SerializeField] private Text[] playerNames;

    private GameObject MainCam;

    private Vector3 MoveCamPos = new Vector3(0, 100, -10); //�ڷ� ������ �� �̵��� ī�޶� ��ġ

    private void Awake()
    {
        MainCam = GameObject.Find("Main Camera");
    }

    private void PlayerNameSave()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            if (playerNames[i].text == "")
            {
                PlayerPrefs.SetString(string.Format("Player{0}", i + 1), string.Format("�÷��̾� {0}", i + 1));
            }
            else
            {
                PlayerPrefs.SetString(string.Format("Player{0}", i + 1), playerNames[i].text);
            }
        }
    }
    public void GameStart()
    {
        PlayerNameSave();

        SceneManager.LoadScene("SampleScene");
    }
    public void Back()
    {
        StartCoroutine(CameraMove_co());   
    }
    private IEnumerator CameraMove_co()
    {
        CharacterSelectUI.SetActive(false);

        while (Vector3.Distance(MainCam.transform.position, MoveCamPos) > 0.01f)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, MoveCamPos, 0.03f);
            yield return null;
        }
        MainCam.transform.position = MoveCamPos;

        LobbyMenuUI.SetActive(true);

        yield break;
    }
}
