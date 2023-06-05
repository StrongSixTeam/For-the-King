using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSlot : MonoBehaviour
{
    private bool isSuccess = false;
    public Sprite[] move; //�̵� UI �̹���
    public Sprite[] attackBlackSmith; //���� UI �̹��� - ��������
    public Sprite[] attackHunter; //���� UI �̹��� - ��ɲ�
    public Sprite[] attackScholar; //���� UI �̹��� - ����
    public Sprite empty; //�� �̹���
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
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //��� ������Ʈ ����
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(6).gameObject.SetActive(false); //�ؽ�Ʈ ����

        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //��ü ����â ���� ���缭 Ű��
            if (SlotController.instance.type == SlotController.Type.move)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
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
                    transform.GetChild(6).gameObject.SetActive(true); //�۾� �����ֱ�
                }
            }
            else if (SlotController.instance.type == SlotController.Type.attackScholar)
            {
                if (i < SlotController.instance.fixCount) //���߷� ���������
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
                if (i < SlotController.instance.fixCount) //���߷� ���������
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
                if (i < SlotController.instance.fixCount) //���߷� ���������
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
                transform.GetChild(6).gameObject.SetActive(false); //�ؽ�Ʈ ����
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
    IEnumerator TryCo() //���� �����ִ� ��
    {
        SlotController.instance.isSlot = true;
        int a = SlotController.instance.fixCount;
        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            yield return new WaitForSeconds(0.3f);
            if (SlotController.instance.fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //���� �̵�ĭ
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

        if (SlotController.instance.hasLimit) //���ݾ��� �ƴϸ�
        {
            //�׶��̼� �ѱ�
            highlight.SetActive(true);
            highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).gameObject.SetActive(true);
            if (SlotController.instance.limit <= SlotController.instance.success)
            {
                //���� ó��
                if (EncounterManager.instance.enemyNumber >= 0) //���Ͷ� ���� ���
                {
                    Invoke("OffAll", 1f);
                }
                else if (EncounterManager.instance.number == 2) //�ŵ� �ǽ� ����
                {
                    StartCoroutine(GodSuccessWait_co());
                }
                else
                {
                    //���� ó�� isCleared
                    Invoke("OffAll", 1f);
                }
                //���̶���Ʈ ó��
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(true);
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).GetChild(0).GetComponent<Text>().text = SlotController.instance.success.ToString();
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                isSuccess = false;
                if (EncounterManager.instance.enemyNumber >= 0) //���Ͷ� ���� ���
                {
                    StartCoroutine(BattleBtn_co());
                }
                else if (EncounterManager.instance.number == 2) //�ŵ� �ǽ� ���� ���н�
                {
                    //���� ���Ƽ ó��
                }
                Invoke("OffAll", 1f); //����
                //���̶���Ʈ ó��
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(0).gameObject.SetActive(false);
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).GetChild(0).GetComponent<Text>().text = SlotController.instance.success.ToString();
                highlight.transform.GetChild(SlotController.instance.maxSlotCount - SlotController.instance.success).GetChild(1).gameObject.SetActive(true);
                FindObjectOfType<AstsrPathfinding>().ismovingTurn = true;
            }
        }
        else //���ݾ��̸�
        {
            if (runOut) //���ݾ����� �����ϸ�
            {
                Invoke("slotsOff", 1f);
                if (SlotController.instance.limit <= SlotController.instance.success) //���� �����̶��
                {
                    int number = -1;
                    for (int i =0; i < FindObjectOfType<BattleOrderManager>().Order.Count; i++)
                    {
                        for (int j = 0; j < FindObjectOfType<BattleLoader>().Players.Count; j++)
                        {
                            if (FindObjectOfType<BattleOrderManager>().Order[i].name == FindObjectOfType<BattleLoader>().Players[j].name)
                            {
                                number = j;
                                break;
                            }
                        }
                        if (number >= 0)
                        {
                            break;
                        }
                    }
                    if (number >= 0)
                    {
                        FindObjectOfType<BattleLoader>().Players.RemoveAt(number);
                    }
                }
                else //���� ���ж��
                {

                }
            }
            else //���ݾ����� �����̶��
            {
                if (!playerTurn) //�� ���̶��
                {
                    FindObjectOfType<BattleManager>().CalculateEnemyAtk();
                    Invoke("slotsOff", 2f);
                    Invoke("turnChange", 2f);
                }
                else //�÷��̾� ���̶��
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
        yield return new WaitForSeconds(1f);
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
    }
}
