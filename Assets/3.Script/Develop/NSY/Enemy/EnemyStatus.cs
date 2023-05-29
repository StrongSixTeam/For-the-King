using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "ScriptableObjects/EnemyBaseStatsData", order = 2)]

public class EnemyStatus : ScriptableObject
{
    public float maxHp = 100f;
  
    public int Atk = 2;
}
