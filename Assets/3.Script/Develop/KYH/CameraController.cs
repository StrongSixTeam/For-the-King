using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //마우스 포지션 관련 변수
    private Vector3 mousePos;
    private Vector3 worldPos;

    //플레이어 변경 시 카메라 초기 값
    public Vector3 targetPos;
    public Vector3 defaultPos = new Vector3(0, 7f, -8f);

    private bool isCameraPosChange = false;
    private bool isMove = false;

    private float zoomMax;
    private float zoomMin;
    private float moveSpeed;
    private float zoomSpeed;

    private QuestManager questManager;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }
    private void Start()
    {
        moveSpeed = 10f;
        zoomSpeed = 10f;
        zoomMax = 8f;
        zoomMin = 4f;
    }
    private void Update()
    {
        if (!questManager.isQuest)
        {
            CameraMove();
            CameraZoom();
        }
        if (!isCameraPosChange && !isMove && GameManager.instance.MainPlayer != null)
        {
            transform.position = defaultPos + GameManager.instance.MainPlayer.transform.position;
        }
    }
    private void CameraMove() //마우스 위치가 화면 모서리 부근에 있을 때 카메라 이동시키기
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToViewportPoint(mousePos);

        if (!isCameraPosChange)
        {
            if (worldPos.x < 0.01f || Input.GetKey(KeyCode.A))
            {
                isMove = true;
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (worldPos.x > 0.99f || Input.GetKey(KeyCode.D))
            {
                isMove = true;
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y < 0.01f || Input.GetKey(KeyCode.S))
            {
                isMove = true;
                transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y > 0.99f || Input.GetKey(KeyCode.W))
            {
                isMove = true;
                transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
    private void CameraZoom()
    {
        float zoomDir = Input.GetAxis("Mouse ScrollWheel");

        if ((transform.localPosition.y >= zoomMax && zoomDir < 0) || (transform.localPosition.y <= zoomMin && zoomDir > 0))
        {
            return;
        }

        if (!isCameraPosChange)
        {
            transform.position += transform.forward * zoomDir * zoomSpeed;
        }
    }
    public void PlayerChange()
    {
        targetPos = GameManager.instance.MainPlayer.transform.position + defaultPos;
        StartCoroutine(CameraSoftMove());
    }
    public IEnumerator CameraSoftMove() //부드러운 카메라 무빙
    {
        isCameraPosChange = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.02f);
            yield return null;
        }
        transform.position = targetPos;
        if (!questManager.isQuest)
        {
            isCameraPosChange = false;
            isMove = false;
        }
        yield break;
    }

}
