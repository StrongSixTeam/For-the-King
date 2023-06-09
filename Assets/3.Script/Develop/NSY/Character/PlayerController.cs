using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //여기서부터
    [Header("Node")]
    public HexMember targetNode;
    public HexMember middletargetNode;
    public List<HexMember> targetNodes = new List<HexMember>();

    bool moveSwitch = false;
    private Animator playerAni;

    //[SerializeField] private float movementDuration = 1, rotationDuration = 0.3f;

    private void Awake()
    {
        TryGetComponent(out playerAni);
    }

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
            playerAni.SetBool("MapRun" ,true );
            // StartCoroutine(RotationCoroutine());
        }

        if (moveSwitch &&
            transform.position == targetNodes[targetNodes.Count - 1].transform.position)
        {
            //targetNodes.Clear();
            moveSwitch = false;
            Debug.Log("도착");
            playerAni.SetBool("MapRun", false);
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

    //여기까지는 에이스타 쪽에서 구하니까 테스트 끝나면 주석처리하거나 지우셔도 됩니당



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

        //리스트 안에 있는 배열 리스트 안에 있는 리스트들을 싹 다 지우는 함수, 
        //리무브는 지정한 어떤 것드을 뺼 수 있고 
        //for문을 다 돌고 나서 클리어는 
        /*
         
         그래서 클리어하는 위칯를 바꿔줌
         노드가 인덱스 범위를 벗어난 곳을 돌고 있음
         */
    }


    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
