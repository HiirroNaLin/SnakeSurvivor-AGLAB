using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private GameObject bulletPrefab;
    void Start()
    {
        bulletPrefab = this.gameObject;
    }

    /// <summary>
    /// �ӵ���Ͷ�䷽ʽ��������ʵ��ʣ�µ�Ͷ�䷽ʽ��ͨ���ж�ShootType�����ò�ͬ��Ͷ�䷽����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="bullet"></param>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public virtual void ShootStyle(ShootType type, GameObject bullet, Vector2 direction, float speed)
    {
        switch (type)
        {
            case ShootType.First:
                bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
                break;
        }
    }
}

public enum ShootType
{
    First,
    Second,
    Third,
    Fourth,
    Fifth
}
