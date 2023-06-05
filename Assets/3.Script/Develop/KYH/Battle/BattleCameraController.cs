using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    private Vector3 targetPosP;
    private Vector3 targetPosE;

    [SerializeField] private Transform lookPosP;
    [SerializeField] private Transform lookPosE;

    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 lookPos;

    [SerializeField] private BattleManager battleManager;

    private Vector3 defaultPos;
    private Vector3 defaultAngle;

    private void OnEnable()
    {
        if (gameObject.CompareTag("BattleCamera"))
        {
            targetPosP = new Vector3(-109, 3.5f, -17.8f);
            targetPosE = new Vector3(-96, 4.3f, -21.9f);
            defaultPos = new Vector3(-101.5f, 1.5f, -21.7f);
            defaultAngle = new Vector3(1, 6, 0);
            
        }
        if (gameObject.CompareTag("CaveCamera"))
        {
            targetPosP = new Vector3(-206.5f, 8.6f, -49.1f);
            targetPosE = new Vector3(-198.4f, 5.9f, -47.5f);
            defaultPos = new Vector3(-200, 1.2f, -47.5f);
            defaultAngle = new Vector3(-3, 0, 0);
        }

        transform.position = defaultPos;
        transform.eulerAngles = defaultAngle;
    }
    public void PlayerTurnCamera()
    {
        targetPos = targetPosP;
        lookPos = lookPosP.position;
        StartCoroutine(CameraSoftMove_co());
    }
    public void EnemyTurnCamera()
    {
        targetPos = targetPosE;
        lookPos = lookPosE.position;
        StartCoroutine(CameraSoftMove_co());
    }
    private IEnumerator CameraSoftMove_co()
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01)
        {
            transform.LookAt(lookPos);
            transform.position = Vector3.Slerp(transform.position, targetPos, 0.015f);
            yield return null;
        }
        transform.position = targetPos;
        battleManager.RookAt(); //오류나면 여기
        yield break;
    }
    public IEnumerator PlayerWinCam_co()
    {
        battleManager.isEnd = true;

        while (Vector3.Distance(transform.position, targetPosE) > 0.01)
        {
            transform.LookAt(lookPosE);
            transform.position = Vector3.MoveTowards(transform.position, targetPosE, 0.01f);
            yield return null;
        }
        transform.position = targetPosE;
        yield break;
    }
}
