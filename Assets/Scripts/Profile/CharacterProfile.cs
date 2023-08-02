using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjectsAsset/Character")]
public class CharacterProfile :ScriptableObject 
{
    [SerializeField] public int speed;//移动速度

    [SerializeField] public float maxHealth;//最大生命值

    [SerializeField] public float range;//攻击范围

    [SerializeField] public string targetTag;//攻击目标标签

    [SerializeField] public WeaponColor weaponColor;//标识变量：武器颜色

    [Range(1,3)]
    [SerializeField] public int nodeLevel;//标识变量：节点等级

    [SerializeField] public GameObject bulletPrefab;//子弹预制体
}

public enum WeaponColor
{
    None,
    Blue,
    Yellow,
    Red,
    Green,
    Purple,
    Orange,
}
