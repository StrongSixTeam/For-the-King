using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
   public int order;

    [SerializeField] private PlayerStat playerStat;

    [Header("Text")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text strengthText;
    [SerializeField] private Text intelligenceText;
    [SerializeField] private Text awarenessText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text hpText;
    [SerializeField] private Text hpText2;
    [SerializeField] private Text expText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text atkText;
    [SerializeField] private Text defText;

    [Header("Slider")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider expSlider;

    public Image portrait;
    [SerializeField] private GameObject focus;
    [SerializeField] private Sprite[] focusSprites;
    [SerializeField] private GameObject sanctum;


    private bool playerStatSet = false;
    private void Update()
    {
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Length > order))
        {
            if (!playerStatSet) //한번만 실행해도 되는 부분
            {
                playerStat = GameManager.instance.Players[order].GetComponent<PlayerStat>();
                playerStatSet = true;
                portrait.sprite = playerStat.portrait; 
                nameText.text = playerStat.name;
                for (int i = 0; i < playerStat.maxFocus; i++) 
                {
                    focus.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else //계속 업데이트 되어야 하는 부분
            {
                strengthText.text = playerStat.strength.ToString();
                intelligenceText.text = playerStat.intelligence.ToString();
                awarenessText.text = playerStat.awareness.ToString();
                speedText.text = playerStat.speed.ToString();
                levelText.text = playerStat.Lv.ToString();
                atkText.text = playerStat.atk.ToString();
                defText.text = playerStat.def.ToString();

                hpText.text = playerStat.nowHp + " / " + playerStat.maxHp;
                hpText2.text = playerStat.nowHp.ToString();
                expText.text = playerStat.nowExp + " / " + playerStat.maxExp;
                coinText.text = playerStat.coins.ToString();

                hpSlider.maxValue = playerStat.maxHp;
                hpSlider.value = playerStat.nowHp;

                expSlider.maxValue = playerStat.maxExp;
                expSlider.value = playerStat.nowExp;
                
                SetFocus();
                SetSanctum();
            }
        }
    }

    private void SetFocus()
    {
        for (int i = 0; i < playerStat.maxFocus; i++)
        {
            focus.transform.GetChild(i).gameObject.SetActive(true);
            if (i < playerStat.nowFocus)
            {
                focus.transform.GetChild(i).GetComponent<Image>().sprite = focusSprites[0];
            }
            else //빈 이미지로 변경
            {
                focus.transform.GetChild(i).GetComponent<Image>().sprite = focusSprites[1];
            }
        }
    }

    private void SetSanctum()
    {
        if (playerStat.whichSanctum == PlayerStat.Sanctum.none)
        {
            for (int i =0; i < sanctum.transform.childCount; i++)
            {
                sanctum.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (playerStat.whichSanctum == PlayerStat.Sanctum.life)
        {
            for (int i = 0; i < sanctum.transform.childCount; i++)
            {
                if (i != 0)
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else if (playerStat.whichSanctum == PlayerStat.Sanctum.focus)
        {
            for (int i = 0; i < sanctum.transform.childCount; i++)
            {
                if (i != 1)
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else if (playerStat.whichSanctum == PlayerStat.Sanctum.wisdom)
        {
            for (int i = 0; i < sanctum.transform.childCount; i++)
            {
                if (i != 2)
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    sanctum.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
