using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    private Vector3 targetPosP;
    private Vector3 targetPosE;

    [SerializeField] private Vector3 targetPos;
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
        StartCoroutine(CameraSoftMove_co());
    }
    public void EnemyTurnCamera()
    {
        targetPos = targetPosE;
        StartCoroutine(CameraSoftMove_co());
    }
    private IEnumerator CameraSoftMove_co()
    {
        yield return new WaitForSeconds(3f);
        
        while (Vector3.Distance(transform.position, targetPos) > 0.01)
        {
            transform.LookAt(battleOrderManager.Order[battleOrderManager.turn].transform);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.02f);
            yield return null;
        }
        transform.position = targetPos;
        battleManager.RookAt();
        UIAni.SetBool("TurnOn", false);
        //È®·ü ui ÄÑ±â
        yield break;
    }
}
