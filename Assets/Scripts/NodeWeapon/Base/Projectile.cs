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
    /// ����shootController��ShootStyle���������������ʽshootType����ǰ��Ϸ����gameObject���������dir�Լ�����ٶ�speed��
    /// </summary>
    /// <param name="dir"></param>
    public virtual void Cast(Vector2 dir)
    {
        shootController.ShootStyle(shootType,this.gameObject,dir,speed);
        //rb.velocity = dir * speed;
    }

    /// <summary>
    /// Unity�Դ���������ײ������Enemy�ű�����ײ��󣬷���Enemy�ű���
    /// ���øýű��е�ElementalDamage�������Ե��˽���Ԫ���˺����㣬֮�󽫸��ӵ���Ϸ�����������ء�
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
