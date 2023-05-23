using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveSet : MonoBehaviour
{
    public CharacterStatusSet character;

    public void SetMove() //����� ĳ������ �ӵ� ���ȿ� ���� �ִ� ������ ���ϰ� Ȯ���� ���� �̵� => slotcontroller �����ߵ�
    {
        SlotController.instance.type = SlotController.Type.move;
        if (character.speed <= 50) //�ӵ� ���ȿ� ���� �ִ� ���� ���� ���ϱ�
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
        SlotController.instance.fixCount = 2; //�������� ��ĭ�� ����
    }
}
