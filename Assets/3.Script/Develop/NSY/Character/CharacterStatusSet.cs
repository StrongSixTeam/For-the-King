using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//스크립터블 파일 생성 시 만들어지는 기본 이름과 메뉴 네이밍
[CreateAssetMenu(fileName ="CharacterBaseStatsData", menuName = "ScriptableObjects/CharacterBaseStatsData", order =1)  ]
public class CharacterStatusSet : ScriptableObject
{
    
    public int Hp;
    //지력 (학자 특화 스탯)
    public int intelligence;
    //힘 (대장장이 특화 스탯)
    public int strength;
    //인지 (사냥꾼 특화 스탯)
    public int awareness;
    //속도 (사냥꾼 특화 스탯)
    public int speed;
    //경험치
    public int Exp;
    public int Lv;

    



}
