using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [Header("Enemy stat")]
    [SerializeField] EnemyStatus enemyStatus;
    public string monsterName;
    public float maxHp;
    public float nowHp;
    public float atk;
    public float def;

    public int speed;
    public int Lv;
    public int Exp;
    public int maxSlot; //���� ���� ĭ
    public Sprite portrait;
    public bool isDie = false;
    public string attackType;
    public float percent;

    private void Start()
    {
        SetEnemyStat();
    }

    public void SetEnemyStat()
    {
        monsterName = enemyStatus.monsterName;
        maxHp = enemyStatus.maxHp;
        nowHp = enemyStatus.nowHp;
        atk = enemyStatus.atk;
        def = enemyStatus.def;
        speed = enemyStatus.speed;
        Lv = enemyStatus.Lv;
        portrait = enemyStatus.UIImage;
        Exp = enemyStatus.Exp;
        maxSlot = enemyStatus.maxSlot;
        attackType = enemyStatus.AttackType;
        percent = enemyStatus.percent;
    }

}
