using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCnt : MonoBehaviour
{
    public int readyCnt = 0;

    public GameObject battleLoader;
    public GameObject CaveCam;
    
    private void Update()
    {
        if (readyCnt == GameManager.instance.Players.Length)
        {
            readyCnt = 0;

            CaveCam.SetActive(false);
            CaveCam.SetActive(true);
            battleLoader.SetActive(true);

            battleLoader.GetComponent<BattleLoader>().CaveBattle();
        }
    }
}
