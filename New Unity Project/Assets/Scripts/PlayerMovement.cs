using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool moveActive = true;
    /*int direction;

    public GameObject camControl;

    public Transform faceDirection;
    public Rigidbody2D rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    bool canDash;*/
    

    public float runSpeed = 40f;

    public Animator animator;

    void Start()
    {
        //dashTime = startDashTime;
        //direction = 0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OneHit")
        {
            runSpeed = 0f;
            moveActive = false;
        }
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && moveActive)
        {
            jump = true;
            animator.SetBool("jumping", true);
        }

        if (Input.GetButtonDown("Crouch") && moveActive)
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch") && moveActive)
        {
            crouch = false;
        }

        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = 4;
            //camControl.ZoomOut();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 3;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 5;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 2;
        }
        else
        {
            direction = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && moveActive && canDash)
        {
            animator.SetBool("dashing", true);
            canDash = false;
        } */
    }

    public void OnLanding()
    {
        animator.SetBool("jumping", false);
        //animator.SetBool("dashing", false);
        //canDash = true;
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("crouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
