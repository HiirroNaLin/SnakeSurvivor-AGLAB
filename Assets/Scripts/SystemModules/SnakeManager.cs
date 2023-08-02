using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : PersistentSingleton<SnakeManager>
{
    [SerializeField] int r;//间隔
    [SerializeField] int count;//生成数量
    [SerializeField] int initnum;

    [SerializeField] AddInTailEventChannel addintail;
    [SerializeField] EventChannel trimatch;
    [SerializeField] EventChannel combinecolor;
    //传值事件频道，传NodeID和WeaponID，生成对应Node和Weapon的节点
    public GameObject Head;
    public GameObject Last;

    private Factory factory;//节点、武器工厂

    //TO-DO 将蛇添加进列表里，便于管理

    [SerializeField] private List<GameObject> SnakeList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        factory = new Factory();
        Transform tr = GetComponent<Transform>();
        Head = ObjectPool.Instance.GetObject(Resources.Load<GameObject>("Head"), tr.position, tr.rotation);
        Init(Head, initnum, tr.position, r);
    }

    void Start()
    {
        
        count = initnum;
        
        Last = GetLast(Head);
    }

    private void OnEnable()
    {
        addintail.AddListener(AddInTailWithID);
        trimatch.AddListener(TriColorMatch);
        combinecolor.AddListener(CombineColors);
    }
    private void OnDisable()
    {
        addintail.RemoveListener(AddInTailWithID);
        trimatch.RemoveListener(TriColorMatch);
        combinecolor.RemoveListener(CombineColors);
    }

   

    private void Init(GameObject head, int count, Vector3 initpos, int r)
    {
        

        var t = head;
        for (int n = 1; n <= count; n++)
        {
            initpos.y += r;

            var q = factory.GetElement(1, 1);

            t.GetComponent<Node>().Next = q;
            q.GetComponent<Node>().Prior = t;
            q.GetComponent<Node>().index = n;
            t = q;
        }
        
    }

    public GameObject GetLast(GameObject head)
    {
        GameObject t = head;
        while (t.GetComponent<Node>().Next != null)
        {
            t = t.GetComponent<Node>().Next;
        }
        return t;
    }


    public void AddInTailWithID(int NodeID, int WeaponID)
    {

        var p = factory.GetElement(NodeID, WeaponID);

        Last.GetComponent<Node>().Next = p;
        p.GetComponent<Node>().Prior = Last;
        
        UpdateIndex(Head);
        UpdateCount(Head);
        UpdateLast(Head);
    }

    

    public void DeleteHeadNextNode()
    {
        var q = Head.GetComponent<Node>().Next;//q为被删节点
        var r = q.GetComponent<Node>().Next;//被删节点后继

        //删除操作
        Head.GetComponent<Node>().Next = r;
        r.GetComponent<Node>().Prior = q.GetComponent<Node>().Prior;
        q.GetComponent<Node>().Next = null;
        q.GetComponent<Node>().Prior = null;

        //入对象池
        ObjectPool.Instance.PushObject(q);

        //更新下标
        UpdateIndex(Head);
        //更新数量
        UpdateCount(Head);
        //更新尾节点
        UpdateLast(Head);
    }

    public void DeleteNode(GameObject current)
    {
        var prior = current.GetComponent<Node>().Prior;//被删节点前驱
        var next = current.GetComponent<Node>().Next;//被删节点后继

        if (next != null)
        {
            //删除操作
            prior.GetComponent<Node>().Next = next;
            next.GetComponent<Node>().Prior = prior;
            current.GetComponent<Node>().Next = null;
            current.GetComponent<Node>().Prior = null;
        }
        else
        {
            //删除操作
            prior.GetComponent<Node>().Next = null;
            current.GetComponent<Node>().Next = null;
            current.GetComponent<Node>().Prior = null;
        }

        //入对象池
        ObjectPool.Instance.PushObject(current);

        //更新下标
        UpdateIndex(Head);
        UpdateCount(Head);
        UpdateLast(Head);
    }

    public GameObject GetNodeByIndex(GameObject head,int index)
    {
        var temp = head;
        if (index > count)
            return null;
        while (temp != null)
        {
            if (temp.GetComponent<Node>().index == index)
                return temp;
            else
                temp = temp.GetComponent<Node>().Next;
        }
        Debug.LogWarning("Failed:GetNodeByIndex-index=" + index);
        return null;
    }

    public void GenerateNodeByIndex(GameObject head, int index,int NodeID,int WeaponID)
    {
        if (index - 1 < 0||index-1>count)
        {
            Debug.LogWarning("Failed:GenerateNodeByIndex-index=" + index);
            return;
        }

        var prior = GetNodeByIndex(Head, index - 1);

        var current = factory.GetElement(NodeID, WeaponID);

        if (index - 1 == count)//index为尾节点时
        {
            prior.GetComponent<Node>().Next = current;
            current.GetComponent<Node>().Prior = prior;
            current.GetComponent<Node>().Next = null;
        }
        else//index不为尾节点时
        {
            var next = GetNodeByIndex(Head, index);
            prior.GetComponent<Node>().Next = current;
            current.GetComponent<Node>().Prior = prior;
            current.GetComponent<Node>().Next = next;
            next.GetComponent<Node>().Prior = current;
        }       

        UpdateIndex(Head);
        UpdateCount(Head);
        UpdateLast(Head);
    }

    public void TriColorMatch()//三同色升级算法
    {
        int temp = 1;
        var first = GetNodeByIndex(Head, temp);
        var second = GetNodeByIndex(Head, temp+1);
        var third = GetNodeByIndex(Head, temp+2);

        //测试获取节点信息
        //Debug.Log("First-index:" + first.GetComponent<Node>().index+"-level:"+ first.GetComponent<Node>().level+"-weapon:"+ first.GetComponent<Node>().weaponcolor);

        while (first != null && second != null && third != null)
        {
            if (first.GetComponent<Node>().level == second.GetComponent<Node>().level && third.GetComponent<Node>().level == second.GetComponent<Node>().level && third.GetComponent<Node>().level == first.GetComponent<Node>().level)
            {
                if (first.GetComponent<Node>().weaponColor == second.GetComponent<Node>().weaponColor && third.GetComponent<Node>().weaponColor == second.GetComponent<Node>().weaponColor && third.GetComponent<Node>().weaponColor == first.GetComponent<Node>().weaponColor)
                {
                    DeleteNode(first);
                    DeleteNode(second);
                    DeleteNode(third);
                    GenerateNodeByIndex(Head, temp, first.GetComponent<Node>().level + 1, first.GetComponent<Node>().weaponColor);
                    TriColorMatch();
                    
                    break;
                }
                else
                {
                    temp++;
                    first = GetNodeByIndex(Head, temp);
                    second = GetNodeByIndex(Head, temp + 1);
                    third = GetNodeByIndex(Head, temp + 2);
                   
                }
                    
            }
            else
            {
                temp++;
                first = GetNodeByIndex(Head, temp);
                second = GetNodeByIndex(Head, temp + 1);
                third = GetNodeByIndex(Head, temp + 2);
                
            }
                
            
        }
        UpdateList(Head);
    }

    public void CombineColors()//可合成颜色算法
    {
        int level;
        int weaponcolor;
        int temp = 1;
        var first = GetNodeByIndex(Head, temp);
        var second = GetNodeByIndex(Head, temp + 1);

        while (first != null && second != null)
        {
            if (first.GetComponent<Node>().level == second.GetComponent<Node>().level && first.GetComponent<Node>().weaponColor != second.GetComponent<Node>().weaponColor
                &&(first.GetComponent<Node>().weaponColor<=3&& second.GetComponent<Node>().weaponColor<=3 ))
            {
                level = first.GetComponent<Node>().level;
                weaponcolor = first.GetComponent<Node>().weaponColor + second.GetComponent<Node>().weaponColor + 1;
                //Debug.Log(weaponcolor);
                DeleteNode(first);
                DeleteNode(second);
                GenerateNodeByIndex(Head, temp,level,weaponcolor);
                CombineColors();
                break;
            }
            else
            {
                temp++;
                first = GetNodeByIndex(Head, temp);
                second = GetNodeByIndex(Head, temp + 1);
            }
                
        }
        TriColorMatch();
        UpdateList(Head);
    }

    private void UpdateIndex(GameObject head)
    {
        var p = head.GetComponent<Node>().Next;
        int index = 1;
        while (p != null)
        {
            p.GetComponent<Node>().index = index;
            index++;
            p = p.GetComponent<Node>().Next;
        }
    }

    private void UpdateCount(GameObject head)
    {
        var p = head.GetComponent<Node>().Next;
        int temp = 0;
        while (p != null)
        {
            temp++;
            p = p.GetComponent<Node>().Next;
        }
        count = temp;
    }

    private void UpdateLast(GameObject head)
    {
        GameObject t = head;
        while (t.GetComponent<Node>().Next != null)
        {
            t = t.GetComponent<Node>().Next;
        }
        Last = t;
    }

    private void UpdateList(GameObject head)
    {   
        GameObject t = head;
        SnakeList.Clear();
        while (t != null)
        {
            SnakeList.Add(t);
            t = t.GetComponent<Node>().Next;
        }
    }
}
