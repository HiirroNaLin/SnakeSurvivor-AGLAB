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

    [SerializeField] private EnemyProfile enemyProfile;//初始化配置文件

    //---------------------------------------------------------------------//

    [SerializeField] private EnemyController enemyController;//移动控制器

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
    /// 持续伤害方法
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
    /// 死亡方法。将该游戏对象推入对象池，调用敌人生成器的登记敌人死亡方法
    /// </summary>
    public override void Die()
    {
        health = 0f;
        //TO-DO 对象池生成死亡特效
        ObjectPool.Instance.PushObject(this.gameObject);
        EnemySpawner.Instance.OnEnemyDied();
    }

    /// <summary>
    /// 恢复生命值方法
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
    /// 持续恢复生命值方法
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
    /// 受伤方法
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
    /// 元素反应伤害计算方法
    /// </summary>
    /// <param name="incomingElement"></param>
    /// <param name="damage"></param>
    /// <param name="secondaryDamage"></param>
    public void ElementalDamage(Element.Type incomingElement, int damage, bool secondaryDamage)
    {
        damage = Mathf.RoundToInt(damage * Random.Range(0.9f, 1.1f));

        //获取两元素得到的元素反应
        ElementalReactions.Reaction reaction = ElementalReactions.GetReaction(element.GetType(), incomingElement);

        //元素反应不为空且不允许二次伤害
        if (reaction != null && !secondaryDamage)
        {
            //对伤害值进行元素倍率修正计算
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

        //若元素反应为空且此对象的元素类型为None且不允许二次伤害，则将子弹的元素类型赋值给当前对象的元素类型
        if (reaction == null && baseType == Element.Type.None && !secondaryDamage)
        {
            element.SetType(incomingElement);
        }

        //修正计算后的伤害值传入受伤方法中
        TakeDamage(damage);
    }

}
