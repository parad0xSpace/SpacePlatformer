using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using https://youtu.be/bZVqz_3_NmQ

public class CharaController : MonoBehaviour
{
    private float moveDir; //movementInputDirection
    private float jumpTimer;
    private float turnTimer;  
    // private float wallJumpTimer;
    private float dashTimer; //dashTimeLeft
    private float lastImageXPos;
    private float lastDash = -100;
    private float groundCheckTimer;


    private bool isFacingRight = true;
    //private bool isWalking;
    private bool isGrounded;
    private bool isJumping;
    private bool wallCollision; //isTouchingWall
    private bool canGroundJump;
    private bool canWallJump;
    private bool isSliding;
    private bool jumpAttempt;
    private bool checkJumpMulti;
    private bool isWallJumping;
    // private bool hasWallJumped;
    private bool isDashing;
    private bool canMove;
    private bool canFlip;
    private bool checkedCircle;

    private int jumpCount;
    private int faceDir = 1;
    private int dashCount;
   // private int lastWJumpDir;

    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 10f;
    public float jumpForce = 20f;
    public float groundCheckRadius = .05f;
    public float wallCheckDistance = 1f;
    public float wallSlideSpeed = 1.5f;
    //public float airMoveSpeed = 10f;
    public float dragMulti = .95f;
    public float jumpHeightVar = .5f;
    //public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = .15f;
    public float turnTimerSet = .15f;
    // public float wallJumpTimerSet = .2f;
    public float dashTimerSet;
    public float dashSpeed;
    public float imageDistance; //distanceBetweenImages
    public float dashCooldown;
    public float groundCheckTimerSet = .05f;

    public int jumpMax = 1;
    public int dashMax = 1;

    [HideInInspector]
    public bool hasClaws;
    [HideInInspector]
    public bool hasPack;
    [HideInInspector]
    public bool isTalking;

    public Vector2 wallJumpDir;

    public LayerMask whatIsGround;

    public Transform groundCheck;
    public Transform wallCheck;

    public GameObject wallGrit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpCount = jumpMax;
        //Debug.Log("Jump Max: " + jumpMax);
        //Debug.Log("Jump count: " + jumpCount);
        dashCount = dashMax;

        wallJumpDir.Normalize();

        jumpTimer = jumpTimerSet;
        groundCheckTimer = 0f;

