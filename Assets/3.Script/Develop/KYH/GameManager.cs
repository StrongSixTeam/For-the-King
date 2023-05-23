using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraController.PlayerChange();
        }
    }
}
