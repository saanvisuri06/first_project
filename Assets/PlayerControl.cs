using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] int speed = 0;
    [SerializeField] int jumpForce = 500;
    [SerializeField] int dashForce = 1000; 
    [SerializeField] float dashDuration = 0.1f; 

    private bool isGrounded = true;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
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
}



