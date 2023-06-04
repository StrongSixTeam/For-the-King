using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBattleBox : MonoBehaviour
{
    //�ε��� �ϳ��� �� �ϳ�
    List<List<GameObject>> stage = new List<List<GameObject>>();

    //������������ ���� ����
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


    //�� ��ȣ�� �Է��ϸ�, ���� ��ȯ
    public List<GameObject> GetEnemyObject(int stageIndex)
    {
        return stage[stageIndex];
    }
}
