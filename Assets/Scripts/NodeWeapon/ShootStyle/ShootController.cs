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
    /// 子弹的投射方式方法，待实现剩下的投射方式。通过判断ShootType来采用不同的投射方法。
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
