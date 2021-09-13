using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : Enemy
{
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private Transform targetPosition;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        targetPosition = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        MoveEnemy();
    }
    void MoveEnemy()
    {
        if(Vector3.Distance(targetPosition.position, transform.position) <= chaseRadius && Vector3.Distance(targetPosition.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(transform.position, targetPosition.position, enemyMoveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(temp);
            }
        }
    }
    void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }  
}
