using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���⼭����
    [Header("Node")]
    public HexMember targetNode;
    public HexMember middletargetNode;
    public List<HexMember> targetNodes = new List<HexMember>();

    bool moveSwitch = false;

    //[SerializeField] private float movementDuration = 1, rotationDuration = 0.3f;


    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            MouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSwitch = true;
            StartCoroutine(MoveTargetNode());
            // StartCoroutine(RotationCoroutine());
        }

        if (moveSwitch &&
            transform.position == targetNodes[targetNodes.Count - 1].transform.position)
        {
            //targetNodes.Clear();
            moveSwitch = false;
            Debug.Log("����");
        }
    }

    private void MouseInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            targetNode = hit.transform.gameObject.GetComponent<HexMember>();
            targetNodes.Add(targetNode);
        }
    }

    //��������� ���̽�Ÿ �ʿ��� ���ϴϱ� �׽�Ʈ ������ �ּ�ó���ϰų� ����ŵ� �˴ϴ�



    private IEnumerator MoveTargetNode()
    {
        for (int i = 0; i < targetNodes.Count;)
        {

            Rotation(i);
            while (Vector3.Distance(transform.position, targetNodes[i].transform.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetNodes[i].transform.position, 10f * Time.deltaTime);
                yield return null;
            }
            transform.position = targetNodes[i].transform.position;
            i++;
        }
        targetNodes.Clear();
        yield break;

        //����Ʈ �ȿ� �ִ� �迭 ����Ʈ �ȿ� �ִ� ����Ʈ���� �� �� ����� �Լ�, 
        //������� ������ � �͵��� �E �� �ְ� 
        //for���� �� ���� ���� Ŭ����� 
        /*
         
         �׷��� Ŭ�����ϴ� ������ �ٲ���
         ��尡 �ε��� ������ ��� ���� ���� ����
         */
    }


    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
