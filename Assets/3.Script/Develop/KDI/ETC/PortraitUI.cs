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

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(Player.position + move);
    }
}
