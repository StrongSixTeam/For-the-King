using System.Collections.Generic;
using UnityEngine;


public class AstsrPathfinding : MonoBehaviour
{

    //11시 40분에 소영언니 플레이어컨트롤이랑 연결하기

    HexMapCreator hexMapCreator = new HexMapCreator();

    HexMember endNode, currentNode;
    HexMember saveTargetNode;

    List<HexMember> openList = new List<HexMember>();
    List<HexMember> closeList = new List<HexMember>();
    List<HexMember> finalNodeList = new List<HexMember>();

    [SerializeField] GameObject startPos, endPos;


    [SerializeField] GameObject[] moveNumberPrefabs = new GameObject[10];
    GameObject[] showMoveCount = new GameObject[10];
    bool ismovingTurn = false; //이걸 true로 바꾸면 A*가 가동되도록 //이동할때는 slotcontroller에서 success int 값 받으면 되겠쥬? - 단이언니
    [SerializeField] int canMoveCount = 5; //플레이어의 이동가능횟수 조절


    int loopCount = 0;
    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();

        for (int i = 0; i < 10; i++)
        {
            showMoveCount[i] = Instantiate(moveNumberPrefabs[i]);
            showMoveCount[i].SetActive(false);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) //(Input.GetMouseButton(0))
        //{
            MouseInput();

            if (endNode != null && endNode != saveTargetNode)
            {
                saveTargetNode = endNode;
                ShowMovingPath();
            }
        //}
    }

    //타겟노드 설정
    private void MouseInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //Debug.Log(hit.transform.gameObject.GetComponent<HexMember>().index);
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

        ListReset();
        openList.Add(startNode);


        while (openList.Count > 0)
        {
            currentNode = openList[0];

            for (int i = 0; i < openList.Count; i++)
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
            if (currentNode == endNode)
            {
                startPos.transform.position = startNode.transform.position;
                endPos.transform.position = endNode.transform.position;

                HexMember targetNode = endNode;
                while (targetNode != startNode)
                {
                    finalNodeList.Add(targetNode);
                    targetNode = targetNode.parentNode;
                }


                if (finalNodeList.Count > canMoveCount)
                {
                    loopCount = 0;
                    ListReset();
                    return;
                }

                finalNodeList.Add(startNode);
                finalNodeList.Reverse();

                ShowMoveNumber(finalNodeList);
                return;
            }

            //openList에 추가한다
            OpenListAdd(currentNode);


            loopCount++;
            if (loopCount > 500)
            {
                Debug.Log("무한루프");
                loopCount = 0;
                ListReset();
                return;
            }
        }

    }

    private void OpenListAdd(HexMember currentNode)
    {

        //6개의 이웃 중에서
        for (int i = 0; i < 6; i++)
        {
            //벽이 아니거나 closeList에 없다면 openList에 추가
            if (currentNode.neighbors[i].ispass &&
                !closeList.Contains(currentNode.neighbors[i]))
            {
                currentNode.neighbors[i].G = currentNode.G + 1;
                GetH(currentNode, i);

                currentNode.neighbors[i].parentNode = currentNode;

                openList.Add(currentNode.neighbors[i]);
            }
        }
    }

    private void GetH(HexMember currentNode, int i)
    {

        int dx = endNode.xNum - currentNode.neighbors[i].xNum; //타겟과 이웃노드의 x거리
        int dy = endNode.zNum - currentNode.neighbors[i].zNum; //타겟과 이웃노드의 z거리
        int x = Mathf.Abs(dx);
        int y = Mathf.Abs(dy);

        if ((dx < 0) ^ ((currentNode.neighbors[i].zNum & 1) == 1))
        {
            x = Mathf.Max(0, x - (y + 1) / 2);
        }
        else
        {
            x = Mathf.Max(0, x - (y) / 2);
        }

        currentNode.neighbors[i].H = x + y;

    }

    private void ShowMoveNumber(List<HexMember> finalNodeList)
    {
        for (int i = 0; i < finalNodeList.Count; i++)
        {
            showMoveCount[i].transform.position = finalNodeList[i].transform.position + new Vector3(0f, 0.2f, 0f);
            if (!showMoveCount[i].activeSelf)
            {
                showMoveCount[i].SetActive(true);
            }
        }
    }


    private void ListReset()
    {
        openList.Clear();
        closeList.Clear();
        finalNodeList.Clear();

        for (int i = 0; i < 10; i++)
        {
            if (showMoveCount[i].activeSelf)
            {
                showMoveCount[i].SetActive(false);
            }
        }
    }



    private void OnDrawGizmos()
    {

        if (finalNodeList != null)
        {
            for (int i = 0; i < finalNodeList.Count - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine
                    (new Vector3(finalNodeList[i].transform.position.x, 1f, finalNodeList[i].transform.position.z),
                    new Vector3(finalNodeList[i + 1].transform.position.x, 1f, finalNodeList[i + 1].transform.position.z));

            }
        }
    }
}
