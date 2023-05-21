using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AstsrPathfinding : MonoBehaviour
{
    
    HexMapCreator hexMapCreator = new HexMapCreator();

    bool ismovingTurn = false;


    HexMember endNode, currentNode;

    List<HexMember> openList = new List<HexMember>();
    List<HexMember> closeList = new List<HexMember>();
    List<HexMember> finalNodeList = new List<HexMember>();

    int temp = 0;
    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseInput();
            if(endNode != null)
            {
                ShowMovingPath();
            } 
        }
    }

    //타겟노드 설정
    private void MouseInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            Debug.Log(hit.transform.gameObject.GetComponent<HexMember>().index);
            endNode = hit.transform.gameObject.GetComponent<HexMember>();
        }
    }

    
    public void ShowMovingPath()
    {
        ismovingTurn = true;
        Pathfinding(hexMapCreator.hexMembers[1350]);
    }

    //플레이어로부터 마우스까지의 패스파인딩
    private void Pathfinding(HexMember startNode)
    {
        
        openList.Add(startNode);

        
        while(openList.Count > 0)
        {
            currentNode = openList[0];

            for(int i=0; i<openList.Count; i++)
            {
                //열린 리스트 중, 현재노드보다 F가 작거나 같다면
                //H가 작은 것을 현재 노드로 설정
                if (openList[i].F <= currentNode.F &&
                    openList[i].H < currentNode.H)
                {
                    currentNode = openList[i];
                }
            }


            //열린 리스트에서 닫힌 리스트로 옮기기
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            //목적지에 도착했나요?
            if(currentNode == endNode)
            {
                HexMember targetNode = endNode;
                while (targetNode != startNode)
                {
                    finalNodeList.Add(targetNode);
                    targetNode = targetNode.parentNode;
                }

                finalNodeList.Add(startNode);
                finalNodeList.Reverse(); //순서를 거꾸로 뒤집니다

                for (int i = 0; i < finalNodeList.Count; i++)
                {
                    Debug.Log(i + "번 째는 " + finalNodeList[i].xNum + " | " + finalNodeList[i].zNum);

                }
                ListReset();
                return;
            }

            //openList에 추가한다
            OpenListAdd(currentNode);

            temp++;
            if(temp > 1000)
            {
                Debug.Log("무한루프");
                temp = 0;
                ListReset();
                return;
            }
        }

    }

    private void OpenListAdd(HexMember currentNode)
    {

        //6개의 이웃 중에서
        for (int i=0; i<6; i++)
        {
            //벽이 아니거나 closeList에 없다면 openList에 추가
            if (currentNode.neighbors[i].ispass &&
                !closeList.Contains(currentNode.neighbors[i]))
            {
                currentNode.neighbors[i].G = currentNode.G + 1;
                currentNode.neighbors[i].H =
                    (Mathf.Abs(currentNode.neighbors[i].xNum - endNode.zNum) //이거 비용계산 다시 해야함
                    + Mathf.Abs(currentNode.neighbors[i].zNum - endNode.zNum));

                currentNode.neighbors[i].parentNode = currentNode;

                openList.Add(currentNode.neighbors[i]);
            }
        }
    }


    private void ListReset()
    {
        openList.Clear();
        closeList.Clear();
        finalNodeList.Clear();
    }
}
