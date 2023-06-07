using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpStatus : MonoBehaviour
{
    /*
     Exp�� 100�� ������ ��
     ������ 1 ����.

     ������ ���� ��
     ������ �ϰ�������
     ������ ��ġ��ŭ ����.
     (�� �������� Ưȭ ������ ���� �� ���� ��ġ�� ����)

     ������ ���ѱ���.
     �� ���� �Ϲ� ������ 2 ������ ��ġ�� ������ 
     Ưȭ ������ 5 ������ ��ġ�� ����
     
     ���� �¸� �� ������ ���� ���� ��
     ����ġ ����

     */

    public List<PlayerStat> characters;
    //3�� �� �־���ϴϱ� �迭�� ����

    public void LevelUp()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].nowExp >= characters[i].maxExp) //���� Exp�� max Exp���� ũ�ų� ������ �� ������.
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
