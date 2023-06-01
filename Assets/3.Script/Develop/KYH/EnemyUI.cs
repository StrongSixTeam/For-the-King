using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public EnemyStat enemyStat;

    [Header("Text")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text HPText;

    [Header("Slider")]
    [SerializeField] private Slider hpSlider;

    private BattleLoader battleLoader;

    public Image portrait;

    private bool isDefaultSet = false;

    private void Awake()
    {
        battleLoader = FindObjectOfType<BattleLoader>();
    }
    private void Update()
    {
        if(enemyStat != null)
        {
            if (!isDefaultSet)
            {
                isDefaultSet = true;

                nameText.text = enemyStat.name;
                levelText.text = "" + enemyStat.Lv;
                hpSlider.maxValue = enemyStat.maxHp;
                portrait.sprite = enemyStat.portrait;
            }
            else
            {
                HPText.text = "" + enemyStat.nowHp;
                hpSlider.value = enemyStat.nowHp;
            }
        }
    }
}
