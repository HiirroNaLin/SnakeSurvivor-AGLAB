using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjectsAsset/Projectile")]
public class ProjectileProfile :ScriptableObject
{
    [SerializeField] public int bulletRate;//子弹速率
    [SerializeField] public int countDown;//冷却时间
    [SerializeField] public int damage;//伤害
    [SerializeField] public float speed;//射速
    [SerializeField] public Element.Type elementType;//子弹元素类型
}
