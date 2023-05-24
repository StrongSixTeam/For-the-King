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

    private QuestManager questManager;

    private CharacterStatusSet[] moveCharacter;

    private void Start()
    {
        //moveCharacter = GameObject.FindGameObjectsWithTag("Player")[];
        cameraController = FindObjectOfType<CameraController>();
        questManager = FindObjectOfType<QuestManager>();

        questManager.PopUp(questManager.questTurn);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cameraController.PlayerChange();
        }
    }

}
