using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    /// <summary>
    /// 受伤方法
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(float damage);
    /// <summary>
    /// 死亡方法
    /// </summary>
    void Die();
    /// <summary>
    /// 恢复生命值方法
    /// </summary>
    /// <param name="value"></param>
    void Heal(float value);
    /// <summary>
    /// 持续恢复生命值协程
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator HealThroughTime(float percent, WaitForSeconds waitTime);
    /// <summary>
    /// 持续受伤协程
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator DamageOverTime(float percent, WaitForSeconds waitTime);

}
