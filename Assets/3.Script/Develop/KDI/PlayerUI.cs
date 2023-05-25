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

    private void Update()
    {
        if (GameManager.instance.isSettingDone && (GameManager.instance.Players.Length > order))
        {
            playerStat = GameManager.instance.Players[order].GetComponent<PlayerStat>();
            name.text = playerStat.name;
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
        }
        
    }
}
