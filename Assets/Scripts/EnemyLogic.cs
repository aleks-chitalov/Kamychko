using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public float chaseRadius;
    public float attackRadius;
    public float enemySpeed;
    public Transform targetPosition;
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
            Vector2 temp = Vector2.MoveTowards(transform.position, targetPosition.position, enemySpeed * Time.fixedDeltaTime);
            rb.MovePosition(temp);
        }
    }   
}
