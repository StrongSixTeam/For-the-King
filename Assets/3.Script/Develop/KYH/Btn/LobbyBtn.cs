using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtn : MonoBehaviour
{
    [Header("on/off UI 설정")]
    [SerializeField] private GameObject LobbyMenuUI;
    [SerializeField] private GameObject CharacterSelectUI;
    
    private GameObject MainCam;

    private Vector3 MoveCamPos = new Vector3(0, 85, 0); //게임 시작 눌렀을 때 이동할 카메라 위치

    private void Awake()
    {
        MainCam = GameObject.Find("Main Camera");
    }
    public void GameStart()
    {
        StartCoroutine(CameraMove_co());
    }
    private IEnumerator CameraMove_co()
    {
        LobbyMenuUI.SetActive(false);
        
        while(Vector3.Distance(MainCam.transform.position, MoveCamPos) > 0.01f)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, MoveCamPos, 0.03f);
            yield return null;
        }
        MainCam.transform.position = MoveCamPos;
        CharacterSelectUI.SetActive(true);

        yield break;
    }

}
