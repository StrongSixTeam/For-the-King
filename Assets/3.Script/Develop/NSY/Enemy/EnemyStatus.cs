using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "ScriptableObjects/EnemyBaseStatsData", order = 2)]

public class EnemyStatus : ScriptableObject
{
   
    public string monsterName = "Normal";
    public float atk = 10f;
    public float def = 10f;
    public float maxHp = 60f;
    public float nowHp = 60f;
   
    //�ӵ� 
    public int speed = 50;
    
    public int Lv = 1;
    public int Exp = 10;
    public Sprite UIImage; //�ʻ�ȭ �̹���
   
}
