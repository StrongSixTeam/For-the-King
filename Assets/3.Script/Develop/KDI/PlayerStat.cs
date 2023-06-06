using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Stat")]
    public string name;
    public int order; //플레이어 순서
    public string className = ""; //클래스 이름
    public float originalAtk; //직업별 공격력
    public float atk; //공격력
    public float originalDef; //직업별 방어력
    public float def; //방어력
    public float originalMaxHp;
    public float maxHp;
    public float nowHp;
    public int intelligence;
    public int strength;
    public int awareness;
    public int speed;

    public int nowFocus; //현재 집중력
    public int maxFocus; //최대 집중력

    public int coins; //현재 가지고 있는 코인

    public float nowExp = 0;
    public float maxExp = 30;
    public int Lv = 1;
    public Sprite portrait;

    public Weapon weapon;
    public Armor armorHelmet;
    public Armor armor;
    public Armor armorBoots;
    public Armor armorNecklace;

    public enum Sanctum
    {
        none, focus, life, wisdom
    }
    public Sanctum whichSanctum;

    public void SetStat(CharacterStatusSet data)
    {
        className = data.className;
        originalAtk = data.atk;
        atk = data.atk;
        originalDef = data.def;
        def = data.def;
        originalMaxHp = maxExp;
        maxHp = data.maxHp;
        nowHp = data.nowHp;
        intelligence = data.intelligence;
        strength = data.strength;
        awareness = data.awareness;
        speed = data.speed;
        portrait = data.UIImage;
        maxFocus = data.focus;
        nowFocus = maxFocus;
        coins = data.coins;
        weapon = data.weapon;
        whichSanctum = Sanctum.none;
    }
}
