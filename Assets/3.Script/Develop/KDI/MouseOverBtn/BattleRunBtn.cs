using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleRunBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject[] icons;
    private BattleOrderManager battleOrderManager;
    [SerializeField] private GameObject battleFightBtn;
    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (battleFightBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            FindObjectOfType<BattleManager>().FindPlayer(battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order).GetComponent<PlayerStat>().nowFocus += battleFightBtn.GetComponent<RightClick>().usedFocus;
            battleFightBtn.GetComponent<RightClick>().usedFocus = 0;
            GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
        }
        slot.GetComponent<CloneSlot>().isShowText = false;
        slot.GetComponent<CloneSlot>().runOut = true;
        SlotController.instance.maxSlotCount = 2;
        SlotController.instance.limit = 2;
        SlotController.instance.type = SlotController.Type.move;
        SlotController.instance.percent = battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().speed;
        slot.SetActive(true);
        icons[0].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[1].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[2].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        slot.GetComponent<CloneSlot>().Initialized();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
