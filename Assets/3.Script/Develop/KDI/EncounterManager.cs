using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance = null;

    public Text txtName;
    public Text txtContext;

    [SerializeField] private Transform parent;
    [SerializeField] private EncounterContent[] encounter;

    [SerializeField] private GameObject[] btns;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject successCalc;

    private void Awake()
    {
        instance = this;
        parent = transform.parent;
    }
    public void ActiveEncounter(int n)
    {

        txtName.text = encounter[n].Name;
        txtContext.text = encounter[n].Content;

        if (encounter[n].type == EncounterContent.Type.town) 
        {
            ActiveBtn(0);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        }
        else if (encounter[n].type == EncounterContent.Type.interactiveObject)
        {
            ActiveBtn(1);
            SlotController.instance.maxSlotCount = encounter[n].slotCount;
            SlotController.instance.type = StringToType(encounter[n].slotType);
            SlotController.instance.limit = encounter[n].limit;
            SlotController.instance.percent = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().awareness; //이거 ecounter type이랑 같은거 찾는 메소드 써서 바꾸기
            slot.GetComponent<CloneSlot>().Initialized();
            slot.SetActive(true);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(true); //확률 결과도 보여주기
            successCalc.GetComponent<SuccessCalc>().Calculate(SlotController.instance.maxSlotCount, SlotController.instance.percent, SlotController.instance.limit);
        }
        else if (encounter[n].type == EncounterContent.Type.enemy)
        {
            ActiveBtn(2);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
        else if (encounter[n].type == EncounterContent.Type.dungeon)
        {
            ActiveBtn(3);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void ExitButton()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
    }

    private void ActiveBtn(int n)
    {
        for (int i =0; i < btns.Length; i++)
        {
            btns[i].SetActive(false);
        }
        btns[n].SetActive(true);
    }

    private SlotController.Type StringToType(string some)
    {
        if (some.Equals("move"))
        {
            return SlotController.Type.move;
        }
        else if (some.Equals("attackBlackSmith"))
        {
            return SlotController.Type.attackBlackSmith;
        }
        else if (some.Equals("attackHunter"))
        {
            return SlotController.Type.attackHunter;
        }
        else if(some.Equals("attackScholar"))
        {
            return SlotController.Type.attackScholar;
        }
        else
        {
            return SlotController.Type.move;
        }
    }
}
