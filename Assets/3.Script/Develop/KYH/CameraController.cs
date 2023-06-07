using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //���콺 ������ ���� ����
    private Vector3 mousePos;
    private Vector3 worldPos;

    //�÷��̾� ���� �� ī�޶� �ʱ� ��
    public Vector3 targetPos;
    public Vector3 defaultPos;

    private bool isCameraPosChange = false;
    private bool isMove = false;
    private bool isPlayer = true;

    private float zoomMax;
    private float zoomMin;
    private float moveSpeed;
    private float zoomSpeed;

    private QuestManager questManager;
    private PlayerController_Jin player;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }
    private void Start()
    {
        defaultPos = new Vector3(0, 10f, -9f);
        moveSpeed = 10f;
        zoomSpeed = 5f;
        zoomMax = 15f;
        zoomMin = 7f;
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
    private void CameraMove() //���콺 ��ġ�� ȭ�� �𼭸� �αٿ� ���� �� ī�޶� �̵���Ű��
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
        isPlayer = true;
        StartCoroutine(CameraSoftMove());
        
    }
    public IEnumerator CameraSoftMove() //�ε巯�� ī�޶� ����
    {
        isCameraPosChange = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.02f);
            yield return null;

            if (GameManager.instance.MainPlayer.transform.position + defaultPos != targetPos && isPlayer)
            {
                break;
            }
        }
        if (GameManager.instance.MainPlayer.transform.position + defaultPos == targetPos)
        {
            transform.position = targetPos;
        }
        if (!questManager.isQuest)
        {
            isCameraPosChange = false;
            isMove = false;
        }

        isPlayer = false;
        yield break;
    }

}
