using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected float max_health;//�������ֵ

    protected float current_health;//��ǰ����ֵ

    protected virtual void onEnable()
    {
        current_health = max_health;
    }


    //����
    public virtual void TakeDamage(float damage)
    {
        current_health -= damage;

        if (current_health <= 0f)
            Die();
    }

    //����
    public virtual void Die()
    {
        //TO-DO ���������������Ч
    }

    //���
    public virtual void Shoot()
    {

    }

    //�ƶ�
    public virtual void Move()
    {

    }

    //װ������
    public virtual void Equip(Weapon weapon)
    {

    }

    //��׼
    public virtual void Target()
    {

    }
}
