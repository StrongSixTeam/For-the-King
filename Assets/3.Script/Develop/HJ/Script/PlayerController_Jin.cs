using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //�÷��̾ ����ִ� ��
    public List<HexMember> targetNodes = new List<HexMember>();


    //Astar ��ũ��Ʈ���� ȣ��
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


    //�÷��̾ �̵��� �������� �ؿ� �ִ� ��� Ȯ��
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