using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Stat")]
    public string name;
    public int order; //�÷��̾� ����
    public string className = ""; //Ŭ���� �̸�
    public float atk; //���ݷ�
    public float def; //����
    public float maxHp;
    public float nowHp;
    public int intelligence;
    public int strength;
    public int awareness;
    public int speed;

    public int nowFocus; //���߷�
    public int maxFocus; //���߷�

    public float nowExp = 1;
    public float maxExp = 100;
    public int Lv = 1;
    public Sprite portrait;

    public void SetStat(CharacterStatusSet data)
    {
        className = data.className;
        atk = data.atk;
        def = data.def;
        maxHp = data.maxHp;
        nowHp = data.nowHp;
        intelligence = data.intelligence;
        strength = data.strength;
        awareness = data.awareness;
        speed = data.speed;
        portrait = data.UIImage;
        maxFocus = data.focus;
        nowFocus = maxFocus;
    }
}
