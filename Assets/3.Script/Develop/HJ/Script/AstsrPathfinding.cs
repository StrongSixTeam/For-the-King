using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
    Transform redHexBoxTransform;

    public bool ismovingTurn = false; //이걸 true로 바꾸면 A*가 가동되도록 
    public int canMoveCount = 5; //플레이어의 이동가능횟수 조절 //이동할때는 slotcontroller에서 success int 값 받으면 되겠쥬? - 단이언니
    public int WhoseTurn; //0, 1, 2 플레이어 턴 지정 (누구의 playerController에 접근할건지)

    //PlayerSpawner가 SetPlayerCount(), SetPlayer()로 설정
    [SerializeField] PlayerController_Jin[] playerController;
    GameObject[] playerObject;

    [SerializeField] GameObject hexCursorRadPrefab;
    [SerializeField] GameObject hexCursorGreenPrefab;
    GameObject[] hexCursor = new GameObject[2];

    [SerializeField] GameObject redHex;
    GameObject[] redHexBox = new GameObject[20];

    [SerializeField] Texture2D cursotImage;
    [SerializeField] Texture2D cursotImageClick;

    bool cursorSwitch = false;
    int loopCount = 0;
    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();
        MoveCountBox = transform.GetChild(0);
        redHexBoxTransform = transform.GetChild(1);

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

        for (int i = 0; i < 20; i++)
        {
            GameObject temp = Instantiate(redHex);
            redHexBox[i] = temp;
            redHexBox[i].transform.SetParent(redHexBoxTransform);
            temp.SetActive(false);
        }

        Cursor.SetCursor(cursotImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SetPlayerCount(int playerCount)
    {
        playerController = new PlayerController_Jin[playerCount];
        playerObject = new GameObject[playerCount];
    }

    public void SetPlayer(int index, PlayerController_Jin player, GameObject playerObj)
    {
        playerController[index] = player;
        playerObject[index] = playerObj;
    }

    public List<GameObject> GetPlayerHexNums(PlayerController_Jin calledPlayer, int myHexNum)
    {
        List<GameObject> players = new List<GameObject>();
        for (int i = 0; i < playerController.Length; i++)
        {
            //호출한 본인을 제외한 플레이어
            if (playerController[i] != calledPlayer)
            {
                //myHexNum의 범위 안에 있는지 확인
                for (int e = 0; e < 6; e++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (hexMapCreator.hexMembers[myHexNum].neighbors[e].neighbors[j].index == playerController[i].myHexNum && !players.Contains(playerController[i].gameObject))
                        {
                            players.Add(playerController[i].gameObject);
                        }
                    }
                }
            }
        }

        return players;
    }

    void Update()
    {
        if (!GameManager.instance.isBlock)
        {
            MouseInput();

            if (endNode != null && isPathfinding)
            {

                Pathfinding(hexMapCreator.hexMembers[playerController[WhoseTurn].myHexNum]);

                //if (endNode != saveTargetNode)
                //{
                if (finalNodeList.Count>0 && endNode.transform.position == finalNodeList[finalNodeList.Count - 1].transform.position)
                {
                    ShowHexCursorGreen();
                }
                else
                {
                    ShowHexCursorRad();
                }
                //}
            }
            else if (endNode != null)
            {
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

            if (ismovingTurn)
            {
                if (Input.GetMouseButtonDown(0)) //왼쪽을 클릭하면
                {
                    if (!EventSystem.current.IsPointerOverGameObject()) // 포인터의 위치가 UI가 아니라면
                    {
                        if (endNode == null || !endNode.ispass || hexCursor[0].activeSelf || endNode.index == playerController[WhoseTurn].myHexNum)
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
                            //Debug.Log(canMoveCount + "칸 이동할수있는데 " + (finalNodeList.Count - 1) + "이동 | 남은수 : " + (canMoveCount - (finalNodeList.Count - 1)));
                            canMoveCount -= (finalNodeList.Count - 1);
                        }
                        else
                        {
                            //Debug.Log("전부 소비! 턴 종료");
                            canMoveCount -= (finalNodeList.Count - 1);
                            ismovingTurn = false;
                        }
                        isPathfinding = false;
                    }
                }
            }

            if (SlotController.instance.success + SlotController.instance.fail == SlotController.instance.maxSlotCount && GameManager.instance.isTrunChange)
            {
                GameManager.instance.isTrunChange = false;
                isPathfinding = true;
                ismovingTurn = true;
                canMoveCount = SlotController.instance.success;
                canMoveCount = 9; //테스트용
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


        if (Input.GetMouseButtonDown(0) && !cursorSwitch)
        {
            Cursor.SetCursor(cursotImageClick, Vector2.zero, CursorMode.ForceSoftware);
            cursorSwitch = true;
        }
        else if (!Input.GetMouseButton(0) && cursorSwitch)
        {
            Cursor.SetCursor(cursotImage, Vector2.zero, CursorMode.ForceSoftware);
            cursorSwitch = false;
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
    public void SetcanMoveCount(int plus)
    {
        canMoveCount += plus;
    }

    //타겟노드 설정
    private void MouseInput()
    {
        //Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.RaycastAll(inputRay, out hit))
        //{
        //    //Debug.Log(hit.transform.gameObject.GetComponent<HexMember>().index);
        //    endNode = hit.transform.gameObject.GetComponent<HexMember>();
        //    //Debug.Log(endNode.H);
        //}


        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<HexMember>() != null)
            {
                endNode = hits[i].transform.GetComponent<HexMember>();
                return;
            }
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
                int loopNumber = 0;
                HexMember targetNode = endNode;
                while (targetNode != startNode)
                {
                    bool check = false;
                    for (int i = 0; i < 6; i++)
                    {
                        if (closeList.Contains(targetNode.parentNode.parentNode))
                        {
                            if (targetNode.neighbors[i] == targetNode.parentNode.parentNode)
                            {
                                finalNodeList.Add(targetNode);
                                targetNode = targetNode.parentNode.parentNode;
                                check = true;
                                break;
                            }
                        }
                    }
                    if (!check)
                    {
                        finalNodeList.Add(targetNode);
                        targetNode = targetNode.parentNode;
                    }

                    if (loopNumber++ > 1000)
                    {
                        //throw new System.Exception("finalNodeList 에러");
                        Debug.Log("finalNodeList 에러");
                        return;
                    }
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

    int[] dirLeftTop = new int[6] { 5, 4, 3, 1, 2, 0 };
    int[] dirLeft = new int[6] { 4, 5, 3, 1, 2, 0 };
    int[] dirLeftBottom = new int[6] { 3, 4, 5, 1, 2, 0 };
    int[] dirRightTop = new int[6] { 0, 1, 2, 4, 5, 3 };
    int[] dirRight = new int[6] { 1, 0, 2, 4, 5, 3 };
    int[] dirRightBottom = new int[6] { 2, 1, 0, 4, 5, 3 };
    private void OpenListAdd(HexMember currentNode)
    {
        if (currentNode.zNum % 2 == 1) //홀
        {
            //왼쪽
            if (endNode.zNum == currentNode.zNum && endNode.xNum < currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirLeft[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeft[i]]))
                    {
                        currentNode.neighbors[dirLeft[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeft[i]);

                        currentNode.neighbors[dirLeft[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeft[i]]);
                    }
                }
            }

            //왼쪽 대각선 위
            else if (endNode.zNum > currentNode.zNum && endNode.xNum <= currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    //벽이 아니거나 closeList에 없다면 openList에 추가
                    if (currentNode.neighbors[dirLeftTop[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeftTop[i]]))
                    {
                        currentNode.neighbors[dirLeftTop[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeftTop[i]);

                        currentNode.neighbors[dirLeftTop[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeftTop[i]]);
                    }
                }
            }

            //왼쪽 대각선 아래
            else if (endNode.zNum < currentNode.zNum && endNode.xNum <= currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirLeftBottom[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeftBottom[i]]))
                    {
                        currentNode.neighbors[dirLeftBottom[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeftBottom[i]);

                        currentNode.neighbors[dirLeftBottom[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeftBottom[i]]);
                    }
                }
            }

            //오른쪽
            else if (endNode.zNum == currentNode.zNum && endNode.xNum > currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRight[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRight[i]]))
                    {
                        currentNode.neighbors[dirRight[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRight[i]);

                        currentNode.neighbors[dirRight[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRight[i]]);
                    }
                }
            }

            //오른쪽 대각선 위
            else if (endNode.zNum > currentNode.zNum && endNode.xNum >= currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRightTop[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRightTop[i]]))
                    {
                        currentNode.neighbors[dirRightTop[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRightTop[i]);

                        currentNode.neighbors[dirRightTop[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRightTop[i]]);
                    }
                }
            }

            //오른쪽 대각선 아래
            else if (endNode.zNum < currentNode.zNum && endNode.xNum > currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRightBottom[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRightBottom[i]]))
                    {
                        currentNode.neighbors[dirRightBottom[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRightBottom[i]);

                        currentNode.neighbors[dirRightBottom[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRightBottom[i]]);
                    }
                }
            }

        }
        else //짝
        {
            //왼쪽
            if (endNode.zNum == currentNode.zNum && endNode.xNum < currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirLeft[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeft[i]]))
                    {
                        currentNode.neighbors[dirLeft[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeft[i]);

                        currentNode.neighbors[dirLeft[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeft[i]]);
                    }
                }
            }

            //왼쪽 대각선 위
            else if (endNode.zNum > currentNode.zNum && endNode.xNum < currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirLeftTop[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeftTop[i]]))
                    {
                        currentNode.neighbors[dirLeftTop[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeftTop[i]);

                        currentNode.neighbors[dirLeftTop[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeftTop[i]]);
                    }
                }
            }

            //왼쪽 대각선 아래
            else if (endNode.zNum < currentNode.zNum && endNode.xNum < currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirLeftBottom[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirLeftBottom[i]]))
                    {
                        currentNode.neighbors[dirLeftBottom[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirLeftBottom[i]);

                        currentNode.neighbors[dirLeftBottom[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirLeftBottom[i]]);
                    }
                }
            }

            //오른쪽
            else if (endNode.zNum == currentNode.zNum && endNode.xNum > currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRight[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRight[i]]))
                    {
                        currentNode.neighbors[dirRight[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRight[i]);

                        currentNode.neighbors[dirRight[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRight[i]]);
                    }
                }
            }

            //오른쪽 대각선 위
            else if (endNode.zNum > currentNode.zNum && endNode.xNum >= currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRightTop[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRightTop[i]]))
                    {
                        currentNode.neighbors[dirRightTop[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRightTop[i]);

                        currentNode.neighbors[dirRightTop[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRightTop[i]]);
                    }
                }
            }

            //오른쪽 대각선 아래
            else if (endNode.zNum < currentNode.zNum && endNode.xNum >= currentNode.xNum)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (currentNode.neighbors[dirRightBottom[i]].ispass &&
                    !closeList.Contains(currentNode.neighbors[dirRightBottom[i]]))
                    {
                        currentNode.neighbors[dirRightBottom[i]].G = currentNode.G + 1;
                        GetH(currentNode, dirRightBottom[i]);

                        currentNode.neighbors[dirRightBottom[i]].parentNode = currentNode;

                        openList.Add(currentNode.neighbors[dirRightBottom[i]]);
                    }
                }
            }

        }

    }

    int xNum;
    int zNum;

    private void GetH(HexMember currentNode, int indexNum)
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

        xNum = currentNode.neighbors[indexNum].xNum;
        zNum = currentNode.neighbors[indexNum].zNum;

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
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        zNum++;
                        cost++;
                    }
                    else
                    {
                        xNum--;
                        zNum++;
                        cost++;
                    }
                }
                else if (endNode.zNum < zNum)
                {
                    //endNode의 왼쪽대각선 아래\
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        zNum--;
                        cost++;
                    }
                    else
                    {
                        xNum--;
                        zNum--;
                        cost++;
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
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        xNum++;
                        zNum++;
                        cost++;
                    }
                    else
                    {
                        zNum++;
                        cost++;
                    }
                }
                else if (endNode.zNum < zNum)
                {
                    //endNode의 오른쪽대각선 아래
                    if (zNum % 2 == 1) //z가 홀수
                    {
                        xNum++;
                        zNum--;
                        cost++;
                    }
                    else
                    {
                        zNum--;
                        cost++;
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

        currentNode.neighbors[indexNum].H = cost;

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

    public void ShowRedHex(int centerIndex)
    {
        List<int> close = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (hexMapCreator.hexMembers[centerIndex].neighbors[i].neighbors[j].ispass &&
                    !close.Contains(hexMapCreator.hexMembers[centerIndex].neighbors[i].neighbors[j].index))
                {
                    for (int e = 0; e < 20; e++)
                    {
                        if (!redHexBox[e].activeSelf)
                        {
                            redHexBox[e].SetActive(true);
                            redHexBox[e].transform.position = hexMapCreator.hexMembers[centerIndex].neighbors[i].neighbors[j].transform.position + new Vector3(0f, 0.2f, 0f); ;
                            close.Add(hexMapCreator.hexMembers[centerIndex].neighbors[i].neighbors[j].index);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void ShowRedHexStop()
    {
        for (int i = 0; i < 20; i++)
        {
            if (redHexBox[i].activeSelf)
            {
                redHexBox[i].SetActive(false);
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
