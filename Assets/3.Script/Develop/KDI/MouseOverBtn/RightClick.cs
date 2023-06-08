using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightClick : MonoBehaviour, IPointerClickHandler
{
    public int usedFocus = 0;
    public int order = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (order == 1)
            {
                BattleOrderManager battleOrderManager = FindObjectOfType<BattleOrderManager>();
                if (GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus > 0 && SlotController.instance.maxSlotCount > SlotController.instance.fixCount)
                {
                    usedFocus += 1;
                    FindObjectOfType<EncounterManager>().UseFocus();
                    FindObjectOfType<BattleFightBtn>().SetText();
                }
            }
            else if (order == 2)
            {
                BattleOrderManager battleOrderManager = FindObjectOfType<BattleOrderManager>();
                if (GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus > 0 && SlotController.instance.maxSlotCount > SlotController.instance.fixCount)
                {
                    usedFocus += 1;
                    FindObjectOfType<EncounterManager>().UseFocus();
                    FindObjectOfType<BattleRunBtn>().SetText();
                }
            }
            else
            {
                usedFocus += 1;
                FindObjectOfType<EncounterManager>().UseFocus();
            }
        }
    }
}