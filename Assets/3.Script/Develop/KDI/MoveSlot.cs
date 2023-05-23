using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlot : MonoBehaviour
{
    public CharacterStatusSet player;

    public void SetMove()
    {
        SlotController.instance.type = SlotController.Type.move;
        if (player.speed <= 50)
        {
            SlotController.instance.maxSlotCount = 4;
        }
        else if (player.speed <= 90)
        {
            SlotController.instance.maxSlotCount = 5;
        }
        else
        {
            SlotController.instance.maxSlotCount = 6;
        }
        SlotController.instance.percent = player.speed;
        SlotController.instance.fixCount = 2; //무조건 두 칸 이동은 고정
    }
}
