﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isControllable = true;

    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float hangTime = 0.2f;
    [SerializeField] float jumpBuffer = 0.1f;

    private Rigidbody2D myRigidbody2D;
    private CapsuleCollider2D feetCollider;

    private bool isGrounded;
    private float hangTimeCounter;
    private float jumpBufferCounter;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        feetCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    private void Update()
    {
        CheckGrounded();
        CheckJumpingConditions();

        if (isControllable)
        {
            MoveSideways();
            Jump();
        }
    }

    private void CheckGrounded()
    {
        isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Stepable"));
    }

    private void CheckJumpingConditions()
    {
        if (isGrounded)
        {
            hangTimeCounter = hangTime;
        }
        else
        {
            hangTimeCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (CanJumpThisFrame())
        {
            jumpBufferCounter = 0;
            ThrustUpwards();
        }

        if (JumpIsCancelled())
        {
            hangTimeCounter = 0;

            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x
                                                , myRigidbody2D.velocity.y * .3f);
        }
    }

    private bool CanJumpThisFrame()
    {
        return hangTimeCounter > 0 && jumpBufferCounter > 0;
    }

    private bool JumpIsCancelled()
    {
        return (Input.GetButtonUp("Jump") && myRigidbody2D.velocity.y > 0);
    }

    private void ThrustUpwards()
    {
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
    }

    private void MoveSideways()
    {
        Walk();
    }

    private void Walk()
    {
        float horizontalThrust = Input.GetAxisRaw("Horizontal");

        myRigidbody2D.velocity = new Vector2(horizontalThrust * walkingSpeed
                                             , myRigidbody2D.velocity.y);
    }
}
