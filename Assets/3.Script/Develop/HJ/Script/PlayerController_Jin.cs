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
            Debug.Log("오아튼에 도착!");
            return true;
        }
        else if (map.objectIndex[1] == myHexNum)
        {
            Debug.Log("우드스모크에 도착!");
            if (quest.questTurn == 2)
            {
                quest.PopUp("WoodSmoke");
                quest.questTurn = 3;
            }
            return true;
        }
        else if (map.objectIndex[2] == myHexNum)
        {
            Debug.Log("눈부신 광산에 도착!");
            return true;
        }
        else if (map.objectIndex[3] == myHexNum)
        {
            Debug.Log("패리드에 도착!");
            //if (quest) quest 클리어 설정해주기 (추후에)
            return true;
        }
        else if (map.objectIndex[4] == myHexNum)
        {
            Debug.Log("잊혀진 저장고에 도착!");
            return true;
        }
        else if (map.objectIndex[5] == myHexNum)
        {
            Debug.Log("카젤리의 시계에 도착!");
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
