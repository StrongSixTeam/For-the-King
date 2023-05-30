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
    public EncounterContent[] encounter;

    [SerializeField] private GameObject[] btns;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject successCalc;
    public int number;

    private void Awake()
    {
        instance = this;
        parent = transform.parent;
    }
    public void ActiveEncounter(int n)
    {
        number = n;
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
            SlotController.instance.fixCount = 0;
            SlotController.instance.maxSlotCount = encounter[n].slotCount;
            SlotController.instance.type = StringToType(encounter[n].slotType);
            SlotController.instance.limit = encounter[n].limit;
            SlotController.instance.percent = FindTypePercent(encounter[n].slotType);
            slot.GetComponent<CloneSlot>().Initialized();
            slot.SetActive(true);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(true); //확률 결과도 보여주기
            successCalc.GetComponent<SuccessCalc>().Calculate(SlotController.instance.maxSlotCount, SlotController.instance.percent, SlotController.instance.limit);
        }
        else if (encounter[n].type == EncounterContent.Type.enemy)
        {
            ActiveBtn(2);
            SlotController.instance.fixCount = 0;
            SlotController.instance.maxSlotCount = encounter[n].enemyCount;
            SlotController.instance.type = SlotController.Type.empty;
            slot.GetComponent<CloneSlot>().Initialized();
            slot.SetActive(true);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
        else if (encounter[n].type == EncounterContent.Type.dungeon)
        {
            ActiveBtn(3);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
        else if (encounter[n].type == EncounterContent.Type.sanctum)
        {//집중력과 체력 모두 회복
            ActiveBtn(1);
            //ActiveBtn(4)
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void ExitButton()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        if (btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus += btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus;
            btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus = 0;
        }
    }

    private void ActiveBtn(int n)
    {
        for (int i =0; i < btns.Length; i++)
        {
            btns[i].SetActive(false);
        }
        btns[n].SetActive(true);
    }

    public void UseFocus()
    {
        if (GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus >= 1)
        {
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus -= 1;
            SlotController.instance.fixCount += 1;
            slot.GetComponent<CloneSlot>().Initialized();
            successCalc.GetComponent<SuccessCalc>().Calculate(SlotController.instance.maxSlotCount, SlotController.instance.percent, SlotController.instance.limit);
        }
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
            return SlotController.Type.empty;
        }
    }

    public void ClearBool(int n)
    {
        encounter[n].isCleared = true;
    }

    public void TryConnect()
    {
        slot.GetComponent<CloneSlot>().Try();
    }

    private int FindTypePercent(string some)
    {
        if (some.Equals("move"))
        {
            return GameManager.instance.MainPlayer.GetComponent<PlayerStat>().speed;
        }
        else if (some.Equals("attackBlackSmith"))
        {
            return GameManager.instance.MainPlayer.GetComponent<PlayerStat>().strength;
        }
        else if (some.Equals("attackHunter"))
        {
            return GameManager.instance.MainPlayer.GetComponent<PlayerStat>().awareness;
        }
        else if (some.Equals("attackScholar"))
        {
            return GameManager.instance.MainPlayer.GetComponent<PlayerStat>().intelligence;
        }
        else
        {
            return GameManager.instance.MainPlayer.GetComponent<PlayerStat>().speed;
        }
    }

    public void BattleBtn()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        MultiCamera.instance.MakeCloud();
    }

    public void EnemyRunBtn(int n)
    {
        slot.SetActive(true);
        SlotController.instance.fixCount = 0;
        SlotController.instance.maxSlotCount = encounter[n].slotCount;
        SlotController.instance.type = SlotController.Type.move;
        SlotController.instance.limit = SlotController.instance.maxSlotCount;
        SlotController.instance.percent = FindTypePercent("move");
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(true); //확률 결과도 보여주기
        successCalc.GetComponent<SuccessCalc>().Calculate(SlotController.instance.maxSlotCount, SlotController.instance.percent, SlotController.instance.limit);
    }

    public void EnemyFightBtn(int n)
    {
        ActiveBtn(2);
        SlotController.instance.fixCount = 0;
        SlotController.instance.maxSlotCount = encounter[n].enemyCount;
        SlotController.instance.type = SlotController.Type.empty;
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(false);
    }

    public void EnemyExitBtn(int n)
    {
        ActiveBtn(2);
        SlotController.instance.fixCount = 0;
        SlotController.instance.maxSlotCount = encounter[n].enemyCount;
        SlotController.instance.type = SlotController.Type.empty;
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(false);
    }
}
