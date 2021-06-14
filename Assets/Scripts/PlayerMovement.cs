using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 20;
    public Transform feet;
    public LayerMask groundLayers;

    // Settings Toggles
    public bool canJump = true;
    public bool moveInAir = true;
    public bool jumpInAir = false;

    public Animator anim;

    float movementX;

    private void Update()
    {

        if (IsGrounded() || moveInAir) {
            movementX = Input.GetAxisRaw("Horizontal");
        }

        if(canJump && Input.GetButtonDown("Jump") && (IsGrounded() || jumpInAir))
        {
            Jump();
        }

        if (Math.Abs(movementX) == 0) {
            //Animation temp = GetComponent<Animation>();
            //temp.Stop("PlayerRun");
        }

        if (Math.Abs(movementX) > 0.05f) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        
        if (movementX > 0f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (movementX < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y );

        rb.velocity = movement;
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null) {
            return true;
        }
        
        return false;
    }
}
