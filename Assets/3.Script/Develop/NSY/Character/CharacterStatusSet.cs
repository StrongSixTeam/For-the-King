using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//스크립터블 파일 생성 시 만들어지는 기본 이름과 메뉴 네이밍
[CreateAssetMenu(fileName ="CharacterBaseStatsData", menuName = "ScriptableObjects/CharacterBaseStatsData", order =1)  ]
public class CharacterStatusSet : ScriptableObject
{
    public string className= "Normal";
    public float atk = 10f;
    public float def = 10f;
    public float maxHp = 60f;
    public float nowHp = 60f;
    //지력 (학자 특화 스탯)
    public int intelligence=50;
    //힘 (대장장이 특화 스탯)
    public int strength=50;
    //인지 (사냥꾼 특화 스탯)
    public int awareness=50;
    //속도 (사냥꾼 특화 스탯)
    public int speed=50;
    //경험치
    public float nowExp=1;
    public float maxExp=100;
    public int Lv=1;

    public Sprite UIImage; //초상화 이미지

    public int focus = 6; //집중력
    public int coins = 5; //초기 코인
    
    
}
