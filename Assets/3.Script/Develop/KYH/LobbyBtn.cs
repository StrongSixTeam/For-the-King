using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtn : MonoBehaviour
{
    public GameObject Background;
    
    private GameObject MainCam;

    private Vector3 MoveCamPos = Vector3.zero; //게임 시작 눌렀을 때 이동할 카메라 위치

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
        Background.SetActive(false);
        
        while(Vector3.Distance(MainCam.transform.position, MoveCamPos) > 0.01f)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, MoveCamPos, 0.05f);
            yield return null;
        }
        MainCam.transform.position = MoveCamPos;
        yield break;
    }

}
