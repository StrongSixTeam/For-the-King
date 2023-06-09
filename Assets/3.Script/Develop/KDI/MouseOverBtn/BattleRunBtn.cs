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
        if (battleFightBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus += battleFightBtn.GetComponent<RightClick>().usedFocus;
            battleFightBtn.GetComponent<RightClick>().usedFocus = 0;
            GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
            slot.GetComponent<CloneSlot>().Initialized();
        }
        SetText();
    }
    public void SetText()
    {
        infoText.text = "��������";
        int extraPercent = 0;
        if (GetComponent<RightClick>().usedFocus == 1)
        {
            extraPercent = 10;
        }
        else if (GetComponent<RightClick>().usedFocus == 2)
        {
            extraPercent = 15;
        }
        else if (GetComponent<RightClick>().usedFocus == 3)
        {
            extraPercent = 18;
        }
        else if (GetComponent<RightClick>().usedFocus == 4)
        {
            extraPercent = 20;
        }
        slot.GetComponent<CloneSlot>().isShowText = false;
        slot.GetComponent<CloneSlot>().runOut = true;
        SlotController.instance.maxSlotCount = 2;
        SlotController.instance.limit = 2;
        SlotController.instance.type = SlotController.Type.move;
        SlotController.instance.percent = GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().speed;
        SlotController.instance.percent += extraPercent;
        if (SlotController.instance.percent > 100)
        {
            SlotController.instance.percent = 100;
        }
        slot.SetActive(true);
        icons[0].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[1].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[2].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        slot.GetComponent<CloneSlot>().Initialized();
        Accuracy.SetActive(true);
        Damage.SetActive(false);
        Accuracy.transform.localPosition = runPos;
        Accuracy.transform.GetChild(0).GetComponent<Text>().text = SlotController.instance.percent.ToString() + "%"; //���Դ� Ȯ��
    }
}
