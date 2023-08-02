using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjectsAsset/Enemy")]
public class EnemyProfile : ScriptableObject
{
    [SerializeField] public bool isStructure;//是否是建筑物

    [SerializeField] public int speed;//移动速度

    [SerializeField] public float maxHealth;//最大生命值

    [SerializeField] public float range;//攻击范围

    [SerializeField] public float damage;//伤害

    [SerializeField] public GameObject bulletPrefab;//子弹预制体
}
