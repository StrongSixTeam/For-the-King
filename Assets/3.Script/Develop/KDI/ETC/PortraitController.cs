using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitController : MonoBehaviour
{
    //플레이어 세 명의 인덱스나 위치를 비교 -> 한명만 / 두명 / 세명
    //한 명일때는 move = new Vector3(0, 1f, 0);
    //두 명일때는 앞번호 친구를 new Vector3(-0.5f, 1f, 0) 뒤친구를 new Vector3(0.5f, 1f, 0)
    //세 명일때는 (0, 1.5f, 0) (-0.5f, 1f, 0) (0.5f, 1f, 0)으로

    int count;
    [SerializeField] int samePosition = 0;
    int index1 = 0;
    int index2 = 0;
    int index3 = 0;
    public List<MovingUI> movingUIs;
    public GameObject camera;

    private void Update()
    {
        if (GameManager.instance.isSettingDone)
        {
            count = GameManager.instance.Players.Count;
            CheckPositions();
            if (GameManager.instance.Players.Count != movingUIs.Count && camera.activeSelf)
            {
                for(int i =0; i < movingUIs.Count; i++)
                {
                    if (!movingUIs[i].gameObject.activeSelf)
                    {
                        movingUIs.RemoveAt(GameManager.instance.deadIndex);
                        movingUIs[i].Player = null;
                        break;
                    }
                }
                for (int i = 0; i < movingUIs.Count; i++)
                {
                    movingUIs[i].order = i;
                }
                for (int i = 0; i < GameManager.instance.Players.Count; i++)
                {
                    movingUIs[i].Player = GameManager.instance.Players[i].transform;
                }
            }
        }
    }
    private void CheckPositions()
    {
        if (count == 1) //전체 플레이어가 한 명
        {
            samePosition = 1;
        }
        else if (count == 2) //전체 플레이어가 두 명
        {
            
            
            if (GameManager.instance.Players[0].transform.position == GameManager.instance.Players[1].transform.position)
            {
                index1 = 0;
                index2 = 1;
                samePosition = 2;
            }
            else
            {
                samePosition = 1;
            }
        }
        else //전체 플레이어가 세 명
        {
            if (GameManager.instance.Players[0].transform.position == GameManager.instance.Players[1].transform.position && GameManager.instance.Players[1].transform.position == GameManager.instance.Players[2].transform.position)
            {
                samePosition = 3;
            }
            else
            {
                if (GameManager.instance.Players[0].transform.position == GameManager.instance.Players[1].transform.position)
                {
                    index1 = 0;
                    index2 = 1;
                    index3 = 2;
                    samePosition = 2;
                }
                else if (GameManager.instance.Players[0].transform.position == GameManager.instance.Players[2].transform.position)
                {
                    index1 = 0;
                    index2 = 2;
                    index3 = 1;
                    samePosition = 2;
                }
                else if (GameManager.instance.Players[1].transform.position == GameManager.instance.Players[2].transform.position)
                {
                    index1 = 1;
                    index2 = 2;
                    index3 = 0;
                    samePosition = 2;
                }
                else
                {
                    samePosition = 1;
                }
            }
        }

        if (samePosition == 3)
        {
            transform.GetChild(0).GetComponent<PortraitUI>().move = new Vector3(0, 2f, 0);
            transform.GetChild(1).GetComponent<PortraitUI>().move = new Vector3(-0.5f, 1f, 0);
            transform.GetChild(2).GetComponent<PortraitUI>().move = new Vector3(0.5f, 1f, 0);
        }
        else if (samePosition == 2)
        {
            transform.GetChild(index1).GetComponent<PortraitUI>().move = new Vector3(-0.5f, 1f, 0);
            transform.GetChild(index2).GetComponent<PortraitUI>().move = new Vector3(0.5f, 1f, 0);
            transform.GetChild(index3).GetComponent<PortraitUI>().move = new Vector3(0f, 1f, 0);
        }
        else
        {
            transform.GetChild(0).GetComponent<PortraitUI>().move = new Vector3(0, 1f, 0);
            transform.GetChild(1).GetComponent<PortraitUI>().move = new Vector3(0, 1f, 0);
            transform.GetChild(2).GetComponent<PortraitUI>().move = new Vector3(0, 1f, 0);
        }
    }
}
