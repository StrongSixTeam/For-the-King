using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosControl : MonoBehaviour
{
    [SerializeField] Transform[] parentsBar = new Transform[15];
    [SerializeField] GameObject[] chaosImageBox = new GameObject[4];

    int chaosTurn = 6; //ī���� ���� ��

    public int endBarIndex;

    Queue<int> FIFO = new Queue<int>();

    void Start()
    {
        for(int i=0; i<4; i++)
        {
            chaosImageBox[i].SetActive(false);
        }

        endBarIndex = 14;
    }

    //1�ð��� ����������
    public void CountChaosTurn()
    {
        if(endBarIndex == 14)
        {
            endBarIndex = 0;
        }
        else
        {
            endBarIndex++;
        }


        chaosTurn--;
        if (chaosTurn < 0)
        {
            Debug.Log("ī���� ����");

            //�ǿ����ʿ� �ִ� �ٿ� ��ӽ�������
            for(int i=0; i<6; i++)
            {
                if (!chaosImageBox[i].activeSelf)
                {
                    chaosImageBox[i].SetActive(true);
                    chaosImageBox[i].transform.SetParent(parentsBar[endBarIndex]);
                    chaosImageBox[i].transform.localPosition = new Vector3(0f, 0.1f, 0f);
                    FIFO.Enqueue(i);
                    chaosTurn = 2;
                    return;
                }
            }
        }
    }
    
    public void RemoveChaos()
    {
        //���� ���ʿ� �ִ� ī������ ��Ȱ��ȭ
        chaosImageBox[FIFO.Dequeue()].SetActive(false);
    }

}
