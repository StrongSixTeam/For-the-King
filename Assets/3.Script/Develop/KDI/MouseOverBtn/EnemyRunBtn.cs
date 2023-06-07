using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyRunBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    private EncounterManager encounterManager;
    public int n;
    [SerializeField] private GameObject highlight;

    private void Start()
    {
        encounterManager = FindObjectOfType<EncounterManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (encounterManager.enemyButtonActive)
        {
            GetComponent<Button>().interactable = true;
            for (int i = 0; i < highlight.transform.childCount; i++)
            {
                highlight.transform.GetChild(i).gameObject.SetActive(false); //불러올때마다 하이라이트 끄기
            }
            n = EncounterManager.instance.number;
            int en = EncounterManager.instance.enemyNumber;
            if (en >= 0)
            {
                EncounterManager.instance.txtContext.text = EncounterManager.instance.enemies[en].Content;
                slot.GetComponent<CloneSlot>().isShowText = false;
                encounterManager.EnemyRunBtn(en);
            }
            else
            {
                EncounterManager.instance.txtContext.text = EncounterManager.instance.encounter[n].Content;
                slot.GetComponent<CloneSlot>().isShowText = false;
                encounterManager.EnemyRunBtn(n);
            }
        }
    }

}
