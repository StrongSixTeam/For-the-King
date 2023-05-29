using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusInfo : MonoBehaviour
{
    public EnemyStatus enemystatusdata;
    private float Hp;
    private int atk;

    private void Awake()
    {
        Hp = enemystatusdata.maxHp;
        atk = enemystatusdata.Atk;
        Debug.Log($"{Hp}" );
        Debug.Log($"{gameObject.name}" );
        
    }



}
