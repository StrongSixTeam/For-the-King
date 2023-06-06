using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public BattleLoader battleLoader;
    

    private static EffectManager m_Instance;
    public static EffectManager Instance  //����Ʈ �Ŵ��� �ν��Ͻ��� ���Ϲ��� ������Ƽ
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<EffectManager>();
            return m_Instance;
        }
    }
    
    public enum EffectType
    {
        PlayerAttack, //�÷��̾ ������ �� ���ݹ��� �� ��ü �ʿ� ����� ����Ʈ
        PlayerFlesh,  //�÷��̾ ������ �� �����ϴ� �ش� �÷��̾� �ڸ��� ���ܳ��� ��
        PlayerSpace,  //����/�ǰ� �÷��̾� �ٴڿ� ǥ�õǴ� ������

        EnemyAttack,  //���� ������ �� ���ݹ��� �÷��̾� ��ü �ʿ� ����� ����Ʈ
        EnemyFlesh,   //���� ������ �� �����ϴ� �ش� �� �ڸ��� ���ܳ��� �� 
        EnemySpace    //����/�ǰ� �� �ٴڿ� ǥ�õǴ� ������

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
