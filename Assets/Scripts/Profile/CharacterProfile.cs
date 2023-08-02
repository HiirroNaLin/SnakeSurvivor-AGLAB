using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjectsAsset/Character")]
public class CharacterProfile :ScriptableObject 
{
    [SerializeField] public int speed;//�ƶ��ٶ�

    [SerializeField] public float maxHealth;//�������ֵ

    [SerializeField] public float range;//������Χ

    [SerializeField] public string targetTag;//����Ŀ���ǩ

    [SerializeField] public WeaponColor weaponColor;//��ʶ������������ɫ

    [Range(1,3)]
    [SerializeField] public int nodeLevel;//��ʶ�������ڵ�ȼ�

    [SerializeField] public GameObject bulletPrefab;//�ӵ�Ԥ����
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
