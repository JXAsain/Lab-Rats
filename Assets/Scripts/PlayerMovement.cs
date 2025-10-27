using System.Collections;                 
using System.Collections.Generic;         
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;              // Stores horizontal input (-1 for left, 1 for right, 0 for none)
    private float speed = 8f;              // Movement speed multiplier
    private float sprintSpeed = 12f;
    private float jumpingPower = 16f;      // Force applied when the player jumps
    private float fastDropSpeed = -20f;    // Force applied when fast dropping
    private bool isFacingRight = true;     // Tracks which way the player sprite is currently facing

    private bool isWallClimbing;
    
    //private float wallSlidingSpeed = 2f;

    private float climbSpeed = 8f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;        // Reference to Rigidbody2D for movement physics
    [SerializeField] private Transform groundCheck; // Position below player to check if grounded
    [SerializeField] private LayerMask groundLayer; // Defines what counts as "ground"
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canFastDrop = true;
    [SerializeField] private bool canWallClimb = true;



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

        WallClimbing();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallClimbing()
    {
        if (canWallClimb && IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallClimbing = true;

            float verticalInput = Input.GetAxisRaw("Vertical"); // -1 (down), 0 (none), 1 (up)

            // Apply vertical movement based on input
            rb.linearVelocity = new Vector2(0f, verticalInput * climbSpeed); rb.gravityScale = 0f; // Disable gravity while sticking

        }
        else
        {
            isWallClimbing=false;
            rb.gravityScale = 4f; // Restore normal gravity

        }
    }

    private void WallJump()
    {
        if (isWallClimbing)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else 
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f) 
        {
            isWallJumping=true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
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
