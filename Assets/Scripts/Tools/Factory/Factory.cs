using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
    /// <summary>
    /// ͨ���ڵ�ȼ���1��2��3����������ɫ��1����2�ƣ�3�죬4�̣�5�ϣ�6�ȣ�����ȡ�ڵ�Ԥ���塣
    /// </summary>
    /// <param name="NodeID"></param>
    /// <param name="WeaponID"></param>
    /// <returns>
    /// �������Ľڵ���Ϸ����
    /// </returns>
    public GameObject GetElement(int NodeID,int WeaponID)
    {
        return ObjectPool.Instance.GetObject(Resources.Load<GameObject>("Nodes/"+NodeID.ToString()+"_"+WeaponID.ToString() ));

    }

    /// <summary>
    /// ͨ����ɫ��1����2�ƣ�3�죩����ȡʳ��Ԥ����
    /// </summary>
    /// <param name="ColorID"></param>
    /// <returns></returns>
    public GameObject GetFood(int ColorID)
    {
        return ObjectPool.Instance.GetObject(Resources.Load<GameObject>("Foods/" +"Food_"+ ColorID.ToString()));
    }
}
