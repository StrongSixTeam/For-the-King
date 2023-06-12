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

    [SerializeField] private GameObject dieImg;


    private bool playerStatSet = false;
    private void Update()
    {
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Count > order))
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
                //공격력 계산
                if (playerStat.weapon != null)
                {
                    if (playerStat.weapon.maxAdDmg != 0)
                    {
                        playerStat.atk = playerStat.weapon.maxAdDmg;
                        atkText.text = playerStat.atk.ToString();
                    }
                    else
                    {
                        playerStat.atk = playerStat.weapon.maxApDmg;
                        atkText.text = playerStat.weapon.maxApDmg.ToString();
                    }
                }
                else
                {
                    playerStat.atk = playerStat.originalAtk;
                    atkText.text = playerStat.originalAtk.ToString();
                }
                int extraHp = 0;
                //방어력 계산
                float extraDef = 0;
                if (playerStat.armorHelmet != null)
                {
                    extraDef += playerStat.armorHelmet.def;
                }
                if (playerStat.armor != null)
                {
                    extraDef += playerStat.armor.def;
                }
                if (playerStat.armorBoots != null)
                {
                    extraDef += playerStat.armorBoots.def;
                }
                if (playerStat.armorNecklace != null)
                {
                    extraDef += playerStat.armorNecklace.def;
                    extraHp = 3;
                }
                else
                {
                    extraHp = 0;
                }
                playerStat.def = playerStat.originalDef + extraDef;
                defText.text = ((int)playerStat.def).ToString();

                playerStat.maxHp = playerStat.originalMaxHp + extraHp;
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

                if (playerStat.nowHp <= 0 && GameManager.instance.currentLife <= 0)
                {
                    dieImg.SetActive(true);
                }
            }
        }
    }

    private void SetFocus()
    {
        if (playerStat.nowFocus > playerStat.maxFocus)
        {
            playerStat.nowFocus = playerStat.maxFocus;
        }
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
            for (int i = 0; i < sanctum.transform.childCount; i++)
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
