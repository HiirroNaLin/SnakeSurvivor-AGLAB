using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Element))]
public class Enemy : Character,IHealth
{

    [Header("--------Basic--------")]

    [SerializeField] private float range;

    [SerializeField] private int speed;

    [SerializeField] private float health;

    [SerializeField] private float maxHealth;

    [SerializeField] float damage;

    [Header("--------Health Bar--------")]

    [SerializeField] StatsBar HealthBar;

    [SerializeField] bool showHealthBar = true;

    [SerializeField] private EnemyProfile enemyProfile;//��ʼ�������ļ�

    //---------------------------------------------------------------------//

    [SerializeField] private EnemyController enemyController;//�ƶ�������

    public Element.Type baseType;

    private Element element;

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    //---------------------------------------------------------------------//

    void Start()
    {
        element = GetComponent<Element>();
    }


    void OnEnable()
    {
        speed = enemyProfile.speed;
        damage = enemyProfile.damage;
        maxHealth = enemyProfile.maxHealth;
        range = enemyProfile.range;
        health = MaxHealth;

        if (showHealthBar)
        {
            ShowHealthBar();
        }
        else
        {
            HideHealthBar();
        }
    }

    public void ShowHealthBar()
    {
        HealthBar.gameObject.SetActive(true);
        HealthBar.Initialize(health, MaxHealth);
    }

    public void HideHealthBar()
    {
        HealthBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// �����˺�����
    /// </summary>
    /// <param name="value"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator DamageOverTime(float value, WaitForSeconds waitTime)
    {
        while (health > 0f)
        {
            yield return waitTime;

            TakeDamage(value * MaxHealth);
        }
    }

    /// <summary>
    /// ����������������Ϸ�����������أ����õ����������ĵǼǵ�����������
    /// </summary>
    public override void Die()
    {
        health = 0f;
        //TO-DO ���������������Ч
        ObjectPool.Instance.PushObject(this.gameObject);
        EnemySpawner.Instance.OnEnemyDied();
    }

    /// <summary>
    /// �ָ�����ֵ����
    /// </summary>
    /// <param name="value"></param>
    public void Heal(float value)
    {
        if (health == MaxHealth) return;

        health = Mathf.Clamp(health + value, 0f, MaxHealth);

        if (showHealthBar)
        {
            HealthBar.UpdateStats(health, MaxHealth);
        }
    }

    /// <summary>
    /// �����ָ�����ֵ����
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator HealThroughTime(float percent, WaitForSeconds waitTime)
    {
        while (health < MaxHealth)
        {
            yield return waitTime;

            Heal(percent * MaxHealth);
        }
    }

    /// <summary>
    /// ���˷���
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        
        health -= damage;

        if (showHealthBar)
        {
            HealthBar.UpdateStats(health, MaxHealth);
        }

        if (health <= 0f)
            Die();
    }

    /// <summary>
    /// Ԫ�ط�Ӧ�˺����㷽��
    /// </summary>
    /// <param name="incomingElement"></param>
    /// <param name="damage"></param>
    /// <param name="secondaryDamage"></param>
    public void ElementalDamage(Element.Type incomingElement, int damage, bool secondaryDamage)
    {
        damage = Mathf.RoundToInt(damage * Random.Range(0.9f, 1.1f));

        //��ȡ��Ԫ�صõ���Ԫ�ط�Ӧ
        ElementalReactions.Reaction reaction = ElementalReactions.GetReaction(element.GetType(), incomingElement);

        //Ԫ�ط�Ӧ��Ϊ���Ҳ���������˺�
        if (reaction != null && !secondaryDamage)
        {
            //���˺�ֵ����Ԫ�ر�����������
            damage = Mathf.RoundToInt(damage * reaction.damageMult);

            if (reaction.reactionName.Contains("Rockmelt"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Diffuse"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Quake"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Posionpool"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Paralysis"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Erosion"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Retardation"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Vapor"))
                print(reaction.reactionName);

            if (reaction.reactionName.Contains("Deflagrate"))
                print(reaction.reactionName);

            if (baseType == Element.Type.None)
                element.SetType(Element.Type.None);
        }

        //��Ԫ�ط�ӦΪ���Ҵ˶����Ԫ������ΪNone�Ҳ���������˺������ӵ���Ԫ�����͸�ֵ����ǰ�����Ԫ������
        if (reaction == null && baseType == Element.Type.None && !secondaryDamage)
        {
            element.SetType(incomingElement);
        }

        //�����������˺�ֵ�������˷�����
        TakeDamage(damage);
    }

}
