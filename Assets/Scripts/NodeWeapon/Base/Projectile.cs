using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ShootType shootType;

    [SerializeField] protected float speed;

    [SerializeField] protected float damage;

    [SerializeField] private ProjectileProfile projectileProfile;

    private Rigidbody2D rb;
    private ShootController shootController;

    void OnEnable()
    {
        speed = projectileProfile.speed;
        damage = projectileProfile.damage;
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootController = GetComponent<ShootController>();
        rb.gravityScale = 0;
    }

    /// <summary>
    /// 调用shootController的ShootStyle方法，传入射击方式shootType，当前游戏对象gameObject，射击方向dir以及射击速度speed。
    /// </summary>
    /// <param name="dir"></param>
    public virtual void Cast(Vector2 dir)
    {
        shootController.ShootStyle(shootType,this.gameObject,dir,speed);
        //rb.velocity = dir * speed;
    }

    /// <summary>
    /// Unity自带方法。碰撞到带有Enemy脚本的碰撞体后，返回Enemy脚本，
    /// 调用该脚本中的ElementalDamage方法，对敌人进行元素伤害计算，之后将该子弹游戏对象推入对象池。
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ElementalDamage(projectileProfile.elementType,(int)damage,false);
            
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

}