        canMove = true;
        canFlip = true;
        isTalking = false;
    }

    void Update()
    {
        CheckInput();
        CheckMoveDir();
        UpdateAnims();
        JumpCheck();
        SlideCheck();
        CheckDash();
        JumpTime();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isSliding && moveDir == 0 && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x * dragMulti, rb.velocity.y);
        }
        else if (canMove && !isWallJumping)
        {
            rb.velocity = new Vector2(moveSpeed * moveDir, rb.velocity.y);
        }

        if (isSliding && !isWallJumping && hasClaws)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(0f, -wallSlideSpeed);
            }
        }

        if(isTalking)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void CheckSurroundings()
    {
        checkedCircle = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(checkedCircle &&rb.velocity.y < .01f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        wallCollision = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if (!wallCollision)
        {
            isWallJumping = false;
        }
    }

    private void CheckMoveDir()
    {
        if (isFacingRight && moveDir < 0)
        {
            Flip();
        }
        if (!isFacingRight && moveDir > 0)
        {
            Flip();
        }
    }

    private void CheckInput()
    {
        moveDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            if((isGrounded || !wallCollision) && canGroundJump)
            {
                //Debug.Log(jumpCount);
                GroundJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                jumpAttempt = true;
            }
        }

        if(Input.GetButtonDown("Horizontal") && wallCollision && hasClaws)
        {
            if(!isGrounded && moveDir != faceDir)
            {
                canMove = false;
                canFlip = false;
                turnTimer = turnTimerSet;
            }
        }

        if (!canMove && turnTimer > 0)
        {
            turnTimer -= Time.deltaTime;
        }
        else if (turnTimer <= 0)
        {
            canMove = true;
            canFlip = true;
        }

        if (checkJumpMulti && !Input.GetButton("Jump"))
        {
            checkJumpMulti = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightVar);
        }

        if(Input.GetButtonDown("Dash") && hasPack)
        {
            if(Time.time >= (lastDash + dashCooldown) && dashCount > 0)
            {
                //Debug.Log("Trying to dash");
                AttemptToDash();
            }
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimer = dashTimerSet;
        lastDash = Time.time;
        dashCount--;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXPos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isGrounded)
        {
            dashCount = dashMax;
        }

        if (isDashing)
        {
            if (dashTimer > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * faceDir, 0f);
                dashTimer -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXPos) > imageDistance)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXPos = transform.position.x;
                }
            }

            if(dashTimer <= 0 || wallCollision)
            {
                canMove = true;
                canFlip = true;
                isDashing = false;
            }
        }
    }

    private void JumpCheck() //CheckIfCanJump
    {
        if(groundCheckTimer > 0)
        {
            groundCheckTimer -= Time.deltaTime;
        }

        if (isGrounded && groundCheckTimer <= 0)
        {
            isJumping = false;
            jumpCount = jumpMax;
        }

        if (jumpCount <= 0)
        {
            canGroundJump = false;
        }
        else if(jumpCount > 0)
        {
            canGroundJump = true;
        }

        if (wallCollision && hasClaws)
        {
            canWallJump = true;
        }

        /*if(hasWallJumped && moveDir == -lastWJumpDir)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            hasWallJumped = false;
        }
        else if(wallJumpTimer <= 0)
        {
            hasWallJumped = false;
        }
        else
        {
            wallJumpTimer -= Time.deltaTime;
        }*/
    }

    private void JumpTime()
    {
        if(jumpTimer >= 0 && jumpAttempt)
        {
            if(!isGrounded && wallCollision)
            {
                WallJump();
                //Debug.Log("Wall jump");
            }
            else if(isGrounded && canGroundJump)
            {
                GroundJump();
                //Debug.Log("Delayed ground jump");
            }
        }

        if(jumpAttempt)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void GroundJump()
    {
        if (!isTalking)
        {
            //Debug.Log("Grounded status pre-jump: " + isGrounded);

            //Debug.Log("Jumps remaining pre-jump: " + jumpCount);
            //Debug.Log("Jumps remaining post-jump: " + jumpCount);
            //Debug.Log("Ground jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpTimer = 0f;
            isGrounded = false;
            jumpCount--;
            groundCheckTimer = groundCheckTimerSet;
            jumpAttempt = false;
            checkJumpMulti = true;
            //Debug.Log("Grounded status post-jump: " + isGrounded);
        }
    }

    private void WallJump()
    {
        if (!isTalking && canWallJump && hasClaws)
        {
            isSliding = false;
            isJumping = true;
            isWallJumping = true;
            jumpCount = jumpMax;
            jumpCount--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDir.x * -faceDir, wallJumpForce * wallJumpDir.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0f;
            jumpAttempt = false;
            checkJumpMulti = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            //hasWallJumped = true;
            //wallJumpTimer = wallJumpTimerSet;
            //lastWJumpDir = -faceDir;
        }
    }

    private void SlideCheck() //CheckIfWallSliding
    {
        if (wallCollision && moveDir == faceDir && rb.velocity.y < 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
    }

    private void Flip()
    {
        if (!isSliding && canFlip && !isTalking)
        {
            faceDir *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void UpdateAnims()
    {
        if(!isTalking)
        {
            anim.SetFloat("isWalking", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            anim.SetBool("isJumping", isJumping);
            anim.SetBool("isSliding", isSliding);
            if (isSliding)
            {
                wallGrit.SetActive(true);
            }
            else
            {
                wallGrit.SetActive(false);
            }
        }
    }

    public void HasClaws()
    {
        PlayerPrefs.SetInt("ClimbingClaws", 1);
        hasClaws = true;
    }

    public void HasBooster()
    {
        PlayerPrefs.SetInt("BoosterPack", 1);
        hasPack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3 (wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
