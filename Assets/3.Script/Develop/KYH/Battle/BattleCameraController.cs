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

    [SerializeField] private BattleOrderManager battleOrderManager;
    [SerializeField] private BattleManager battleManager;

    [SerializeField] Animator UIAni;

    private void Start()
    {
        targetPosP = new Vector3(-109, 3.5f, -17.8f);
        targetPosE = new Vector3(-96, 4.3f, -21.9f);
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
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.05f);
            yield return null;
        }
        transform.position = targetPos;
        battleManager.RookAt();
        //È®·ü ui ÄÑ±â
        yield break;
    }
}
