using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    public static Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    private GameObject pool;


    /// <summary>
    /// 从对象池中获取prefab的游戏对象
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            if (pool == null)
                pool = new GameObject("ObjectPool");
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    /// <summary>
    /// 从对象池中获取prefab游戏对象，parentpos为该游戏对象生成时的父级游戏对象坐标
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parentpos"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab, Transform parentpos)
    {
        GameObject _object;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab, parentpos);
            PushObject(_object);
            if (pool == null)
                pool = new GameObject("ObjectPool");
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    /// <summary>
    /// 从对象池中获取prefab游戏对象，position为生成坐标，rotation为生成四元数
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject _object;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab,position,rotation);
            PushObject(_object);
            if (pool == null)
                pool = new GameObject("ObjectPool");
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    /// <summary>
    /// 将prefab压入对象池中并禁用，等待被使用
    /// </summary>
    /// <param name="prefab"></param>
    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)",string.Empty);
        if (!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
