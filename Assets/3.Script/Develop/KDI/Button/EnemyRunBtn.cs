using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyRunBtn : MonoBehaviour, IPointerEnterHandler
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
        n = EncounterManager.instance.number;
        EncounterManager.instance.txtContext.text = EncounterManager.instance.encounter[n].Content;
        slot.GetComponent<CloneSlot>().isShowText = false;
        encounterManager.EnemyRunBtn(n);
    }

}
