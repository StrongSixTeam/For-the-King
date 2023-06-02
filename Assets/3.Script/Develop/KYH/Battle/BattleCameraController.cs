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

    private void Start()
    {
        if (gameObject.CompareTag("BattleCamera"))
        {
            targetPosP = new Vector3(-109, 3.5f, -17.8f);
            targetPosE = new Vector3(-96, 4.3f, -21.9f);
        }
        if (gameObject.CompareTag("CaveCamera"))
        {
            targetPosP = new Vector3(-206.5f, 8.6f, -49.1f);
            targetPosE = new Vector3(-198.4f, 5.9f, -47.5f);
        }
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
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.01f);
            yield return null;
        }
        transform.position = targetPos;
        battleManager.RookAt(); //오류나면 여기
        //확률 ui 켜기
        yield break;
    }
}
