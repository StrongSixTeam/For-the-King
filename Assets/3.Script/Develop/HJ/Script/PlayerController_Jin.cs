using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Jin : MonoBehaviour
{

    public int myHexNum = 1350; //플레이어가 밟고있는 땅
    public List<HexMember> targetNodes = new List<HexMember>();
    private MapObjectCreator map;
    private QuestManager quest;
    private Animator animator;
    private CloudBox cloudBox;

    private AstsrPathfinding astsrPathfinding;

    public bool isRun;

    private void Start()
    {
        map = FindObjectOfType<MapObjectCreator>();
        quest = FindObjectOfType<QuestManager>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        cloudBox = FindObjectOfType<CloudBox>();
        cloudBox.CloudActiveFalse(myHexNum);
        astsrPathfinding = FindObjectOfType<AstsrPathfinding>();
    }


    //Astar 스크립트에서 호출
    public void StartMove(List<HexMember> finalNodeList)
    {
        if (finalNodeList.Count<=0)
        {
            return;
        }
        if(finalNodeList[finalNodeList.Count-1].index == myHexNum)
        {
            isRun = false;
            return;
        }
        targetNodes = finalNodeList;

        StartCoroutine(MoveTargetNode());
        isRun = true;

        animator.SetBool("MapRun", isRun);
    }

    private bool CheckObject()
    {
        if (map.objectIndex[0] == myHexNum) //오아튼
        {
            EncounterManager.instance.ActiveEncounter(0);
            return true;
        }
        else if (map.objectIndex[1] == myHexNum) //우드스모크
        {
            if (quest.questTurn == 2)
            {
                quest.PopUp("WoodSmoke");
                quest.questTurn = 3;
            }
            else
            {
                EncounterManager.instance.ActiveEncounter(1);
            }
            return true;
        }
        else if (map.objectIndex[2] == myHexNum) //신의의식도구
        {
            EncounterManager.instance.ActiveEncounter(2);
            return true;
        }
        else if (map.objectIndex[3] == myHexNum) //카오스 우두머리
        {
            EncounterManager.instance.ActiveEncounter(3);
            //if (quest) quest 클리어 설정해주기 (추후에)
            return true;
        }
        else if (map.objectIndex[4] == myHexNum) //눈부신 광산
        {
            EncounterManager.instance.ActiveEncounter(4);
            return true;
        }
        else if (map.objectIndex[5] == myHexNum) //패리드
        {
            EncounterManager.instance.ActiveEncounter(5);
            return true;
        }
        else if (map.objectIndex[6] == myHexNum) //잊혀진 저장고
        {
            EncounterManager.instance.ActiveEncounter(6);
            return true;
        }
        else if (map.objectIndex[7] == myHexNum) //카젤리의 시계
        {
            EncounterManager.instance.ActiveEncounter(7);
            return true;
        }
        else if (map.objectIndex[8] == myHexNum) //시체의 지하실
        {
            EncounterManager.instance.ActiveEncounter(8);
            return true;
        }
        else if (map.objectIndex[9] == myHexNum) //집중 성소
        {
            EncounterManager.instance.ActiveEncounter(9);
            return true;
        }
        else if (map.objectIndex[10] == myHexNum) //생명 성소
        {
            EncounterManager.instance.ActiveEncounter(10);
            return true;
        }
        else if (map.objectIndex[11] == myHexNum) //지혜 성소
        {
            EncounterManager.instance.ActiveEncounter(11);
            return true;
        }
        else if (map.randomObjectIndex[0] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(12);
            return true;
        }
        else if (map.randomObjectIndex[1] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(13);
            return true;
        }
        else if (map.randomObjectIndex[2] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(14);
            return true;
        }
        else if (map.randomObjectIndex[3] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(15);
            return true;
        }
        else if (map.randomObjectIndex[4] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(16);
            return true;
        }
        else if (map.randomObjectIndex[5] == myHexNum) //몬스터01=0, 몬스터02, 몬스터03, 몬스터04, 물음표, 느낌표=5
        {
            EncounterManager.instance.ActiveEncounter(17);
            return true;
        }
        else 
        {
            return false;
        }
    }

    private IEnumerator MoveTargetNode()
    {
        List<HexMember> nowtTargetNodes = new List<HexMember>();
        nowtTargetNodes = targetNodes;
        for (int i = 0; i < nowtTargetNodes.Count; i++)
        {
            int origin = myHexNum;
            //Rotation(i);

            Vector3 direction = targetNodes[i].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rTime = 0f;

            while (Vector3.Distance(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0)) > 0.01f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rTime);
                rTime += Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0), 4f * Time.deltaTime);

                yield return null;
            }
            transform.position = nowtTargetNodes[i].transform.position + new Vector3(0, 0.1f, 0);
            myHexNum = nowtTargetNodes[i].index;
            cloudBox.CloudActiveFalse(myHexNum);


            if (myHexNum != origin)
            {
                if (CheckObject()) //현재 오브젝트에 도달하면
                {
                    //못이동한만큼 canMoveCount에 더해주자
                    astsrPathfinding.SetcanMoveCount((nowtTargetNodes.Count-1) - i);
                    Debug.Log((nowtTargetNodes.Count - 1) - i + "만큼 추가");

                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    CheckMyHexNum();
                    nowtTargetNodes.Clear();
                    targetNodes.Clear();
                    animator.SetBool("MapRun", false);
                    astsrPathfinding.SetisPathfinding();
                    yield break;
                }
                else //낫띵이라면
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        CheckMyHexNum();
        nowtTargetNodes.Clear();
        targetNodes.Clear();
        isRun = false;
        animator.SetBool("MapRun", false);
        astsrPathfinding.SetisPathfinding(); //움직임이 끝날때 다시 찾음
        yield break;
    }

    private void CheckMyHexNum()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.up, 10f);

        for(int i=0; i<hits.Length; i++)
        {
            if(hits[i].transform.GetComponent<HexMember>() != null)
            {
                myHexNum = hits[i].transform.GetComponent<HexMember>().index;
                cloudBox.CloudActiveFalse(myHexNum);
                return;
            }
        }

    }



    //플레이어의 이동을 멈추고 싶을 때 호출
    public void StopMove(int stopIndex)
    {
        StopAllCoroutines();
        for(int i=0; i<targetNodes.Count; i++)
        {
            if(targetNodes[i].index == stopIndex)
            {
                animator.SetBool("MapRun", false);

                while (targetNodes[targetNodes.Count-1].index != stopIndex)
                {
                    targetNodes.RemoveAt(targetNodes.Count-1);
                }

                //타겟노드를 재설정하고 마저 이동한다
                StartCoroutine(MoveTargetNode());
                return;
            }
        }

    } 

    private void Rotation(int index)
    {
        Vector3 direction = targetNodes[index].transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }


}
