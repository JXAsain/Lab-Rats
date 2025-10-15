using System.Collections;                 
using System.Collections.Generic;         
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;              // Stores horizontal input (-1 for left, 1 for right, 0 for none)
    private float speed = 8f;              // Movement speed multiplier
    private float sprintSpeed = 12f;
    private float jumpingPower = 16f;      // Force applied when the player jumps
    private float fastDropSpeed = -20f;    // Force applied when fast dropping
    private bool isFacingRight = true;     // Tracks which way the player sprite is currently facing

    [SerializeField] private Rigidbody2D rb;        // Reference to Rigidbody2D for movement physics
    [SerializeField] private Transform groundCheck; // Position below player to check if grounded
    [SerializeField] private LayerMask groundLayer; // Defines what counts as "ground"

    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canFastDrop = true;


    // Called every frame (handles input and simple logic)
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input (-1, 0, or 1)

        // Jump if the Jump button is pressed and player is on the ground
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower); // Apply upward velocity
        }

        // If player releases jump button while moving upward, cut the jump short
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); // Reduce upward velocity
        }

        // Fast Drop
        if (!IsGrounded() && canFastDrop && Input.GetAxisRaw("Vertical") < 0)  
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fastDropSpeed);
        }

        Flip(); // Flip sprite direction if player changes movement direction

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

      
    }

    // Called on a fixed time interval (better for physics updates)
    private void FixedUpdate()
    {
        // Decide which speed to use
        float currentSpeed = speed;

        if (canSprint &&  Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }

        rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y); // Apply horizontal movement, keep current vertical velocity

    }

    // Checks if the player is standing on the ground
    private bool IsGrounded()
    {
        // Creates an invisible circle at groundCheck's position, checks collision with groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    // Flips the player sprite if moving left/right opposite to current facing direction
    private void Flip()
    {
        // If facing right but moving left OR facing left but moving right
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;               // Toggle facing direction
            Vector3 localScale = transform.localScale;    // Get current scale
            localScale.x *= -1f;                          // Flip the X axis (mirror sprite)
            transform.localScale = localScale;            // Apply the flipped scale
        }
    }
}
