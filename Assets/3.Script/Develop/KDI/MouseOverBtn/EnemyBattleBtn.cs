using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyBattleBtn : MonoBehaviour, IPointerEnterHandler
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
        if (encounterManager.enemyButtonActive)
        {
            GetComponent<Button>().interactable = true;
            if (transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus > 0)
            {
                GameManager.instance.MainPlayer.GetComponent<PlayerStat>().nowFocus += transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus;
                transform.parent.GetChild(1).GetComponent<RightClick>().usedFocus = 0;
            }

            n = EncounterManager.instance.number;
            int en = EncounterManager.instance.enemyNumber;

            if (en >= 0)
            {
                EncounterManager.instance.txtContext.text = EncounterManager.instance.enemies[en].Content;
                encounterManager.EnemyFightBtn(en);
            }
            else
            {
                EncounterManager.instance.txtContext.text = EncounterManager.instance.encounter[n].Content;
                encounterManager.EnemyFightBtn(n);
            }
            slot.GetComponent<CloneSlot>().isShowText = false;
        }
    }
}
