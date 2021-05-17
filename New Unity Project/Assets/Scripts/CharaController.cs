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
    private bool canMove;
    private bool canFlip;
    private bool isWallJumping;
    // private bool hasWallJumped;
    private bool isDashing;
    private bool hasClaws;
    private bool hasPack;

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

    public int jumpMax = 1;
    public int dashMax = 1;

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
        dashCount = dashMax;

        wallJumpDir.Normalize();

        jumpTimer = jumpTimerSet;

        canMove = true;
        canFlip = true;

        if(PlayerPrefs.GetInt("ClimbingClaws") == 1)
        {
            hasClaws = true;
        }
        else
        {
            hasClaws = false;
        }

        if (PlayerPrefs.GetInt("BoosterPack") == 1)
        {
            hasPack = true;
        }
        else
        {
            hasPack = false;
        }
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
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            isJumping = false;
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
            if((isGrounded || (jumpCount > 0 && !wallCollision)) && canGroundJump)
            {
                Debug.Log(jumpCount);
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
        if (isGrounded)
        {
            jumpCount = jumpMax;
        }

        if (wallCollision && hasClaws)
        {
            canWallJump = true;
        }

        if (jumpCount <= 0)
        {
            canGroundJump = false;
        }
        else
        {
            canGroundJump = true;
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
                Debug.Log("Wall jump");
            }
            else if(isGrounded && canGroundJump)
            {
                GroundJump();
                Debug.Log("Delayed ground jump");
            }
        }

        if(jumpAttempt)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void GroundJump()
    {
        if (canGroundJump)
        {
            Debug.Log("Ground jump");
            jumpCount--;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer = 0f;
            jumpAttempt = false;
            checkJumpMulti = true;
        }
    }

    private void WallJump()
    {
        if (canWallJump && hasClaws)
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
        if (!isSliding && canFlip)
        {
            faceDir *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void UpdateAnims()
    {
        anim.SetFloat("isWalking", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isSliding", isSliding);
        if(isSliding)
        {
            wallGrit.SetActive(true);
        }
        else
        {
            wallGrit.SetActive(false);
        }
    }

    public void HasClaws()
    {
        PlayerPrefs.SetInt("ClimbingClaws", 1);
    }

    public void HasBooster()
    {
        PlayerPrefs.SetInt("BoosterPack", 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3 (wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
