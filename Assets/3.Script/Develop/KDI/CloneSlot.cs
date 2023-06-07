using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSlot : MonoBehaviour
{
    [SerializeField] private GameObject Get;
    private bool isSuccess = false;
    public Sprite[] move; //이동 UI 이미지
    public Sprite[] attackBlackSmith; //공격 UI 이미지 - 대장장이
    public Sprite[] attackHunter; //공격 UI 이미지 - 사냥꾼
    public Sprite[] attackScholar; //공격 UI 이미지 - 학자
    public Sprite empty; //빈 이미지
    public bool isShowText = true;
    [SerializeField] private GameObject highlight;

    public bool hasLimit = true;
    public bool runOut = false;
    public bool playerTurn = true;
    public void Initialized()
    {
        isSuccess = false;
        SlotController.instance.success = 0;
        SlotController.instance.fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //모든 오브젝트 끄기
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(6).gameObject.SetActive(false); //텍스트 끄기

        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //전체 슬롯창 개수 맞춰서 키기
            if (SlotController.instance.type == SlotController.Type.move)
            {
                if (i < SlotController.instance.fixCount) //집중력 사용했으면
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }

                if (isShowText)
                {
                    transform.GetChild(6).gameObject.SetActive(true); //글씨 보여주기
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackScholar)
            {
                if (i < SlotController.instance.fixCount) //집중력 사용했으면
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackHunter)
            {
                if (i < SlotController.instance.fixCount) //집중력 사용했으면
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackHunter[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackHunter[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackBlackSmith)
            {
                if (i < SlotController.instance.fixCount) //집중력 사용했으면
                {
                    for (int j = 0; j < 3; j++) //show focus
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackBlackSmith[j];
                        if (j == 1)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++) //show pre
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackBlackSmith[j];
                        if (j == 0)
                        {
                            transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (SlotController.instance.type == SlotController.Type.empty)
            {
                transform.GetChild(6).gameObject.SetActive(false); //텍스트 끄기
                for (int j = 0; j < 3; j++) //show focus
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = empty;
                    if (j == 1)
                    {
                        transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Try()
    {
        isSuccess = false;
        StartCoroutine(TryCo());
    }
    IEnumerator TryCo() //슬롯 보여주는 용
    {
        SlotController.instance.isSlot = true;
        int a = SlotController.instance.fixCount;
        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            yield return new WaitForSeconds(0.3f);
            if (SlotController.instance.fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //고정 이동칸
                SlotController.instance.fixCount--;
                SlotController.instance.success++;
                continue;
            }
            else
            {
                int j = Random.Range(0, 100);
                if (j < SlotController.instance.percent)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    SlotController.instance.success++;
                }
                else
                {
                    transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    SlotController.instance.fail++;
                }
            }
        }
        SlotController.instance.fixCount = a;
        SlotController.instance.isSlot = false;

        if (SlotController.instance.hasLimit) //공격씬이 아니면
        {
            //그라데이션 켜기
            highlight.SetActive(true);
            highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).gameObject.SetActive(true);
            //하이라이트 처리
            highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).GetChild(0).GetComponent<Text>().text = SlotController.instance.success.ToString();
            if (EncounterManager.instance.number == 16) //물음표인 경우
            {
                GameManager.instance.MainPlayer.GetComponent<PlayerController_Jin>().BeOriginalScale();
                EncounterManager.instance.encounter[16].isCleared = true;
                GameManager.instance.DeactivePortrait();
                Invoke("OffAll", 1f);
                if (SlotController.instance.success == 3) //성공이면
                {
                    //아이템 하나 획득
                    Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.MainPlayer.transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                    txt.transform.SetParent(GameObject.Find("Canvas").transform);
                    txt.text = "+ " + FindObjectOfType<ItemInputTest1>().EatItem[3].itemName;
                    for (int i = 0; i < GameManager.instance.Players.Count; i++)
                    {
                        if (GameManager.instance.MainPlayer == GameManager.instance.Players[i])
                        {
                            if (i == 0)
                            {
                                InventoryController1.instance.playerNum = PlayerNum.Player0;
                            }
                            else if (i == 1)
                            {
                                InventoryController1.instance.playerNum = PlayerNum.Player1;
                            }
                            else if (i == 2)
                            {
                                InventoryController1.instance.playerNum = PlayerNum.Player2;
                            }
                        }
                    }
                    FindObjectOfType<ItemInputTest1>().Get(FindObjectOfType<ItemInputTest1>().EatItem[3]);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    FindObjectOfType<MapObjectCreator>().UseObject(4);
                }
                else if (SlotController.instance.success == 2) //아무일도 일어나지 않는다
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    FindObjectOfType<MapObjectCreator>().UseObject(4);
                }
                else if (SlotController.instance.success == 1) //+5 대미지
                {
                    Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.MainPlayer.transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                    txt.transform.SetParent(GameObject.Find("Canvas").transform);
                    txt.text = "+ 5 대미지";
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp -= 5;
                    FindObjectOfType<MapObjectCreator>().UseObject(4);
                }
                else //+10 대미지
                {
                    Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.MainPlayer.transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                    txt.transform.SetParent(GameObject.Find("Canvas").transform);
                    txt.text = "+ 10 대미지";
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp -= 10;
                    FindObjectOfType<MapObjectCreator>().UseObject(4);
                }
            }
            else if (EncounterManager.instance.enemyNumber >= 0) //몬스터랑 만난 경우
            {
                if (SlotController.instance.limit <= SlotController.instance.success) //성공이면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    Invoke("OffAll", 1f);
                }
                else //실패면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    StartCoroutine(BattleBtn_co());
                    Invoke("OffAll", 1f); //끄기
                }
            }
            else if (EncounterManager.instance.number >=12 && EncounterManager.instance.number <= 15) //숨겨진 적과 만나면
            {
                if (SlotController.instance.limit <= SlotController.instance.success) //성공이면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    Invoke("OffAll", 1f);
                }
                else //실패면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    StartCoroutine(BattleBtn_co());
                    Invoke("OffAll", 1f); //끄기
                }
            }
            else if (EncounterManager.instance.number == 2) //신도의식도구
            {
                if (SlotController.instance.success == 4) //성공이면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    StartCoroutine(GodSuccessWait_co());
                    Invoke("OffAll", 1f); //끄기
                }
                else if (SlotController.instance.success == 3) //성공이면
                {
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
                    StartCoroutine(GodSuccessWait_co());
                    Invoke("OffAll", 1f); //끄기
                }
                else if (SlotController.instance.success == 2)
                {
                    Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.MainPlayer.transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                    txt.transform.SetParent(GameObject.Find("Canvas").transform);
                    txt.text = "+ 5 대미지";
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowHp -= 5;
                    Invoke("OffAll", 1f); //끄기
                }
                else if (SlotController.instance.success == 1)
                {
                    for (int i =0; i < GameManager.instance.Players.Count; i++)
                    {
                        Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.Players[i].transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                        txt.transform.SetParent(GameObject.Find("Canvas").transform);
                        txt.text = "+ 5 파티 대미지";
                    }
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    for (int i = 0; i < GameManager.instance.Players.Count; i++)
                    {
                        GameManager.instance.Players[i].GetComponent<PlayerStat>().nowHp -= 5;
                    }
                    Invoke("OffAll", 1f); //끄기
                }
                else
                {
                    Text txt = Instantiate(Get, FindObjectOfType<Camera>().WorldToScreenPoint(GameManager.instance.MainPlayer.transform.position) + new Vector3(0, 100, 0), Quaternion.identity).GetComponent<Text>();
                    txt.transform.SetParent(GameObject.Find("Canvas").transform);
                    txt.text = "+ 1 카오스";
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                    highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                    FindObjectOfType<ChaosControl>().PlusChaos();
                    Invoke("OffAll", 1f); //끄기
                }
            }
            else
            {
                Invoke("OffAll", 1f); //끄기
            }
            FindObjectOfType<AstsrPathfinding>().ismovingTurn = true;
        }
        else //공격씬이면
        {
            if (runOut) //공격씬에서 도주하면
            {
                Invoke("slotsOff", 1f);
                if (SlotController.instance.limit <= SlotController.instance.success) //도주 성공이라면
                {
                    //int number = -1;
                    //for (int i =0; i < FindObjectOfType<BattleOrderManager>().Order.Count; i++)
                    //{
                    //    for (int j = 0; j < FindObjectOfType<BattleLoader>().Players.Count; j++)
                    //    {
                    //        if (FindObjectOfType<BattleOrderManager>().Order[i].name == FindObjectOfType<BattleLoader>().Players[j].name)
                    //        {
                    //            number = j;
                    //            break;
                    //        }
                    //    }
                    //    if (number >= 0)
                    //    {
                    //        break;
                    //    }
                    //}
                    Destroy(FindObjectOfType<BattleOrderManager>().Order[FindObjectOfType<BattleOrderManager>().turn]);

                    for (int i = 0; i < FindObjectOfType<BattleLoader>().Players.Count; i++)
                    {
                        if (FindObjectOfType<BattleLoader>().Players[i].Equals(FindObjectOfType<BattleOrderManager>().Order[FindObjectOfType<BattleOrderManager>().turn]))
                        {
                            FindObjectOfType<BattleLoader>().Players.RemoveAt(i);
                        }
                    }

                    FindObjectOfType<BattleOrderManager>().turn -= 1;

                    FindObjectOfType<BattleOrderManager>().SetOrder();

                    if (FindObjectOfType<BattleLoader>().Players.Count > 0)
                    {
                        FindObjectOfType<BattleManager>().RunFalse();
                    }

                    //if (number >= 0)
                    //{
                    //    Destroy(FindObjectOfType<BattleLoader>().Players[number]);
                    //    FindObjectOfType<BattleLoader>().Players.RemoveAt(number);
                    //
                    //    FindObjectOfType<BattleOrderManager>().SetOrder();
                    //
                    //    if (FindObjectOfType<BattleLoader>().Players.Count > 0)
                    //    {
                    //        FindObjectOfType<BattleManager>().RunFalse();
                    //    }
                    //}
                }
                else //도주 실패라면
                {
                    FindObjectOfType<BattleManager>().RunFalse();
                }
            }
            else //공격씬에서 공격이라면
            {
                if (!playerTurn) //적 턴이라면
                {
                    FindObjectOfType<BattleManager>().CalculateEnemyAtk();
                    Invoke("slotsOff", 2f);
                    Invoke("turnChange", 2f);
                }
                else //플레이어 턴이라면
                {
                    FindObjectOfType<BattleManager>().CalculateAtk();
                    FindObjectOfType<BattleManager>().MakeBullet();
                    Invoke("turnChange", 2f);
                    Invoke("slotsOff", 2f);
                }
            }
        }
    }

    private void turnChange()
    {
        if (playerTurn)
        {
            playerTurn = false;
        }
        else
        {
            playerTurn = true;
        }
    }

    private void slotsOff()
    {
        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator GodSuccessWait_co()
    {
        yield return new WaitForSeconds(0.9f);
        EncounterManager.instance.GodSuccess();
    }

    IEnumerator BattleBtn_co()
    {
        yield return new WaitForSeconds(0.9f);
        EncounterManager.instance.BattleBtn();
    }

    private void OffAll()
    {
        highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
        highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
        highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).gameObject.SetActive(false);
        highlight.SetActive(false);
        FindObjectOfType<EncounterManager>().DisableButton();
        EncounterManager.instance.enemyButtonActive = true;
    }
}
