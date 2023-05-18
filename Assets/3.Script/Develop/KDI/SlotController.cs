using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public static SlotController instance = null;

    public Sprite[] move; //이동 UI 이미지
    public Sprite[] attackScholar; //공격 UI 이미지 - 학자
    public Sprite[] attack; //뭐

    [Header("여기서 수치값 조정 가능 + 다른 스크립트에서도 참조 가능")]
    public int maxSlotCount; //육각형 슬롯 개수
    public int fixCount; //집중력 사용했을시 
    public int success = 0; //성공 몇개
    public int fail = 0; //실패 몇개
    public int percent = 40; //확률 몇퍼

    private void Awake()
    {
        instance = this;
    }
    public enum Type
    {
        move,
        attackScholar,
        attackBlackSmith,
        attackHunter
    }
    public Type type;

    private void Initialized() //초기화
    {
        success = 0;
        fail = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false); //모든 오브젝트 끄기
            }
        }
        transform.GetChild(6).gameObject.SetActive(false);

        for (int i = 0; i < maxSlotCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true); //전체 슬롯창 개수 맞춰서 키기
            if (type == Type.move)
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
            else if (type == Type.attackScholar)
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

    public void OnClick()
    {
        StopAllCoroutines(); //이미 실행중이면 멈추고시작
        Initialized();
        StartCoroutine(MakeMove());
    }

    IEnumerator MakeMove() //슬롯 보여주는 용(only)
    {
        for (int i = 0; i < maxSlotCount; i++)
        {
            transform.GetChild(6).GetComponent<Text>().text = "이동 판정 : " + success;
            yield return new WaitForSeconds(0.4f);
            if (fixCount > 0)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true); //고정 이동칸
                fixCount--;
                success++;
                continue;
            }
            else
            {
                int j = Random.Range(0, 100);
                if (j < percent)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    success++;
                }
                else
                {
                    transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    fail++;
                }
            }
        }
            transform.GetChild(6).GetComponent<Text>().text = "이동 판정 : " + success;
    }

}
