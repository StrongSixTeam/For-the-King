using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveSet : MonoBehaviour
{
    public CharacterStatusSet character;

    public void SetMove() //실행시 캐릭터의 속도 스탯에 맞춰 최대 슬롯을 정하고 확률을 돌려 이동 => slotcontroller 빼놔야됨
    {
        SlotController.instance.type = SlotController.Type.move;
        if (character.speed <= 50) //속도 스탯에 따라 최대 슬롯 개수 정하기
        {
            SlotController.instance.maxSlotCount = 4;
        }
        else if (character.speed <= 90)
        {
            SlotController.instance.maxSlotCount = 5;
        }
        else
        {
            SlotController.instance.maxSlotCount = 6;
        }

        SlotController.instance.percent = character.speed;
        SlotController.instance.fixCount = 2; //고정으로 두칸은 성공
    }
}
