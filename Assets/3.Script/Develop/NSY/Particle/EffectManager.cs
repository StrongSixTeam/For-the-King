using System.Collections.Generic;
using UnityEngine;



public enum EffectType
{
    EnemyHit,     //플레이어가 공격할 시 공격받은 적 몸체 쪽에 생기는 이펙트
    PlayerSkill,  //플레이어가 공격할 시 나타나는 스킬 이펙트       

    PlayerHit,     //적이 공격할 시 공격받은 플레이어 몸체 쪽에 생기는 이펙트
    EnemySkill,     //적이 공격할 시 나타나는 스킬 이펙트               
                  
    Revival       //부활!!

}



public class EffectManager : MonoBehaviour
{
    public BattleLoader battleLoader;


    private static EffectManager m_Instance;
    public static EffectManager Instance  //이펙트 매니저 인스턴스를 리턴받을 프로퍼티
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<EffectManager>();
            return m_Instance;
        }
    }



    public ParticleSystem plHitEffectprefab;
    public ParticleSystem enHitEffectprefab;
    public ParticleSystem plSkillEffectprefab;
    public ParticleSystem enSkillEffectprefab;
    public ParticleSystem revivalEffectprefab;

    
    private List<ParticleSystem> effectList = new List<ParticleSystem>();



    public void PlayEffect(Vector3 pos, Transform parent = null, EffectType effectType = EffectType.EnemyHit)
    {
        GameObject targetPrefab = null;

        if (effectType == EffectType.PlayerHit)
        {
            targetPrefab = Resources.Load<GameObject>("Hit_01");
        }

        else if (effectType == EffectType.EnemyHit)
        {
            targetPrefab = Resources.Load<GameObject>("Hit_02");
        }

        else if (effectType==EffectType.PlayerSkill)
        {
            targetPrefab = Resources.Load<GameObject>("Electro slash");
        }
        
        else if (effectType==EffectType.EnemySkill)
        {
            targetPrefab = Resources.Load<GameObject>("Stone slash");  
        }

        else if (effectType==EffectType.Revival)
        {
            targetPrefab = Resources.Load<GameObject>("BirthBorder");
        }


        var obj = Instantiate(targetPrefab, pos, Quaternion.identity);

        ParticleSystem effect = obj.GetComponent<ParticleSystem>();
        if (parent != null)
        {

            effect.transform.SetParent(parent);

        }

        effect.Play();
        effectList.Add(effect);
    }

    private void Update()
    {
        for (int i = 0; effectList.Count > i;)
        {
            if (effectList[i] == null)
            {

                effectList.RemoveAt(i);
                continue;
            }

            //isAlive는 이펙트가 끝났는지 안 끝났는지 검사
            //false면 이펙트 끝남
            if (effectList[i].IsAlive(true) == false)
            {

                Destroy(effectList[i].gameObject);
                effectList.RemoveAt(i);
                continue;
            }
            i++;
        }
    }

}
