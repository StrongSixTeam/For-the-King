using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //�÷��̾ ����ִ� ��
    public List<HexMember> targetNodes = new List<HexMember>();
    private MapObjectCreator map;
    private QuestManager quest;

    public bool isRun;

    private void Start()
    {
        map = FindObjectOfType<MapObjectCreator>();
        quest = FindObjectOfType<QuestManager>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    //Astar ��ũ��Ʈ���� ȣ��
    public void StartMove(List<HexMember> finalNodeList)
    {
        targetNodes = finalNodeList;
        StartCoroutine(MoveTargetNode());
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
        else if (map.objectIndex[2] == myHexNum) //�����ǽĵ���
        {
            EncounterManager.instance.ActiveEncounter(2);
            return true;
        }
        else if (map.objectIndex[3] == myHexNum) //ī���� ��θӸ�
        {
            EncounterManager.instance.ActiveEncounter(3);
            //if (quest) quest Ŭ���� �������ֱ� (���Ŀ�)
            return true;
        }
        else if (map.objectIndex[4] == myHexNum) //���ν� ����
        {
            EncounterManager.instance.ActiveEncounter(4);
            return true;
        }
        else if (map.objectIndex[5] == myHexNum) //�и���
        {
            EncounterManager.instance.ActiveEncounter(5);
            return true;
        }
        else if (map.objectIndex[6] == myHexNum) //������ �����
        {
            EncounterManager.instance.ActiveEncounter(6);
            return true;
        }
        else if (map.objectIndex[7] == myHexNum) //ī������ �ð�
        {
            EncounterManager.instance.ActiveEncounter(7);
            return true;
        }
        else if (map.objectIndex[8] == myHexNum) //��ü�� ���Ͻ�
        {
            EncounterManager.instance.ActiveEncounter(8);
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator MoveTargetNode()
    {
        List<HexMember> nowtTargetNodes = new List<HexMember>();
        nowtTargetNodes = targetNodes;
        for (int i = 0; i < nowtTargetNodes.Count;)
        {
            int origin = myHexNum;
            Rotation(i);
            while (Vector3.Distance(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0)) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0), 4f * Time.deltaTime);
                yield return null;
            }
            transform.position = nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0);
            myHexNum = nowtTargetNodes[i].index;
            i++;
            if (myHexNum != origin)
            {
                if (CheckObject())
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    CheckMyHexNum();
                    nowtTargetNodes.Clear();
                    targetNodes.Clear();
                    yield break;
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        CheckMyHexNum();
        nowtTargetNodes.Clear();
        targetNodes.Clear();
        yield break;
    }

    private void CheckMyHexNum()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 10f);

        for(int i=0; i<hits.Length; i++)
        {
            if(hits[i].transform.GetComponent<HexMember>() != null)
            {
                myHexNum = hits[i].transform.GetComponent<HexMember>().index;
                return;
            }
        }

    }



    //�÷��̾��� �̵��� ���߰� ���� �� ȣ��
    public void StopMove(int stopIndex)
    {
        StopAllCoroutines();
        for(int i=0; i<targetNodes.Count; i++)
        {
            if(targetNodes[i].index == stopIndex)
            {
                while (targetNodes[targetNodes.Count-1].index != stopIndex)
                {
                    targetNodes.RemoveAt(targetNodes.Count-1);
                }

                //Ÿ�ٳ�带 �缳���ϰ� ���� �̵��Ѵ�
                StartCoroutine(MoveTargetNode());
                return;
            }
        }

    } 

    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }


}
