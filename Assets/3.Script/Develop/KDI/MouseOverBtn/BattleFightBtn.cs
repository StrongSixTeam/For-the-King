using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleFightBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject[] icons;
    private BattleOrderManager battleOrderManager;
    [SerializeField] private GameObject battleRunBtn;
    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (battleRunBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().nowFocus += battleRunBtn.GetComponent<RightClick>().usedFocus;
            battleRunBtn.GetComponent<RightClick>().usedFocus = 0;
            GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
        }
        slot.GetComponent<CloneSlot>().runOut = false;
        SlotController.instance.maxSlotCount = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon.maxSlot;
        SlotController.instance.type = FindObjectOfType<BattleManager>().AttackTypeToType(battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().weapon);
        if (SlotController.instance.type == SlotController.Type.attackBlackSmith)
        {
            SlotController.instance.percent = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().strength;
        }
        else if (SlotController.instance.type == SlotController.Type.attackHunter)
        {
            SlotController.instance.percent = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().awareness;
        }
        else if (SlotController.instance.type == SlotController.Type.attackScholar)
        {
            SlotController.instance.percent = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().intelligence;
        }
        slot.SetActive(true);
        icons[2].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[1].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        slot.GetComponent<CloneSlot>().Initialized();
        //±Û¾¾ ¶ß°Ô
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
