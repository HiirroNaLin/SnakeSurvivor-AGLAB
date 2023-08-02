using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected float max_health;//最大生命值

    protected float current_health;//当前生命值

    protected virtual void onEnable()
    {
        current_health = max_health;
    }


    //受伤
    public virtual void TakeDamage(float damage)
    {
        current_health -= damage;

        if (current_health <= 0f)
            Die();
    }

    //死亡
    public virtual void Die()
    {
        //TO-DO 对象池生成死亡特效
    }

    //射击
    public virtual void Shoot()
    {

    }

    //移动
    public virtual void Move()
    {

    }

    //装备武器
    public virtual void Equip(Weapon weapon)
    {

    }

    //瞄准
    public virtual void Target()
    {

    }
}
