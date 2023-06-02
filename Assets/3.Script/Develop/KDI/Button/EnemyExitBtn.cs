using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyExitBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    private EncounterManager encounterManager;
    public int n;
    public bool active = true;

    private void Start()
    {
        encounterManager = FindObjectOfType<EncounterManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (active)
        {
            if (transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus > 0)
            {
                GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus += transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus;
                transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus = 0;
            }
            n = EncounterManager.instance.number;
            int en = EncounterManager.instance.enemyNumber;
            EncounterManager.instance.txtContext.text = "이전 블록으로 돌아갑니다";
            if (en >= 0)
            {
                encounterManager.EnemyExitBtn(en);
            }
            else
            {
                encounterManager.EncounterEnemyExitBtn(n);
            }
            slot.GetComponent<CloneSlot>().isShowText = false;
        }
    }

}
