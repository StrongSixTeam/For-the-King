using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudBox : MonoBehaviour
{

    [SerializeField] GameObject cloud01;
    [SerializeField] GameObject cloud02;
    [SerializeField] GameObject cloud03;

    //HexMapCreator에서 노드 생성 완료 후, CloudObj 생성
    [SerializeField] HexMapCreator hexMapCreator;

    GameObject[] clouds = new GameObject[3];
    [SerializeField] GameObject[] cloudBox;

    int zPlusNum=0;

    List<int> closeIndex = new List<int>();

    private void Start()
    {
        hexMapCreator = FindObjectOfType<HexMapCreator>();

        clouds[0] = cloud01;
        clouds[1] = cloud02;
        clouds[2] = cloud03;
        cloudBox = new GameObject[hexMapCreator.hexMembers.Length];

        for (int i=0; i<hexMapCreator.hexMembers.Length; i++)
        {
            cloudBox[i] = Instantiate(clouds[Random.Range(0, 3)]);
            cloudBox[i].transform.position = hexMapCreator.hexMembers[i].transform.position + new Vector3(0f, 2f, 0f);
            cloudBox[i].transform.SetParent(gameObject.transform);
        }
    }

    public void CloudActiveFalse(int hexIndex)
    {
        zPlusNum = -180;

        for (int z=0; z<8; z++)
        {
            for (int x=-4; x<5; x++)
            {
                if (!closeIndex.Contains(hexIndex + x + zPlusNum))
                {
                    closeIndex.Add(hexIndex + x + zPlusNum);
                    StartCoroutine(ActiveFalseCo(hexIndex + x + zPlusNum));
                }
            }
            zPlusNum += 60;
        }
    }

    IEnumerator ActiveFalseCo(int index)
    {
        for (int i = 0; i < 10; i++)
        {
            cloudBox[index].transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        cloudBox[index].SetActive(false);
        yield break;
    }
}
