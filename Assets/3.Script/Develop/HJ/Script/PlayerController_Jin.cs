using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //플레이어가 밟고있는 땅
    public List<HexMember> targetNodes = new List<HexMember>();
    private MapObjectCreator map;
    private QuestManager quest;

    private void Start()
    {
        map = FindObjectOfType<MapObjectCreator>();
        quest = FindObjectOfType<QuestManager>();
    }


    //Astar 스크립트에서 호출
    public void StartMove(List<HexMember> finalNodeList)
    {
        targetNodes = finalNodeList;
        StartCoroutine(MoveTargetNode());
    }

    private bool CheckObject()
    {
        if (map.objectIndex[0] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.town; //scriptable data 짜도되고 연결 이쁘게만 하기
            EncounterManager.instance.ActiveEncounter("오아튼", "오아튼에 온걸 환영");
            return true;
        }
        else if (map.objectIndex[1] == myHexNum)
        {
            if (quest.questTurn == 2)
            {
                quest.PopUp("WoodSmoke");
                quest.questTurn = 3;
            }
            else
            {
                EncounterManager.instance.type = EncounterManager.Type.town;
                EncounterManager.instance.ActiveEncounter("우드스모크", "우드스모크 환영 티비");
            }
            return true;
        }
        else if (map.objectIndex[2] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.dungeon;
            EncounterManager.instance.ActiveEncounter("눈부신 광산", "여기는 던전 티비");
            return true;
        }
        else if (map.objectIndex[3] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.town;
            EncounterManager.instance.ActiveEncounter("패리드", "패리드 환영 티비");
            //if (quest) quest 클리어 설정해주기 (추후에)
            return true;
        }
        else if (map.objectIndex[4] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.dungeon;
            EncounterManager.instance.ActiveEncounter("잊혀진 저장고", "버려짐 티비");
            return true;
        }
        else if (map.objectIndex[5] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.town;
            EncounterManager.instance.ActiveEncounter("카젤리의 시계", "여기 마을 맞나?");
            return true;
        }
        else if (map.objectIndex[6] == myHexNum)
        {
            EncounterManager.instance.type = EncounterManager.Type.dungeon;
            EncounterManager.instance.ActiveEncounter("시체의 지하실", "던전 티비");
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator MoveTargetNode()
    {
        for (int i = 0; i < targetNodes.Count;)
        {

            Rotation(i);
            while (Vector3.Distance(transform.position, targetNodes[i].transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetNodes[i].transform.position, 4f * Time.deltaTime);
                yield return null;
            }

            transform.position = targetNodes[i].transform.position;
            i++;
            myHexNum = targetNodes[i].index;
            if (CheckObject()) //이동중 오브젝트를 만나면 이동 정지
            {
                yield break;
            }
        }
        targetNodes.Clear();
        yield break;
    }



    //플레이어의 이동을 멈추고 싶을 때 호출
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

                //타겟노드를 재설정하고 마저 이동한다
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
