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
    private Vector3 DefaultPos = new Vector3(0, 7f, -8f);

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
        if (!isCameraPosChange && !isMove && GameManager.instance.MainPlayer.transform.position != null)
        {
            transform.position = DefaultPos + GameManager.instance.MainPlayer.transform.position;
        }
    }
    private void CameraMove() //���콺 ��ġ�� ȭ�� �𼭸� �αٿ� ���� �� ī�޶� �̵���Ű��
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToViewportPoint(mousePos);

        if (!isCameraPosChange)
        {
            if (worldPos.x < 0.01f)
            {
                isMove = true;
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (worldPos.x > 0.99f)
            {
                isMove = true;
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y < 0.01f)
            {
                isMove = true;
                transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y > 0.99f)
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
        transform.SetParent(GameManager.instance.MainPlayer.transform);
        StartCoroutine(CameraSoftMove());
    }
    public IEnumerator CameraSoftMove() //�ε巯�� ī�޶� ����
    {
        isCameraPosChange = true;

        while (Vector3.Distance(transform.localPosition, DefaultPos) > 0.01)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, DefaultPos, 0.02f);
            yield return null;
        }

        transform.localPosition = DefaultPos;
        if (!questManager.isQuest)
        {
            transform.SetParent(null);
            isCameraPosChange = false;
            isMove = false;
        }
        yield break;
    }

}
