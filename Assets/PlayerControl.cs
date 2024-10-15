using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isDashing = false;
    private int score = 0;
    private SpriteRenderer sr;

    [SerializeField] Animator animator;
    [SerializeField] int speed = 0;
    [SerializeField] int jumpForce = 500;
    [SerializeField] int dashForce = 1000; 
    [SerializeField] float dashDuration = 0.1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(speed * movementVector.x, speed * movementVector.y);
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        animator.SetBool("walk_right_bool", !Mathf.Approximately(movementVector.x, 0));
        //if(!Mathf.Approximately(movementVector.x, 0))
        {
            sr.flipX = movementVector.x < 0;
        }
    }

    void OnJump(InputValue value)
    {   
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
    }

    void OnDash(InputValue value)
    {
        if (!isDashing)
        {
            isDashing = true;

            rb.velocity = new Vector2(movementVector.x * dashForce, rb.velocity.y);

            Invoke("EndDash", dashDuration);
        }
    }

    void EndDash()
    {
        isDashing = false;

        rb.velocity = new Vector2(movementVector.x * speed, rb.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My score is " + score);
        }
    }

    
}



