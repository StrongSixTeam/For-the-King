using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    private bool Day = true;
    [SerializeField] private MapObjectCreator mapObjectCreator;
    ChaosControl chaosController;

    [SerializeField] PostProcessingMainCamera postProcessingMainCamera;

    private void Start()
    {
        chaosController = FindObjectOfType<ChaosControl>();
        postProcessingMainCamera = FindObjectOfType<PostProcessingMainCamera>();
        StartCoroutine(SetMapObjectCreatorCo());
    }

    IEnumerator SetMapObjectCreatorCo()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        mapObjectCreator = FindObjectOfType<MapObjectCreator>();
        mapObjectCreator.timeMonsterSpawn(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TimeBarScrolling>() != null)
        {
            Debug.Log("1시간 지났어요");
            chaosController.CountChaosTurn();


            if (!collision.GetComponent<TimeBarScrolling>().isDay && Day)
            {
                Debug.Log("지금은 밤이에요");
                Day = false;
                mapObjectCreator.timeMonsterSpawn(Day);
                postProcessingMainCamera.SetColorFilter(false);
            }
            if (collision.GetComponent<TimeBarScrolling>().isDay && !Day)
            {
                Debug.Log("지금은 낮이에요");
                Day = true;
                mapObjectCreator.timeMonsterSpawn(Day);
                postProcessingMainCamera.SetColorFilter(true);
            }
        }

        if (collision.CompareTag("Chaos"))
        {
            Debug.Log("Max Chaos");
            chaosController.RemoveChaos(true);
        }

    }
}
