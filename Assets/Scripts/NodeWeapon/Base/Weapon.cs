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
    /// ����������Ӷ���ػ�ȡһ���ӵ�Ԥ���壬�������ӵ���CastͶ�䷽ʽ����
    /// </summary>
    public virtual void Shoot()
    {
        Vector3 dir = target.position - this.tr.position;
        GameObject bullet = ObjectPool.Instance.GetObject(bullet_prefab);
        bullet.transform.position = tr.position;
        bullet.GetComponent<Projectile>().Cast(dir);
    }


    /// <summary>
    /// ����Ŀ�귽������������Ϸ����һֱָ����ˡ�
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
