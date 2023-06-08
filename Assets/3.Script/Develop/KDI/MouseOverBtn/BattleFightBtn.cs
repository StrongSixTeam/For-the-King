using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleFightBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject[] icons;
    private BattleOrderManager battleOrderManager;
    [SerializeField] private GameObject battleRunBtn;
    [SerializeField] private Text infoText;

    public GameObject Accuracy;
    public GameObject Damage;
    private Vector3 fightPos = new Vector3(2.1f, 0f, 0f);
    private Vector3 runPos = new Vector3(-58f, 0f, 0f);

    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }
    private void Start()
    {
        SetText();
    }
    public void SetText()
    {
        infoText.text = "ÀÏ¹Ý °ø°Ý";
        slot.GetComponent<CloneSlot>().runOut = false;

        GameObject mainPlayer = GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order];

        if (mainPlayer.GetComponent<PlayerStat>().weapon != null)
        {
            SlotController.instance.maxSlotCount = mainPlayer.GetComponent<PlayerStat>().weapon.maxSlot;
            SlotController.instance.type = FindObjectOfType<BattleManager>().AttackTypeToType(mainPlayer.GetComponent<PlayerStat>().weapon);
        }
        else
        {
            SlotController.instance.maxSlotCount = 0;
            SlotController.instance.type = SlotController.Type.empty;
        }
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
        if (SlotController.instance.type == SlotController.Type.attackBlackSmith)
        {
            SlotController.instance.percent = mainPlayer.GetComponent<PlayerStat>().strength;
        }
        else if (SlotController.instance.type == SlotController.Type.attackHunter)
        {
            SlotController.instance.percent = mainPlayer.GetComponent<PlayerStat>().awareness;
        }
        else if (SlotController.instance.type == SlotController.Type.attackScholar)
        {
            SlotController.instance.percent = mainPlayer.GetComponent<PlayerStat>().intelligence;
        }
        SlotController.instance.percent += extraPercent;
        if (SlotController.instance.percent > 100)
        {
            SlotController.instance.percent = 100;
        }
        
        Accuracy.SetActive(true);
        Damage.SetActive(true);
        Accuracy.transform.localPosition = fightPos;
        Accuracy.transform.GetChild(0).GetComponent<Text>().text = SlotController.instance.percent.ToString() + "%"; //½½·Ô´ç È®·ü
        Damage.transform.GetChild(0).GetComponent<Text>().text = mainPlayer.GetComponent<PlayerStat>().atk.ToString(); //´ë¹ÌÁö
        slot.SetActive(true);
        icons[2].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[1].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        slot.GetComponent<CloneSlot>().Initialized();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {  
        if (battleRunBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus += battleRunBtn.GetComponent<RightClick>().usedFocus;
            battleRunBtn.GetComponent<RightClick>().usedFocus = 0;
            GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
            slot.GetComponent<CloneSlot>().Initialized();
        }
        SetText();
        //±Û¾¾ ¶ß°Ô
    }
}
