using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyExitBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    private EncounterManager encounterManager;
    public int n;

    private void Start()
    {
        encounterManager = FindObjectOfType<EncounterManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus += transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus;
            transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus = 0;
        }
        n = EncounterManager.instance.number;
        EncounterManager.instance.txtContext.text = "이전 블록으로 돌아갑니다";
        slot.GetComponent<CloneSlot>().isShowText = false;
        encounterManager.EnemyExitBtn(n);
    }

}
