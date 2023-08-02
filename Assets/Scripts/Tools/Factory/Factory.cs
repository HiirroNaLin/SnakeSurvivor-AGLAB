using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
    /// <summary>
    /// 通过节点等级（1，2，3）和武器颜色（1蓝，2黄，3红，4绿，5紫，6橙）来获取节点预制体。
    /// </summary>
    /// <param name="NodeID"></param>
    /// <param name="WeaponID"></param>
    /// <returns>
    /// 带武器的节点游戏对象
    /// </returns>
    public GameObject GetElement(int NodeID,int WeaponID)
    {
        return ObjectPool.Instance.GetObject(Resources.Load<GameObject>("Nodes/"+NodeID.ToString()+"_"+WeaponID.ToString() ));

    }

    /// <summary>
    /// 通过颜色（1蓝，2黄，3红）来获取食物预制体
    /// </summary>
    /// <param name="ColorID"></param>
    /// <returns></returns>
    public GameObject GetFood(int ColorID)
    {
        return ObjectPool.Instance.GetObject(Resources.Load<GameObject>("Foods/" +"Food_"+ ColorID.ToString()));
    }
}
