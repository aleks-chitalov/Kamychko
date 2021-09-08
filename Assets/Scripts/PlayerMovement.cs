using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{    
    public enum States {stunned, normal}
    public bool readyDash;
    public bool readyShoot;
    States playerState;
    public float movespeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public float framesPressed;
    Vector2 dashDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerState = States.normal;
        readyDash = true;
        framesPressed = 1;
        readyShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (playerState == States.normal)
        {
            rb.MovePosition(rb.position + movement.normalized * movespeed * Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && readyDash)
        {
            dashDirection = movement.normalized;
            playerState = States.stunned;
            readyShoot = true;
        }
        if (Input.GetKey(KeyCode.Space) && readyDash)
        {                       
            framesPressed = framesPressed + 0.1f;
        }
        if (Input.GetKeyUp("space") && readyShoot)
        {
            StartCoroutine(DashCoroutine());
            readyShoot = false;
        }
        

    }

    private void FixedUpdate()
    {
          
       
    }

    private IEnumerator DashCoroutine()
    {        
       
        rb.velocity = dashDirection * Mathf.Clamp(framesPressed,10f,20f);
        readyDash = false;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = new Vector2(0, 0);
        playerState = States.normal;
        framesPressed = 0;
        dashDirection = new Vector2(0, 0);
        StartCoroutine(DashCooldownCouroutine(3f));       
    }

    private IEnumerator DashCooldownCouroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        readyDash = true;
    }
    
    
}
