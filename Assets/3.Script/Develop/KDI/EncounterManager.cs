using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager instance = null;

    public Text txtName;
    public Text txtContext;
    public Text txtExtraContext;

    [SerializeField] private Transform parent;
    public EncounterContent[] encounter;
    public EncounterContent[] enemies;

    [SerializeField] private GameObject[] btns;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject successCalc;
    [SerializeField] private GameObject preview;
    [SerializeField] private GameObject highlight;
    public int number;
    public int enemyNumber;
    public bool isEncounterUI = false;
    public bool outsideCheck = false;

    private AstsrPathfinding astsrPathfinding;

    private void Awake()
    {
        instance = this;
        parent = transform.parent;
        astsrPathfinding = FindObjectOfType<AstsrPathfinding>();
        encounter[2].isShowed = false;
        encounter[3].isShowed = false;
        encounter[4].isShowed = false;
        encounter[8].isShowed = false;
        for (int i = 0; i < encounter.Length; i++)
        {
            encounter[i].isCleared = false;
        }
        for (int i =0; i < enemies.Length; i++)
        {
            enemies[i].isCleared = false;
        }
        for (int i = 0; i < highlight.transform.childCount; i++)
        {
            highlight.transform.GetChild(i).gameObject.SetActive(false);
        }
        highlight.SetActive(false);
    }

    private void Update()
    {
        if (parent.GetChild(1).gameObject.activeSelf || outsideCheck)
        {
            isEncounterUI = true;
        }
        else
        {
            isEncounterUI = false;
        }
    }
    public void ActiveEnemies(int n)
    {
        number = -1;
        enemyNumber = n;
        txtName.text = enemies[n].Name;
        txtContext.text = enemies[n].Content;
        txtExtraContext.text = enemies[n].extraContent;
        preview.GetComponent<Image>().sprite = enemies[n].preview;
        ActiveBtn(2);
        SlotController.instance.fixCount = 0;
        SlotController.instance.maxSlotCount = enemies[n].enemyCount;
        SlotController.instance.limit = enemies[n].limit;
        SlotController.instance.type = SlotController.Type.empty;
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(false);

        astsrPathfinding.ShowRedHex(GameManager.instance.Players[astsrPathfinding.WhoseTurn].GetComponent<PlayerController_Jin>().myHexNum);
    }

    public void ActiveEncounter(int n)
    {
        enemyNumber = -1;
        number = n;
        txtName.text = encounter[n].Name;
        txtContext.text = encounter[n].Content;
        preview.GetComponent<Image>().sprite = encounter[n].preview;

        if (encounter[n].extraContent != null)
        {
            txtExtraContext.text = encounter[n].extraContent;
        }
        else
        {
            txtExtraContext.text = "";
        }

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
            try
            {
                FindObjectOfType<EnemyBattleBtn>().active = true;
                FindObjectOfType<EnemyExitBtn>().active = true;
                FindObjectOfType<EnemyRunBtn>().active = true;

            }
            catch(System.Exception ex)
            {
                Debug.Log(ex);
            }
            
            btns[2].transform.GetChild(0).GetComponent<Button>().interactable = true;
            btns[2].transform.GetChild(2).GetComponent<Button>().interactable = true;
            btns[2].transform.GetChild(1).GetComponent<Button>().interactable = true;
            ActiveBtn(2);
            SlotController.instance.fixCount = 0;
            SlotController.instance.maxSlotCount = encounter[n].enemyCount;
            SlotController.instance.limit = encounter[n].limit;
            SlotController.instance.type = SlotController.Type.empty;
            slot.GetComponent<CloneSlot>().Initialized();
            slot.SetActive(true);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);

            txtName.text = encounter[n].Name;
            txtContext.text = encounter[n].Content;
            txtExtraContext.text = encounter[n].extraContent;
            preview.GetComponent<Image>().sprite = encounter[n].preview;
        }
        else if (encounter[n].type == EncounterContent.Type.dungeon)
        {
            ActiveBtn(3);
            parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
            parent.GetChild(2).gameObject.SetActive(false);
        }
        else if (encounter[n].type == EncounterContent.Type.sanctum)
        {//성소 만나면
            if (!encounter[n].isCleared)
            { //남들이 거치지 않은 성소라면
                ActiveBtn(4);
                parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
                parent.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                //남들이 거친 성소라면 이미 사용된 성소라는 UI 띄우기
            }
        }
    }

    public void DisableButton()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        astsrPathfinding.ismovingTurn = true;
    }

    public void ExitButton()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off

        List<HexMember> temp = new List<HexMember>();
        temp.Add(GameManager.instance.Players[astsrPathfinding.WhoseTurn].GetComponent<PlayerController_Jin>().saveWay);
        GameManager.instance.Players[astsrPathfinding.WhoseTurn].GetComponent<PlayerController_Jin>().StartMove(temp);
        
        if (astsrPathfinding.canMoveCount > 0)
        {
            GameManager.instance.isBlock = false;
            astsrPathfinding.ismovingTurn = true;
        }

        if (btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus += btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus;
            btns[1].transform.GetChild(1).GetComponent<RightClick>().usedFocus = 0;
        }

        astsrPathfinding.ShowRedHexStop();
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

    public void MinusChaosBtn()
    {
        btns[5].SetActive(false);
        GameManager.instance.isBlock = false;
        FindObjectOfType<EncounterManager>().outsideCheck = false;
        FindObjectOfType<AstsrPathfinding>().ismovingTurn = true;
        FindObjectOfType<ChaosControl>().RemoveChaos(false);
        FindObjectOfType<AstsrPathfinding>().ismovingTurn = true;

        if (FindObjectOfType<QuestManager>().questTurn == 5)
        {
            FindObjectOfType<QuestManager>().PopUp("God");
            FindObjectOfType<QuestManager>().questTurn = 6;
        }
    }

    public void PlusLifeBtn() 
    {
        //게임매니저 생명 늘리기
        btns[5].SetActive(false);
        FindObjectOfType<EncounterManager>().outsideCheck = false;
        FindObjectOfType<AstsrPathfinding>().ismovingTurn = false;
        FindObjectOfType<AstsrPathfinding>().ismovingTurn = true;

        if (FindObjectOfType<QuestManager>().questTurn == 5)
        {
            FindObjectOfType<QuestManager>().PopUp("God");
            FindObjectOfType<QuestManager>().questTurn = 6;
        }
    }

    public void TryConnect()
    {
        slot.GetComponent<CloneSlot>().Try();
        btns[2].transform.GetChild(0).GetComponent<Button>().interactable = false;
        btns[2].transform.GetChild(2).GetComponent<Button>().interactable = false;
        btns[2].transform.GetChild(1).GetComponent<Button>().interactable = false;
        FindObjectOfType<EnemyBattleBtn>().active = false;
        FindObjectOfType<EnemyExitBtn>().active = false;
        FindObjectOfType<EnemyRunBtn>().active = false;
        astsrPathfinding.ShowRedHexStop();
    }

    public void GodSuccess()
    {
        FindObjectOfType<EncounterManager>().outsideCheck = true;
        slot.SetActive(false);
        btns[1].SetActive(false);
        btns[5].SetActive(true);
        parent.GetChild(1).gameObject.SetActive(false);
        parent.GetChild(2).gameObject.SetActive(false);
        encounter[2].isCleared = true;
        FindObjectOfType<MapObjectCreator>().UseObject(0);
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

    public void ToMain()
    {
        MultiCamera.instance.ToMain();
    }

    public void DungeonEnterBtn()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        MultiCamera.instance.ToCave();
    }

    public void BattleBtn() //배틀씬으로 이동
    {
        OffMovingUIs();
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off
        MultiCamera.instance.ToBattle();

        astsrPathfinding.ShowRedHexStop();
        GameManager.instance.isBlock = true;
        //전투 끝났을때 다시 true로 돌려줘야함
    }

    public void EnemyRunBtn(int n)
    {
        slot.SetActive(true);
        SlotController.instance.fixCount = 0;
        if (enemyNumber >= 0)
        {
            SlotController.instance.maxSlotCount = enemies[n].slotCount;
        }
        else
        {
            SlotController.instance.maxSlotCount = encounter[n].slotCount;
        }
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
        if (enemyNumber >= 0)
        {
            SlotController.instance.maxSlotCount = enemies[n].enemyCount;
        }
        else
        {
            SlotController.instance.maxSlotCount = encounter[n].enemyCount;
        }
        SlotController.instance.type = SlotController.Type.empty;
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(false);
    }

    public void EncounterEnemyExitBtn(int n)
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
        SlotController.instance.maxSlotCount = enemies[n].enemyCount;
        SlotController.instance.type = SlotController.Type.empty;
        slot.GetComponent<CloneSlot>().Initialized();
        slot.SetActive(true);
        parent.GetChild(1).gameObject.SetActive(true); //EncountUI on
        parent.GetChild(2).gameObject.SetActive(false);
    }


    public void SanctumPrayBtn()
    {
        if (number == 9)
        {
            SanctumFocusBtn();
            encounter[number].isCleared = true;
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().whichSanctum = PlayerStat.Sanctum.focus;
            FindObjectOfType<MapObjectCreator>().UseObject(1);
        }
        else if (number == 10)
        {
            SanctumLifeBtn();
            encounter[number].isCleared = true;
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().whichSanctum = PlayerStat.Sanctum.life;
            FindObjectOfType<MapObjectCreator>().UseObject(2);
        }
        else if (number == 11)
        {
            SanctumIntelBtn();
            encounter[number].isCleared = true;
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().whichSanctum = PlayerStat.Sanctum.wisdom;
            FindObjectOfType<MapObjectCreator>().UseObject(3);
        }
    }

    public void SanctumFocusBtn()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off

        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxFocus += 2;
        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxFocus;
        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxHp;
    }
    public void SanctumLifeBtn()
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off

        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxHp += 10;
        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxFocus;
        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxHp;
    }
    public void SanctumIntelBtn() //추가 경험치 25%
    {
        slot.SetActive(false);
        parent.GetChild(1).gameObject.SetActive(false); //EncountUI off
        parent.GetChild(2).gameObject.SetActive(false); //SlotUI off

        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxFocus;
        GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp = GameManager.instance.MainPlayer.GetComponent<PlayerStat>().maxHp;
    }

    public void OffMovingUIs() //UI 끄기
    {
        MovingUI[] movingUIs = FindObjectsOfType<MovingUI>();
        for (int i = 0; i < movingUIs.Length; i++)
        {
            movingUIs[i].gameObject.SetActive(false);
        }
        PortraitUI[] portraitUIs = FindObjectsOfType<PortraitUI>();
        for (int i = 0; i < movingUIs.Length; i++)
        {
            portraitUIs[i].gameObject.SetActive(false);
        }
    }

    public void OnMovingUIs() //UI 키기
    {
        MovingUI[] movingUIs = FindObjectsOfType<MovingUI>();
        for (int i = 0; i < movingUIs.Length; i++)
        {
            movingUIs[i].gameObject.SetActive(true);
        }
        PortraitUI[] portraitUIs = FindObjectsOfType<PortraitUI>();
        for (int i = 0; i < movingUIs.Length; i++)
        {
            portraitUIs[i].gameObject.SetActive(true);
        }
    }
}
