using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingUI : MonoBehaviour
{
    public int order;

    private Vector3 move = new Vector3(0, 3f, 0);
    public Transform Player;
    [SerializeField] Camera cam;
    [SerializeField] private Text txtMoving;
    private AstsrPathfinding astar;

    int zero = 0;

    private void Start()
    {
        astar = FindObjectOfType<AstsrPathfinding>();
    }
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(Player.position + move);
        txtMoving.text = astar.canMoveCount.ToString();
    }
    public void ResetText()
    {
        astar.canMoveCount = 0;
    }
}
