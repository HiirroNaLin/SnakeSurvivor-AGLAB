using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Character,IHealth
{
    [Header("--------Basic--------")]
    //属性TO-DO，将它们设置成访问器，方便后续修改值
    [SerializeField]private int speed;//移速

    [SerializeField] private float health;//生命值

    [SerializeField] private float maxHealth;//最大生命值

    [SerializeField] public int index;//节点下标

    [SerializeField] public int level;//节点等级

    [SerializeField] public int weaponColor;//武器颜色

    [Header("--------Health Bar--------")]
    [SerializeField] StatsBar HealthBar;

    [SerializeField] bool showHealthBar = true;

    [Header("--------Enemy Target--------")]
    [SerializeField] private string enemyTag;

    [SerializeField] protected float range;

    [SerializeField] protected Transform target;

    [SerializeField]private Weapon weapon;

    [SerializeField] private CharacterProfile characterProfile;//初始化属性

    //---------------------------------------------------------------------//

    private Transform tr;

    public GameObject Prior;

    public GameObject Next;

    public Rigidbody2D rb;

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

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    //---------------------------------------------------------------------//

    #region Unity生命周期函数
    void OnEnable()
    {
        //初始化属性
        speed = characterProfile.speed;
        maxHealth = characterProfile.maxHealth;
        range = characterProfile.range;
        enemyTag = characterProfile.targetTag;
        weaponColor =(int)characterProfile.weaponColor;
        level = characterProfile.nodeLevel;

        health = MaxHealth;

        if (showHealthBar)
        {
            ShowHealthBar();
        }
        else
        {
            HideHealthBar();
        }

        Equip(GetComponentInChildren<Weapon>());
        
    }

    private void FixedUpdate()
    {     
        if (Prior != null)
        {
            Vector3 moveDirection = Prior.transform.position - rb.transform.position;
            rb.transform.position += moveDirection * Speed * Time.deltaTime;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    private void Start()
    {
        
        InvokeRepeating("Target", 0, 1f);
    }

    #endregion

    #region HUD血条
    public void ShowHealthBar()
    {
        HealthBar.gameObject.SetActive(true);
        HealthBar.Initialize(health, MaxHealth);
    }

    public void HideHealthBar()
    {
        HealthBar.gameObject.SetActive(false);
    }
    #endregion

    /// <summary>
    /// 调用武器的Shoot方法。
    /// </summary>
    public override void Shoot()
    {
        weapon.Shoot();
    }

    /// <summary>
    /// 受伤方法。生命值health减去伤害值damage。若生命值health小于等于0，调用死亡方法Die( )。
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
    /// 死亡方法。让生命值health赋值为0，生成死亡特效和死亡音效。
    /// </summary>
    public override void Die()
    {
        health = 0f;
        //TO-DO 对象池生成死亡特效
    }

    /// <summary>
    /// 恢复生命值方法。
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
    /// 持续恢复生命值方法。
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator HealThroughTime(float percent, WaitForSeconds waitTime)
    {
        while (health < MaxHealth)
        {
            yield return waitTime;

            Heal(percent* MaxHealth);
        }
    }

    /// <summary>
    /// 持续受伤协程。
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public IEnumerator DamageOverTime(float percent, WaitForSeconds waitTime)
    {
        while (health >0f)
        {
            yield return waitTime;

            TakeDamage(percent * MaxHealth);
        }
    }

    /// <summary>
    /// 装备武器方法。获取weapon脚本
    /// </summary>
    /// <param name="weapon"></param>
    public override void Equip(Weapon weapon)
    {
        this.weapon = weapon;
    }

    /// <summary>
    /// 获取攻击目标方法。检测攻击范围内是否有敌人，将最近的敌人作为攻击目标，传给weapon。
    /// </summary>
    public override void Target()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag(enemyTag);
        float min_distance = Mathf.Infinity;
        Transform nearest_enemy = null;
        foreach (var enemy in enemis)
        {
            float distance = Vector3.Distance(enemy.transform.position, tr.position);
            if (distance < min_distance)
            {
                min_distance = distance;
                nearest_enemy = enemy.transform;
            }
        }
        if (min_distance < Range)
        {
            target = nearest_enemy;
        }
        else
        {
            target = null;
        }

        weapon.target = target;
    }
}
