﻿using System.Collections.Generic;
using UnityEngine;


public class AstsrPathfinding : MonoBehaviour
{
    public bool isPathfinding = false;

    HexMapCreator hexMapCreator = new HexMapCreator();

    HexMember endNode, currentNode;
    HexMember saveTargetNode;

    List<HexMember> openList = new List<HexMember>();
    List<HexMember> closeList = new List<HexMember>();
    List<HexMember> finalNodeList = new List<HexMember>();


    [SerializeField] GameObject[] moveNumberPrefabs = new GameObject[10];
    GameObject[] showMoveCount = new GameObject[10];
    Transform MoveCountBox;

    [SerializeField] bool ismovingTurn = false; //이걸 true로 바꾸면 A*가 가동되도록 
    [SerializeField] int canMoveCount = 5; //플레이어의 이동가능횟수 조절 //이동할때는 slotcontroller에서 success int 값 받으면 되겠쥬? - 단이언니
    [SerializeField] int WhoseTurn; //0, 1, 2 플레이어 턴 지정 (누구의 playerController에 접근할건지)

    //PlayerSpawner가 SetPlayerCount(), SetPlayer()로 설정
    [SerializeField] PlayerController_Jin[] playerController;

    [SerializeField] GameObject hexCursorRadPrefab;
    [SerializeField] GameObject hexCursorGreenPrefab;
    GameObject[] hexCursor = new GameObject[2];


    int loopCount = 0;
    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();
        MoveCountBox = transform.GetChild(0);

        for (int i = 0; i < 10; i++)
        {
            showMoveCount[i] = Instantiate(moveNumberPrefabs[i]);
            showMoveCount[i].transform.SetParent(MoveCountBox);
            showMoveCount[i].SetActive(false);
        }

