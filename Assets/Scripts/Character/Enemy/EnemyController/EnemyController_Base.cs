using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Base : EnemyController
{
    [SerializeField] public Transform target;

    public void Start()
    {

        target = FindObjectOfType<SnakeController>().transform;
    }

    public override void Move()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, target.position, speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }
}
