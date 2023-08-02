using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{

    [SerializeField] private float forwardSpeed;//前进速度

    [SerializeField] private float handlingCoefficient;//操控系数

    [SerializeField] InputActionReference axisInput;

    //---------------------------------------------------------------------//

    public Rigidbody2D rb;

    private float rotateDirection;

    public float ForwardSpeed
    {
        get { return forwardSpeed; }
        set { forwardSpeed = value; }
    }

    public float HandlingCoefficient
    {
        get { return handlingCoefficient; }
        set { handlingCoefficient = value; }
    }

    //---------------------------------------------------------------------//

    #region Unity生命周期函数
    private void FixedUpdate()
    {
        //持续向前移动
        rb.transform.position += -rb.transform.up * ForwardSpeed * Time.deltaTime;
        //读取Input输入值
        rotateDirection = axisInput.action.ReadValue<Single>();
        //转向
        rb.transform.Rotate(Vector3.forward * rotateDirection * HandlingCoefficient * Time.deltaTime);       
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();     
    }

    private void Start()
    {
        rb.gravityScale = 0;
    }
    #endregion

}
