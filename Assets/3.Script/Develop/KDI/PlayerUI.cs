using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
   public int order;

    [SerializeField] private PlayerStat playerStat;

    [SerializeField] private Text name;
    [SerializeField] private Text strength;
    [SerializeField] private Text intelligence;
    [SerializeField] private Text awareness;
    [SerializeField] private Text speed;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text hpText;
    [SerializeField] private Text expText;
    public Image portrait;
    [SerializeField] private GameObject focus;
    [SerializeField] private Sprite[] focusSprites;

    private bool playerStatSet = false;
    private void Update()
    {
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Length > order))
        {
            if (!playerStatSet) //�ѹ��� �����ص� �Ǵ� �κ�
            {
                playerStat = GameManager.instance.Players[order].GetComponent<PlayerStat>();
                playerStatSet = true;
                portrait.sprite = playerStat.portrait; 
                name.text = playerStat.name;
                for (int i = 0; i < playerStat.maxFocus; i++) 
                {
                    focus.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else //��� ������Ʈ �Ǿ�� �ϴ� �κ�
            {
                strength.text = playerStat.strength.ToString();
                intelligence.text = playerStat.intelligence.ToString();
                awareness.text = playerStat.awareness.ToString();
                speed.text = playerStat.speed.ToString();

                hpSlider.maxValue = playerStat.maxHp;
                hpSlider.value = playerStat.nowHp;

                expSlider.maxValue = playerStat.maxExp;
                expSlider.value = playerStat.nowExp;

                hpText.text = playerStat.nowHp + " / " + playerStat.maxHp;
                expText.text = playerStat.nowExp + " / " + playerStat.maxExp;
                SetFocus();
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
            else //�� �̹����� ����
            {
                focus.transform.GetChild(i).GetComponent<Image>().sprite = focusSprites[1];
            }
        }
    }
}
