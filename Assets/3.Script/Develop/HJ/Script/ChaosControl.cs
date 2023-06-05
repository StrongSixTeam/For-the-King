using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosControl : MonoBehaviour
{
    [SerializeField] Transform[] parentsBar = new Transform[15];
    [SerializeField] Transform[] parentsChaos = new Transform[2];
    [SerializeField] GameObject[] chaosImageBox = new GameObject[4];

    int chaosTurn = 5; //ī���� ���� ��
    int maxChaosCount = 0; //ī������ ������ �� ��� ++

    public int endBarIndex;

    Queue<int> FIFO = new Queue<int>();

    void Start()
    {
        for(int i=0; i<4; i++)
        {
            chaosImageBox[i].SetActive(false);
        }

        endBarIndex = 14;
        maxChaosCount = 0;

        FIFO.Clear();
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
            //�ǿ����ʿ� �ִ� �ٿ� ��ӽ�������
            for(int i=0; i<4; i++)
            {
                if (!chaosImageBox[i].activeSelf)
                {
                    StartCoroutine(CreateChaosCo(i));
                    break;
                }
            }

            chaosTurn = 5;
        }
    }

    IEnumerator CreateChaosCo(int i)
    {
        yield return new WaitForSeconds(1f);
        chaosImageBox[i].SetActive(true);
        chaosImageBox[i].transform.SetParent(parentsBar[endBarIndex]);
        chaosImageBox[i].transform.localPosition = new Vector3(0f, 0.1f, 0f);
        FIFO.Enqueue(i);
        yield break;
    }
    
    public void RemoveChaos(bool isMax)
    {
        if(FIFO.Count <= 0)
        {
            return;
        }

        switch (isMax)
        {
            case true:
                maxChaosCount++;

                switch (maxChaosCount)
                {
                    case 1:
                        GameObject temp1 = chaosImageBox[FIFO.Dequeue()];
                        temp1.transform.SetParent(parentsChaos[0]);
                        temp1.transform.localPosition = Vector3.zero;
                        break;

                    case 2:
                        GameObject temp2 = chaosImageBox[FIFO.Dequeue()];
                        temp2.transform.SetParent(parentsChaos[1]);
                        temp2.transform.localPosition = Vector3.zero;

                        GameManager.instance.currentLife = 0;
                        break;
                }
                break;

            case false: //ī���� ����
                chaosImageBox[FIFO.Dequeue()].SetActive(false);
                break;
        }
    }

}
