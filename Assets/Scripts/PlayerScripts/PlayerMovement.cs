using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isFacingRight = true;
    public Animator animator;
    float currSpeed;
    public bool canMove;


    [Header("Walking")]
    public float walkSpeed = 3f;
    float horizontalMovement;

    [Header("Running")]
    public float runSpeed = 7f;
    public bool isRunning = false;

    [Header("Jump")]
    public float jumpForce = 10f;
    public int maxJumps = 2;
    int jumpsLeft;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Crouch")]
    public CapsuleCollider2D upColl;
    public BoxCollider2D crouchColl;

    [Header("Roof Check")]
    public Transform roofCheckPos;
    public Vector2 roofCheckSize = new Vector2(0.5f, 0.5f);
    bool canGetUp = true;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMulti = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (!canMove)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            currSpeed = isRunning ? runSpeed : walkSpeed;
            rb.linearVelocity = new Vector2(horizontalMovement * currSpeed, rb.linearVelocity.y);
            GroundCheck();
            RoofCheck();
            Gravity();
            Flip();

            animator.SetFloat("yVelocity", rb.linearVelocity.y);
            animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        }
        
    }

    public void Movement(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsLeft > 0)
        {
            if (context.performed && canGetUp)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpsLeft--;
                animator.SetTrigger("Jump");
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsLeft--;
                animator.SetTrigger("Jump");
            }
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsLeft = maxJumps;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            crouchColl.enabled = true;
            upColl.enabled = false;
            animator.SetBool("isCrouching", true);
        }
        else if(context.canceled && canGetUp)
        {
            crouchColl.enabled = false;
            upColl.enabled = true;
            animator.SetBool("isCrouching", false);
        }
    }

    private void RoofCheck()
    {
        if (Physics2D.OverlapBox(roofCheckPos.position, roofCheckSize, 0, groundLayer))
        {
            canGetUp = false;

        }
        else 
        {
            canGetUp = true;
            if (!Keyboard.current.ctrlKey.isPressed)
            {
                crouchColl.enabled = false;
                upColl.enabled = true;
                animator.SetBool("isCrouching", false);
            }
        }
    } 

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMulti;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.DrawWireCube(roofCheckPos.position, roofCheckSize);
    }


}
