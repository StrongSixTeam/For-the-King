using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitUI : MonoBehaviour
{
    public int order;

    private Vector3 move = new Vector3(0, 1f, 0);
    public Transform Player;
    [SerializeField] Camera cam;
    [SerializeField] Image portrait;
    private bool set = false;

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(Player.position + move);
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Length > order))
        {
            if (!set)
            {
            portrait.sprite = GameManager.instance.Players[order].GetComponent<PlayerStat>().portrait;
            }
        }
    }
}
