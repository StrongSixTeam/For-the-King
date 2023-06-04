using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBattleBox : MonoBehaviour
{
    //인덱스 하나당 방 하나
    List<List<GameObject>> stage = new List<List<GameObject>>();

    //스테이지별로 몬스터 세팅
    public List<GameObject> enemys01 = new List<GameObject>();
    public List<GameObject> enemys02 = new List<GameObject>();
    public List<GameObject> enemys03 = new List<GameObject>();

    
    private void Start()
    {
        stage.Add(enemys01);
        stage.Add(enemys02);
        if (enemys03.Count > 0)
        {
            stage.Add(enemys03);
        }

    }


    //방 번호를 입력하면, 몬스터 반환
    public List<GameObject> GetEnemyObject(int stageIndex)
    {
        return stage[stageIndex];
    }
}
