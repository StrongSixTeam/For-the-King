using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitUI : MonoBehaviour
{
    public int order;

    public Vector3 move = new Vector3(0, 1f, 0);
    public Transform Player;
    [SerializeField] Camera cam;
    [SerializeField] Image portrait;
    private bool set = false;

    void Update()
    {
        if (Player != null)
        {
            transform.position = cam.WorldToScreenPoint(Player.position + move);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Count > order))
        {
            if (!set)
            {
                portrait.sprite = GameManager.instance.Players[order].GetComponent<PlayerStat>().portrait;
                set = true;
            }
        }
    }
}
