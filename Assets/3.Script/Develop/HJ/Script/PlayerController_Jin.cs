using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //플레이어가 밟고있는 땅
    public List<HexMember> targetNodes = new List<HexMember>();


    //Astar 스크립트에서 호출
    public void StartMove(List<HexMember> finalNodeList)
    {
        targetNodes = finalNodeList;
        StartCoroutine(MoveTargetNode());
    }

    private IEnumerator MoveTargetNode()
    {
        for (int i = 0; i < targetNodes.Count;)
        {

            Rotation(i);
            while (Vector3.Distance(transform.position, targetNodes[i].transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetNodes[i].transform.position, 2f * Time.deltaTime);
                yield return null;
            }
            transform.position = targetNodes[i].transform.position;
            i++;
            CheckNode();
        }
        targetNodes.Clear();
        yield break;
    }


    //플레이어가 이동을 멈췄을때 밑에 있는 노드 확인
    private void CheckNode()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, new Vector3(0f, -1f, 0f), 50f);

        for(int i=0; i<hits.Length; i++)
        {
            if(hits[i].transform.GetComponent<HexMember>() != null)
            {
                myHexNum = hits[i].transform.GetComponent<HexMember>().index;
                return;
            }
        }
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 50f, Color.black);

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
