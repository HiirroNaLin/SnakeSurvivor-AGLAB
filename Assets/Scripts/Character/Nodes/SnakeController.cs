using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{

    [SerializeField] private float forwardSpeed;//ǰ���ٶ�

    [SerializeField] private float handlingCoefficient;//�ٿ�ϵ��

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

    #region Unity�������ں���
    private void FixedUpdate()
    {
        //������ǰ�ƶ�
        rb.transform.position += -rb.transform.up * ForwardSpeed * Time.deltaTime;
        //��ȡInput����ֵ
        rotateDirection = axisInput.action.ReadValue<Single>();
        //ת��
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
