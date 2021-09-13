using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public enum States {stunned, normal, dashing}
    public bool readyDash;
    States playerState;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public Rigidbody2D rb;
    Vector2 movement;
    public float chargeTime;
    Vector2 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerState = States.normal;
        readyDash = true;
        chargeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");      
        if (Input.GetKeyDown(KeyCode.Space) && readyDash)
        {
            dashDirection = movement.normalized;
            playerState = States.stunned;
        }
        if (Input.GetKey(KeyCode.Space) && readyDash)
        {                       
            chargeTime +=  Time.deltaTime;
        }
        if (Input.GetKeyUp("space") && readyDash)
        {
            StartCoroutine(DashCoroutine());
        }       
    }

    private void FixedUpdate()
    {
        if (playerState == States.normal)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }      
    }

    private IEnumerator DashCoroutine()
    {        
        playerState = States.dashing;
        rb.velocity = dashDirection * dashSpeed * Mathf.Clamp(chargeTime,1,3f);
        readyDash = false;
        yield return new WaitForSeconds(0.3f);
        ResetPlayerState();
        StartCoroutine(DashCooldownCouroutine(3f));       
    }

    private IEnumerator DashCooldownCouroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        readyDash = true;
    }
    void ResetPlayerState()
    {
        rb.velocity = new Vector2(0, 0);
        playerState = States.normal;
        chargeTime = 0;
        dashDirection = new Vector2(0, 0);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if(hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().EnemyStagger(1f);
                hit.GetComponent<Enemy>().TakeDamage(1f);
            }
        }
    }
}
