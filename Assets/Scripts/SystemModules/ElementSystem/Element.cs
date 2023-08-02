using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Element : MonoBehaviour
{
    public UnityEvent changed_type = new UnityEvent();

    public enum Type
    {
        None, Fire, Water, Soil, Toxin, Thunder, Dark
    }

    private Type type = Type.None;

    public new Type GetType()
    {
        return type;
    }

    public void SetType(Type type)
    {
        this.type = type;
        changed_type.Invoke();
    }

    public int GetTypeEnum()
    {
        return ElementToIndex(type);
    }

    /// <summary>
    /// 根据元素类型返回元素编号
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int ElementToIndex(Type type)
    {
        if (type == Type.Fire)
            return 1;
        if (type == Type.Water)
            return 2;
        if (type == Type.Soil)
            return 3;
        if (type == Type.Toxin)
            return 4;
        if (type == Type.Thunder)
            return 5;
        if (type == Type.Dark)
            return 6;

        return 0;
    }
}
