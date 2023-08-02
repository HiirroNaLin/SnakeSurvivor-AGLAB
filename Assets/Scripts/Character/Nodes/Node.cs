using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Character,IHealth
{
    [Header("--------Basic--------")]
    //����TO-DO�����������óɷ���������������޸�ֵ
    [SerializeField]private int speed;//����

    [SerializeField] private float health;//����ֵ

    [SerializeField] private float maxHealth;//�������ֵ

    [SerializeField] public int index;//�ڵ��±�

    [SerializeField] public int level;//�ڵ�ȼ�

    [SerializeField] public int weaponColor;//������ɫ

    [Header("--------Health Bar--------")]
    [SerializeField] StatsBar HealthBar;

    [SerializeField] bool showHealthBar = true;

    [Header("--------Enemy Target--------")]
    [SerializeField] private string enemyTag;

    [SerializeField] protected float range;

    [SerializeField] protected Transform target;

    [SerializeField]private Weapon weapon;

    [SerializeField] private CharacterProfile characterProfile;//��ʼ������

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

    #region Unity�������ں���
    void OnEnable()
    {
        //��ʼ������
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

    #region HUDѪ��
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
    /// ����������Shoot������
    /// </summary>
    public override void Shoot()
    {
        weapon.Shoot();
    }

    /// <summary>
    /// ���˷���������ֵhealth��ȥ�˺�ֵdamage��������ֵhealthС�ڵ���0��������������Die( )��
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
    /// ����������������ֵhealth��ֵΪ0������������Ч��������Ч��
    /// </summary>
    public override void Die()
    {
        health = 0f;
        //TO-DO ���������������Ч
    }

    /// <summary>
    /// �ָ�����ֵ������
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
    /// �����ָ�����ֵ������
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
    /// ��������Э�̡�
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
    /// װ��������������ȡweapon�ű�
    /// </summary>
    /// <param name="weapon"></param>
    public override void Equip(Weapon weapon)
    {
        this.weapon = weapon;
    }

    /// <summary>
    /// ��ȡ����Ŀ�귽������⹥����Χ���Ƿ��е��ˣ�������ĵ�����Ϊ����Ŀ�꣬����weapon��
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
