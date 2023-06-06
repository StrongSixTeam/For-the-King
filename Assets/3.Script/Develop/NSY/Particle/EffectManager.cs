using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public enum EffectType
    {
        PlayerAttack, //플레이어가 공격할 시 공격받은 적 몸체 쪽에 생기는 이펙트
        PlayerFlesh,  //플레이어가 공격할 시 공격하는 해당 플레이어 자리에 생겨나는 빛
        PlayerSpace,  //공격/피격 플레이어 바닥에 표시되는 육각형

        EnemyAttack,  //적이 공격할 시 공격받은 플레이어 몸체 쪽에 생기는 이펙트
        EnemyFlesh,   //적이 공격할 시 공격하는 해당 적 자리에 생겨나는 빛 
        EnemySpace    //공격/피격 적 바닥에 표시되는 육각형

    }

    public ParticleSystem plAttackEffectprefab;
    public ParticleSystem plFleshEffectprefab;
    public ParticleSystem plSpaceEffectprefab;
    public ParticleSystem enAttackEffectprefab;
    public ParticleSystem enFleshEffectprefab;
    public ParticleSystem enSpaceEffectprefab;

   public void PlayerEffect(Vector3 pos, Vector3 normal, Transform parent=null, EffectType effectType=EffectType.PlayerAttack)
    {
        var targetPrefab = plAttackEffectprefab;

        if (effectType == EffectType.PlayerFlesh)
        {
            targetPrefab = plFleshEffectprefab;
        }

        else if (effectType == EffectType.PlayerSpace)
        {
            targetPrefab = plSpaceEffectprefab;
        }


        var effect = Instantiate(targetPrefab, pos, Quaternion.LookRotation(normal));

        if (parent != null) effect.transform.SetParent(parent);
        effect.Play();

    }




    public void EnemyEffect(Vector3 enPos, Vector3 enNormal, Transform enParent=null, EffectType effectType =EffectType.EnemyAttack)
    {
        var targetEnPrefab = enAttackEffectprefab;

        if (effectType == EffectType.EnemyFlesh)
        {
            targetEnPrefab = enFleshEffectprefab;
        }

        else if (effectType == EffectType.EnemySpace)
        {
            targetEnPrefab = enSpaceEffectprefab;
        }

        var enEffect = Instantiate(targetEnPrefab, enPos, Quaternion.LookRotation(enNormal));

        if (enParent != null) enEffect.transform.SetParent(enParent);
        enEffect.Play();


    }

}
