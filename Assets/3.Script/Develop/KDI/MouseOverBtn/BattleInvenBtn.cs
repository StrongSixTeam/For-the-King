using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleInvenBtn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject[] icons;
    [SerializeField] private GameObject battleRunBtn;
    [SerializeField] private GameObject battleFightBtn;
    private BattleOrderManager battleOrderManager;
    [SerializeField] private Text infoText;

    public GameObject Accuracy;
    public GameObject Damage;

    private void Awake()
    {
        battleOrderManager = FindObjectOfType<BattleOrderManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = "소지품 살펴보기";
        if (battleFightBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus += battleFightBtn.GetComponent<RightClick>().usedFocus;
            battleFightBtn.GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
        }
        if (battleRunBtn.GetComponent<RightClick>().usedFocus > 0)
        {
            GameManager.instance.Players[battleOrderManager.Order[battleOrderManager.turn].GetComponent<PlayerStat>().order].GetComponent<PlayerStat>().nowFocus += battleRunBtn.GetComponent<RightClick>().usedFocus;
            battleRunBtn.GetComponent<RightClick>().usedFocus = 0;
            SlotController.instance.fixCount = 0;
        }
        slot.SetActive(false);
        Accuracy.SetActive(false);
        Damage.SetActive(false);
        icons[0].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[2].transform.localScale = new Vector3(1f, 1f, 1f);
        icons[1].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

}
