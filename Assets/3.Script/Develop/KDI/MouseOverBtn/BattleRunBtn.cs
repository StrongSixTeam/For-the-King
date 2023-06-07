using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleRunBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject[] icons;
    private BattleOrderManager battleOrderManager;
    [SerializeField] private GameObject battleFightBtn;
    [SerializeField] private Text infoText;

    public GameObject Accuracy;
    public GameObject Damage;
    private Vector3 fightPos = new Vector3(2.1f, 0f, 0f);
    private Vector3 runPos = new Vector3(-58f, 0f, 0f);
    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = "µµ¸Á°¡±â";
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
        Accuracy.SetActive(true);
        Damage.SetActive(false);
        Accuracy.transform.localPosition = runPos;
        Accuracy.transform.GetChild(0).GetComponent<Text>().text = SlotController.instance.percent.ToString() + "%"; //½½·Ô´ç È®·ü
    }
}