        //부모설정 캔버스로 해두자
        Transform gridCanvas = FindObjectOfType<HexMapCreator>().transform.GetChild(0);
        hexCursor[0] = Instantiate(hexCursorRadPrefab);
        hexCursor[1] = Instantiate(hexCursorGreenPrefab);
        hexCursor[0].transform.parent = gridCanvas;
        hexCursor[1].transform.parent = gridCanvas;
    }

    public void SetPlayerCount(int playerCount)
    {
        playerController = new PlayerController_Jin[playerCount];
    }

    public void SetPlayer(int index, PlayerController_Jin player)
    {
        playerController[index] = player;
    }

    void Update()
    {
        if (!GameManager.instance.isBlock)
        {
            MouseInput();

            if (endNode != saveTargetNode && endNode != null)
            {
                saveTargetNode = endNode;

                switch (endNode.ispass)
                {
                    case true:
                        ShowHexCursorGreen();
                        break;
                    case false:
                        ShowHexCursorRad();
                        break;
                }
            }

            if (isPathfinding && endNode != null)
            {
                Pathfinding(hexMapCreator.hexMembers[playerController[WhoseTurn].myHexNum]);

                if (endNode != saveTargetNode)
                {
                    for (int i = 0; i < finalNodeList.Count; i++)
                    {
                        if (endNode.transform.position != finalNodeList[i].transform.position)
                        {
                            ShowHexCursorRad();
                        }
                    }
                }
            }

            if (ismovingTurn)
            {
                if (Input.GetMouseButtonDown(0)) //왼쪽을 클릭하면
                {
                    if (!endNode.ispass || endNode == null)
                    {
                        return;
                    }

                    //이동
                    playerController[WhoseTurn].StartMove(finalNodeList);

                    //번호 없앰
                    for (int i = 0; i < 10; i++)
                    {
                        if (showMoveCount[i].activeSelf)
                        {
                            showMoveCount[i].SetActive(false);
                        }
                    }

                    if (canMoveCount - (finalNodeList.Count - 1) > 0)
                    {
                        Debug.Log(canMoveCount + "칸 이동할수있는데 " + (finalNodeList.Count - 1) + "이동 | 남은수 : " + (canMoveCount - (finalNodeList.Count - 1)));
                        canMoveCount -= (finalNodeList.Count - 1);
                    }
                    else
                    {
                        Debug.Log("전부 소비! 턴 종료");
                        canMoveCount -= (finalNodeList.Count - 1);
                        ismovingTurn = false;
                    }
                    isPathfinding = false;

                }
            }

            if (SlotController.instance.success + SlotController.instance.fail == SlotController.instance.maxSlotCount && GameManager.instance.isTrunChange)
            {
                GameManager.instance.isTrunChange = false;
                isPathfinding = true;
                ismovingTurn = true;
                canMoveCount = SlotController.instance.success;
                WhoseTurn = GameManager.instance.nextTurn - 1;
                if (WhoseTurn < 0)
                {
                    WhoseTurn = PlayerPrefs.GetInt("PlayerCnt") - 1;
                }
            }


        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                if (showMoveCount[i].activeSelf)
                {
                    showMoveCount[i].SetActive(false);
                }
            }
        }
    }

    public void SetisPathfinding()
    {
        //플레이어가 움직임을 끝내고 호출
        if (canMoveCount > 0)
        {
            isPathfinding = true;
        }
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
            //Debug.Log(endNode.H);
        }
    }


    private void ShowHexCursorRad()
    {
        if (!hexCursor[0].activeSelf)
        {
            hexCursor[0].SetActive(true);
        }
        if (hexCursor[1].activeSelf)
        {
            hexCursor[1].SetActive(false);
        }
        hexCursor[0].transform.position = new Vector3(endNode.transform.position.x, 0.1f, endNode.transform.position.z);
    }
    private void ShowHexCursorGreen()
    {
        if (!hexCursor[1].activeSelf)
        {
            hexCursor[1].SetActive(true);
        }
        if (hexCursor[0].activeSelf)
        {
            hexCursor[0].SetActive(false);
        }

        hexCursor[1].transform.position = new Vector3(endNode.transform.position.x, 0.1f, endNode.transform.position.z);
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
                HexMember targetNode = endNode;
                while (targetNode != startNode)
                {
                    finalNodeList.Add(targetNode);
                    targetNode = targetNode.parentNode;
                }

                if (finalNodeList.Count > canMoveCount)
                {
                    ShowHexCursorRad();
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
                loopCount = 0;
                ListReset();
                return;
            }
        }

    }

    int[] directionOrderLift = new int[6] { 4, 5, 3, 1, 0, 2 };
    int[] directionOrderRight = new int[6] { 1, 0, 2, 4, 5, 3 };
    private void OpenListAdd(HexMember currentNode)
    {
        try
        {
            if (currentNode.xNum > endNode.xNum) //목적지가 왼쪽이라면
            {
                //6개의 이웃 중에서
                for (int i = 0; i < 6; i++)
                {
                    //벽이 아니거나 closeList에 없다면 openList에 추가
                    if (currentNode.neighbors[directionOrderLift[i]].ispass &&
                        !closeList.Contains(currentNode.neighbors[directionOrderLift[i]]))
                    {
                        currentNode.neighbors[directionOrderLift[i]].G = currentNode.G + 1;
                        GetH(currentNode, directionOrderLift[i]);

                        currentNode.neighbors[directionOrderLift[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[directionOrderLift[i]]);
                    }
                }
            }
            else
            {
                //6개의 이웃 중에서
                for (int i = 0; i < 6; i++)
                {
                    //벽이 아니거나 closeList에 없다면 openList에 추가
                    if (currentNode.neighbors[directionOrderRight[i]].ispass &&
                        !closeList.Contains(currentNode.neighbors[directionOrderRight[i]]))
                    {
                        currentNode.neighbors[directionOrderRight[i]].G = currentNode.G + 1;
                        GetH(currentNode, directionOrderRight[i]);

                        currentNode.neighbors[directionOrderRight[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[directionOrderRight[i]]);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
            Debug.Log(currentNode);
            Debug.Log(endNode);
            Debug.Log(openList);
            Debug.Log(closeList);
        }
        
    }


    private void GetH(HexMember currentNode, int i)
    {

        //int dx = endNode.xNum - currentNode.neighbors[i].xNum; //타겟과 이웃노드의 x거리
        //int dy = endNode.zNum - currentNode.neighbors[i].zNum; //타겟과 이웃노드의 z거리
        //int x = Mathf.Abs(dx);
        //int y = Mathf.Abs(dy);

        //if ((dx < 0) ^ ((currentNode.neighbors[i].zNum & 1) == 1))
        //{
        //    x = Mathf.Max(0, x - (y + 1) / 2);
        //}
        //else
        //{
        //    x = Mathf.Max(0, x - (y) / 2);
        //}

        //currentNode.neighbors[i].H = x + y;



        //currentNode.neighbors[i]에서 endNode까지의 거리

        int xNum = currentNode.neighbors[i].xNum;
        int zNum = currentNode.neighbors[i].zNum;

        int cost = 0;

        int loopNum = 0;
        while (endNode.xNum != xNum || endNode.zNum != zNum)
        {

            //왼쪽
            if (endNode.xNum <= xNum)
            {
                if (endNode.zNum > zNum)
                {
                    //endNode의 왼쪽대각선위
                    cost++;
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        zNum++;
                    }
                    else
                    {
                        xNum--;
                        zNum++;
                    }
                }
                else if (endNode.zNum < zNum)
                {
                    //endNode의 왼쪽대각선 아래
                    cost++;
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        zNum--;
                    }
                    else
                    {
                        xNum--;
                        zNum--;
                    }
                }
                else
                {
                    //endNode의 왼쪽
                    cost++;
                    xNum--;
                }
                
            }
            else
            {
                if (endNode.zNum > zNum)
                {
                    //endNode의 오른쪽대각선위
                    cost++;
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        xNum++;
                        zNum++;
                    }
                    else
                    {
                        zNum++;
                    }
                }
                else if (endNode.zNum < zNum)
                {
                    //endNode의 오른쪽대각선 아래
                    cost++;
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        xNum++;
                        zNum--;
                    }
                    else
                    {
                        zNum--;
                    }
                }
                else
                {
                    //endNode의 오른쪽
                    cost++;
                    xNum++;
                }
            }

            if (loopNum++ > 1000)
            {
                throw new System.Exception("Astar H 계산 오류");
            }
        }

        currentNode.neighbors[i].H = cost;

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


}
