using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCnt : MonoBehaviour
{
    public int readyCnt = 0;

    public GameObject battleLoader;
    public GameObject CaveCam;

    [SerializeField] GameObject fieldUI;
    
    private void Update()
    {
        if (readyCnt == GameManager.instance.Players.Count)
        {
            readyCnt = 0;

            CaveCam.SetActive(false);
            CaveCam.SetActive(true);
            battleLoader.SetActive(true);

            battleLoader.GetComponent<BattleLoader>().CaveBattle();
            fieldUI.SetActive(false);
        }
    }
}
