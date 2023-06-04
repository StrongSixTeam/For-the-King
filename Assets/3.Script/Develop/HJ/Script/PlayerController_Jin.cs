using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //�÷��̾ ����ִ� ��
    public List<HexMember> targetNodes = new List<HexMember>();
    private MapObjectCreator map;
    private QuestManager quest;
    private Animator animator;
    private CloudBox cloudBox;

    private AstsrPathfinding astsrPathfinding;

    public bool isRun;

    public HexMember saveWay;

    public int monsterIndex;

    private void Start()
    {
        map = FindObjectOfType<MapObjectCreator>();
        quest = FindObjectOfType<QuestManager>();
        gameObject.SetActive(true);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        animator = GetComponent<Animator>();
        cloudBox = FindObjectOfType<CloudBox>();
        cloudBox.CloudActiveFalse(myHexNum);
        astsrPathfinding = FindObjectOfType<AstsrPathfinding>();
    }


    //Astar ��ũ��Ʈ���� ȣ��
    public void StartMove(List<HexMember> finalNodeList)
    {
        if (finalNodeList.Count <= 0)
        {
            return;
        }
        if (finalNodeList[finalNodeList.Count - 1].index == myHexNum)
        {
            isRun = false;
            return;
        }
        targetNodes = finalNodeList;

        StartCoroutine(MoveTargetNode());
        isRun = true;

        animator.SetBool("MapRun", isRun);
    }

    private bool CheckObject()
    {
        if (map.objectIndex[0] == myHexNum) //����ư
        {
            EncounterManager.instance.ActiveEncounter(0);
            return true;
        }
        else if (map.objectIndex[1] == myHexNum) //��彺��ũ
        {
            if (quest.questTurn == 2)
            {
                quest.PopUp("WoodSmoke");
                quest.questTurn = 3;
            }
            else
            {
                EncounterManager.instance.ActiveEncounter(1);
            }
            return true;
        }
        else if (map.objectIndex[2] == myHexNum && EncounterManager.instance.encounter[2].isShowed && !EncounterManager.instance.encounter[2].isCleared) //�Ŷ��ǽĵ���
        {
            EncounterManager.instance.ActiveEncounter(2);
            return true;
        }
        else if (map.objectIndex[3] == myHexNum && EncounterManager.instance.encounter[3].isShowed && !EncounterManager.instance.encounter[2].isCleared) //ī���� ��θӸ�
        {
            EncounterManager.instance.ActiveEncounter(3);
            return true;
        }
        else if (map.objectIndex[4] == myHexNum && EncounterManager.instance.encounter[4].isShowed) //���ν� ����
        {
            EncounterManager.instance.ActiveEncounter(4);
            return true;
        }
        else if (map.objectIndex[5] == myHexNum) //�и���
        {
            EncounterManager.instance.ActiveEncounter(5);
            return true;
        }
        else if (map.objectIndex[6] == myHexNum && EncounterManager.instance.encounter[5].isShowed) //������ �����
        {
            EncounterManager.instance.ActiveEncounter(6);
            return true;
        }
        else if (map.objectIndex[7] == myHexNum) //ī������ �ð�
        {
            EncounterManager.instance.ActiveEncounter(7);
            return true;
        }
        else if (map.objectIndex[8] == myHexNum && EncounterManager.instance.encounter[7].isShowed) //��ü�� ���Ͻ�
        {
            EncounterManager.instance.ActiveEncounter(8);
            return true;
        }
        else if (map.objectIndex[9] == myHexNum) //���� ����
        {
            EncounterManager.instance.ActiveEncounter(9);
            return true;
        }
        else if (map.objectIndex[10] == myHexNum) //���� ����
        {
            EncounterManager.instance.ActiveEncounter(10);
            return true;
        }
        else if (map.objectIndex[11] == myHexNum) //���� ����
        {
            EncounterManager.instance.ActiveEncounter(11);
            return true;
        }
        else if (map.randomObjectIndex[0] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(12);
            return true;
        }
        else if (map.randomObjectIndex[1] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(13);
            return true;
        }
        else if (map.randomObjectIndex[2] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(14);
            return true;
        }
        else if (map.randomObjectIndex[3] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(15);
            return true;
        }
        else if (map.randomObjectIndex[4] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(16);
            return true;
        }
        else if (map.randomObjectIndex[5] == myHexNum) //����01=0, ����02, ����03, ����04, ����ǥ, ����ǥ=5
        {
            EncounterManager.instance.ActiveEncounter(17);
            return true;
        }
        else
        {
            for (int i = 0; i < map.randomMonsterIndex.Count; i++)
            {
                if (map.randomMonsterIndex[i] == myHexNum)
                {
                    monsterIndex = i;
                    EncounterManager.instance.ActiveEnemies(map.randomMonsterName[i]);
                    return true;
                }
            }
            return false;
        }
    }

    private IEnumerator MoveTargetNode()
    {
        List<HexMember> nowtTargetNodes = new List<HexMember>();
        nowtTargetNodes = targetNodes;
        for (int i = 0; i < nowtTargetNodes.Count; i++)
        {
            for (int q = 0; q < 6; q++)
            {
                if ((i + 1) >= nowtTargetNodes.Count)
                {
                    break;
                }
                if (map.randomObjectIndex[q] == nowtTargetNodes[i + 1].index)
                {
                    map.ShowRandomObject(q);
                }
            }

            int origin = myHexNum;
            //Rotation(i);

            Vector3 direction = targetNodes[i].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rTime = 0f;

            while (Vector3.Distance(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0)) > 0.01f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rTime);
                rTime += Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0), 4f * Time.deltaTime);

                yield return null;
            }
            

            transform.position = nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0);
            myHexNum = nowtTargetNodes[i].index;
            cloudBox.CloudActiveFalse(myHexNum);


            if (myHexNum != origin)
            {
                if (CheckObject()) //���� ������Ʈ�� �����ϸ�
                {
                    //���̵��Ѹ�ŭ canMoveCount�� ��������
                    astsrPathfinding.SetcanMoveCount((nowtTargetNodes.Count - 1) - i);
                    GameManager.instance.ActivePortrait();
                    StartCoroutine(SwitchScaleCo(false));
                    saveWay = nowtTargetNodes[i-1]; 
                    CheckMyHexNum();
                    nowtTargetNodes.Clear();
                    targetNodes.Clear();
                    animator.SetBool("MapRun", false);
                    astsrPathfinding.SetisPathfinding();
                    isRun = false;
                    yield break;
                }
                else //�����̶��
                {
                    GameManager.instance.DeactivePortrait();
                    StartCoroutine(SwitchScaleCo(true));
                }
            }
        }
        CheckMyHexNum();
        nowtTargetNodes.Clear();
        targetNodes.Clear();
        isRun = false;
        animator.SetBool("MapRun", false);
        astsrPathfinding.SetisPathfinding(); //�������� ������ �ٽ� ã��
        yield break;
    }
    public void BeOriginalScale()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void CheckMyHexNum()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 10f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<HexMember>() != null)
            {
                myHexNum = hits[i].transform.GetComponent<HexMember>().index;
                cloudBox.CloudActiveFalse(myHexNum);
                return;
            }
        }
        
    }

    IEnumerator SwitchScaleCo(bool active)
    {
        switch (active)
        {
            case true: //��Ÿ��
                if(gameObject.transform.localScale == new Vector3(1f, 1f, 1f))
                {
                    yield break;
                }
                for (int i = 0; i < 20; i++)
                {
                    float velue = (0.05f * i);
                    gameObject.transform.localScale = new Vector3(velue, velue, velue);
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                break;

            case false: //�����
                if (gameObject.transform.localScale != new Vector3(1f, 1f, 1f))
                {
                    yield break;
                }
                for (int i = 20; i >= 0; i--)
                {
                    float velue = (0.05f * i);
                    gameObject.transform.localScale = new Vector3(velue, velue, velue);
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
                break;
        }
    }

    //�÷��̾� ������ ������Ʈ�� ����Ʈ�� ��ȯ����������
    public List<GameObject> CheckAroundObject()
    {
        List<GameObject> aroundObj = new List<GameObject>();

        aroundObj = map.CheckAround(myHexNum);

        //�ֺ��� �÷��̾ �����ֳ���
        List<GameObject> temp = new List<GameObject>();
        temp = astsrPathfinding.GetPlayerHexNums(this, myHexNum);

        while (temp.Count>0)
        {
            aroundObj.Add(temp[0]);
            temp.RemoveAt(0);
        }

        //������ ������Ʈ, �������Ϳ�����Ʈ, �÷��̾������Ʈ�� ����
        return aroundObj;
    }


    ////�÷��̾��� �̵��� ���߰� ���� �� ȣ��
    //public void StopMove(int stopIndex)
    //{
    //    StopAllCoroutines();
    //    for (int i = 0; i < targetNodes.Count; i++)
    //    {
    //        if (targetNodes[i].index == stopIndex)
    //        {
    //            animator.SetBool("MapRun", false);

    //            while (targetNodes[targetNodes.Count - 1].index != stopIndex)
    //            {
    //                targetNodes.RemoveAt(targetNodes.Count - 1);
    //            }

    //            //Ÿ�ٳ�带 �缳���ϰ� ���� �̵��Ѵ�
    //            StartCoroutine(MoveTargetNode());
    //            return;
    //        }
    //    }

    //}

    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }


}
