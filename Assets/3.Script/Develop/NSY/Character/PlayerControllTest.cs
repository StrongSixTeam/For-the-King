using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{

    public int myHexNumber = 1350; //�÷��̾ ����ִ� ��
    public List<HexMember> targetNode = new List<HexMember>();
    private Animator playerAni;


    private void Awake()
    {
        TryGetComponent(out playerAni);
    }


    //Astar ��ũ��Ʈ���� ȣ��
    public void StartedMove(List<HexMember> finalNodeList)
    {
        targetNode = finalNodeList;
        StartCoroutine(MoveTarget());
        playerAni.SetBool("MapRun", true);
    }

    private IEnumerator MoveTarget()
    {
        for (int i = 0; i < targetNode.Count;)
        {

            RotationNode(i);
            while (Vector3.Distance(transform.position, targetNode[i].transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetNode[i].transform.position, 2f * Time.deltaTime);
                yield return null;
            }

            transform.position = targetNode[i].transform.position;
            i++;
            myHexNumber = targetNode[i].index;
        }
        targetNode.Clear();
        yield break;
    }



    //�÷��̾��� �̵��� ���߰� ���� �� ȣ��
    public void StoppedMove(int stopIndex)
    {
        StopAllCoroutines();
        playerAni.SetBool("MapRun", false);
        for (int i = 0; i < targetNode.Count; i++)
        {
            if (targetNode[i].index == stopIndex)
            {
                while (targetNode[targetNode.Count - 1].index != stopIndex)
                {
                    targetNode.RemoveAt(targetNode.Count - 1);
                }

                //Ÿ�ٳ�带 �缳���ϰ� ���� �̵��Ѵ�
                StartCoroutine(MoveTarget());
                return;
            }
        }

    }

    private void RotationNode(int index)
    {
        Vector3 direction = targetNode[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }


}
