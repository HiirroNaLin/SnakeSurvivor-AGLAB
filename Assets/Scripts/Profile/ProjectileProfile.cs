using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjectsAsset/Projectile")]
public class ProjectileProfile :ScriptableObject
{
    [SerializeField] public int bulletRate;//�ӵ�����
    [SerializeField] public int countDown;//��ȴʱ��
    [SerializeField] public int damage;//�˺�
    [SerializeField] public float speed;//����
    [SerializeField] public Element.Type elementType;//�ӵ�Ԫ������
}
