using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjectsAsset/Enemy")]
public class EnemyProfile : ScriptableObject
{
    [SerializeField] public bool isStructure;//�Ƿ��ǽ�����

    [SerializeField] public int speed;//�ƶ��ٶ�

    [SerializeField] public float maxHealth;//�������ֵ

    [SerializeField] public float range;//������Χ

    [SerializeField] public float damage;//�˺�

    [SerializeField] public GameObject bulletPrefab;//�ӵ�Ԥ����
}
