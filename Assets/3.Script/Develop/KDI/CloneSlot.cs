using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSlot : MonoBehaviour
{
    public Sprite[] move; //이동 UI 이미지
    public Sprite[] attackBlackSmith; //공격 UI 이미지 - 대장장이
    public Sprite[] attackHunter; //공격 UI 이미지 - 사냥꾼
    public Sprite[] attackScholar; //공격 UI 이미지 - 학자
    public void Initialized()
    {
        SlotController.instance.success = 0;
        SlotController.instance.fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //모든 오브젝트 끄기
            }
        }

        transform.GetChild(6).gameObject.SetActive(false); //텍스트 끄기

        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //전체 슬롯창 개수 맞춰서 키기
            if (SlotController.instance.type == SlotController.Type.move)
            {
                for (int j = 0; j < 3; j++)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = move[j]; //이미지 이동 이미지로 바꾸기
                    if (j == 0)
                    {
                        transform.GetChild(i).GetChild(0).gameObject.SetActive(true); //기본 이동 이미지 보여주기
                    }
                }
                transform.GetChild(6).gameObject.SetActive(true); //글씨 보여주기
            }
            else if (SlotController.instance.type == SlotController.Type.attackScholar)
            {
                for (int j = 0; j < 3; j++)
                {
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().sprite = attackScholar[j];
                    if (j == 0)
                    {
                        transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Try()
    {
        StartCoroutine(TryCo());
    }
    IEnumerator TryCo() //슬롯 보여주는 용
    {
        SlotController.instance.isSlot = true;
        int a = SlotController.instance.fixCount;
        for (int i = 0; i < SlotController.instance.maxSlotCount; i++)
        {
            yield return new WaitForSeconds(0.4f);
            if (SlotController.instance.fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //고정 이동칸
                SlotController.instance.fixCount--;
                SlotController.instance.success++;
                continue;
            }
            else
            {
                int j = Random.Range(0, 100);
                if (j < SlotController.instance.percent)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    SlotController.instance.success++;
                }
                else
                {
                    transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    SlotController.instance.fail++;
                }
            }
        }
        SlotController.instance.fixCount = a;
        SlotController.instance.isSlot = false;
    }
}
