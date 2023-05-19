using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //���콺 ������ ���� ����
    private Vector3 mousePos;
    private Vector3 worldPos;

    //�÷��̾� ���� �� ī�޶� �ʱ� ��
    private Vector3 DefaultPos = new Vector3(0, 6f, -7.3f);

    private bool isPlayerChange = false;

    [Header("�� �ִ�/�ּ� �Ÿ�")]
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;

    [Header("�ӵ�")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;

    //�÷��̾� ���� ���� (���� ����)
    private GameObject[] Players;
    private GameObject MainPlayer;
    private int num = 0;

    private void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Start()
    {
        PlayerChange();
    }
    private void Update()
    {
        CameraMove();
        CameraZoom();
    }
    private void CameraMove() //���콺 ��ġ�� ȭ�� �𼭸� �αٿ� ���� �� ī�޶� �̵���Ű��
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToViewportPoint(mousePos);

        if (!isPlayerChange)
        {
            if (worldPos.x < 0.01f)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (worldPos.x > 0.99f)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y < 0.01f)
            {
                transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if (worldPos.y > 0.99f)
            {
                transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
    private void CameraZoom()
    {
        float zoomDir = Input.GetAxis("Mouse ScrollWheel");

        if ((transform.position.y >= zoomMax && zoomDir < 0) || (transform.position.y <= zoomMin && zoomDir > 0))
        {
            return;
        }

        if (!isPlayerChange)
        {
            transform.position += transform.forward * zoomDir * zoomSpeed;
        }
    }
    public void PlayerChange()
    {
        MainPlayer = Players[num];

        transform.SetParent(MainPlayer.transform);
        StartCoroutine(CameraSoftMove());

        num++;
        if (num > 2)
        {
            num = 0;
        }
    }
    private IEnumerator CameraSoftMove() //�ε巯�� ī�޶� ����
    {
        isPlayerChange = true;

        while (Vector3.Distance(transform.localPosition, DefaultPos) > 0.01)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, DefaultPos, 0.02f);
            yield return null;
            Debug.Log(0);
        }

        transform.localPosition = DefaultPos;
        isPlayerChange = false;
        yield break;
    }

}
