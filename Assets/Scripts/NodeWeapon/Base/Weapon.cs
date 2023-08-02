using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform target;

    [SerializeField] public GameObject bullet_prefab;

    [SerializeField] public float bullet_rate;

    [SerializeField] public float countDown;

    [SerializeField] public ProjectileProfile projectileProfile;

    protected Vector3 target_dir;

    private Transform tr;

    void OnEnable()
    {
        bullet_rate = projectileProfile.bulletRate;
        countDown = projectileProfile.countDown;
    }


    private void Start()
    {
        this.tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        LockTarget();
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            Shoot();
            countDown = 1 / bullet_rate;
        }
    }

    /// <summary>
    /// 射击方法。从对象池获取一个子弹预制体，并调用子弹的Cast投射方式方法
    /// </summary>
    public virtual void Shoot()
    {
        Vector3 dir = target.position - this.tr.position;
        GameObject bullet = ObjectPool.Instance.GetObject(bullet_prefab);
        bullet.transform.position = tr.position;
        bullet.GetComponent<Projectile>().Cast(dir);
    }


    /// <summary>
    /// 锁定目标方法。让武器游戏对象一直指向敌人。
    /// </summary>
    public virtual void LockTarget()
    {
        Vector3 dir = target.position - this.tr.position;

        Vector3 relative = this.tr.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        this.tr.Rotate(0, 0, -angle);
        Debug.DrawRay(this.tr.position, dir, Color.green);
    }
}
