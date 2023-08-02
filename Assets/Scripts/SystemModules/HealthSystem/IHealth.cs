using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    /// <summary>
    /// ���˷���
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(float damage);
    /// <summary>
    /// ��������
    /// </summary>
    void Die();
    /// <summary>
    /// �ָ�����ֵ����
    /// </summary>
    /// <param name="value"></param>
    void Heal(float value);
    /// <summary>
    /// �����ָ�����ֵЭ��
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator HealThroughTime(float percent, WaitForSeconds waitTime);
    /// <summary>
    /// ��������Э��
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator DamageOverTime(float percent, WaitForSeconds waitTime);

}
