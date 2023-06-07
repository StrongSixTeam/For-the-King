using System.Collections.Generic;
using UnityEngine;



public enum EffectType
{
    EnemyHit,     //�÷��̾ ������ �� ���ݹ��� �� ��ü �ʿ� ����� ����Ʈ
                

    PlayerHit     //���� ������ �� ���ݹ��� �÷��̾� ��ü �ʿ� ����� ����Ʈ
                   
                  

}



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



    public ParticleSystem plHitEffectprefab;
    public ParticleSystem enHitEffectprefab;

    // public ParticleSystem plFleshEffectprefab;
    // public ParticleSystem plSpaceEffectprefab;

    // public ParticleSystem enFleshEffectprefab;
    // public ParticleSystem enSpaceEffectprefab;

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

            //isAlive�� ����Ʈ�� �������� �� �������� �˻�
            //false�� ����Ʈ ����
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
