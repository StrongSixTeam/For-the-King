using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStatus : MonoBehaviour
{
    /*
     Exp가 100에 도달할 시
     레벨이 1 증가.

     레벨이 오를 시
     스탯이 일괄적으로
     정해진 수치만큼 오름.
     (각 직업마다 특화 스탯은 조금 더 높은 수치로 오름)

     레벨은 무한까지.
     각 직업 일반 스탯은 2 사이의 수치로 오르며 
     특화 스탯은 5 사이의 수치로 오름
     
     전투 승리 및 지혜의 성소 조우 시
     경험치 증가

     */

    public List<PlayerStat> characters;
    //3개 다 있어야하니까 배열로 받자

    public void LevelUp()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].nowExp >= characters[i].maxExp) //지금 Exp가 max Exp보다 크거나 같아질 때 레벨업.
            {
                characters[i].Lv++;
                characters[i].nowExp = characters[i].nowExp - characters[i].maxExp;


                if (characters[i].className == "Blacksmith")
                {
                    characters[i].maxHp = characters[i].maxHp + 5f;

                    if (characters[i].nowHp <= characters[i].maxHp)
                    {
                        characters[i].nowHp = characters[i].nowHp + 20f;

                        if (characters[i].nowHp >= characters[i].maxHp)
                        {
                            characters[i].nowHp = characters[i].maxHp;
                        }


                    }

                    characters[i].intelligence = characters[i].intelligence + 2;
                    characters[i].atk = characters[i].atk * characters[i].Lv;
                    characters[i].def = characters[i].def + 2;
                    characters[i].strength = characters[i].strength + 5;
                    characters[i].awareness = characters[i].awareness + 2;
                    characters[i].speed = characters[i].speed + 2;

                }

                else if (characters[i].className == "Hunter")
                {
                    characters[i].maxHp = characters[i].maxHp + 3f;
                    if (characters[i].nowHp <= characters[i].maxHp)
                    {
                        characters[i].nowHp = characters[i].nowHp + 20f;

                        if (characters[i].nowHp >= characters[i].maxHp)
                        {
                            characters[i].nowHp = characters[i].maxHp;
                        }


                    }
                    characters[i].intelligence = characters[i].intelligence + 2;
                    characters[i].atk = characters[i].atk * characters[i].Lv + 2f;
                    characters[i].def = characters[i].def + 2f;
                    characters[i].strength = characters[i].strength + 2;
                    characters[i].awareness = characters[i].awareness + 5;
                    characters[i].speed = characters[i].speed + 5;
                }

                else if (characters[i].className == "Scholar")
                {
                    characters[i].maxHp = characters[i].maxHp + 3f;
                    if (characters[i].nowHp <= characters[i].maxHp)
                    {
                        characters[i].nowHp = characters[i].nowHp + 20f;

                        if (characters[i].nowHp >= characters[i].maxHp)
                        {
                            characters[i].nowHp = characters[i].maxHp;
                        }

                    }
                    characters[i].intelligence = characters[i].intelligence + 5;
                    characters[i].atk = characters[i].atk * characters[i].Lv + 2f;
                    characters[i].def = characters[i].def + 1;
                    characters[i].strength = characters[i].strength + 2;
                    characters[i].awareness = characters[i].awareness + 5;
                    characters[i].speed = characters[i].speed + 2;
                }



            }

        }


    }




}
