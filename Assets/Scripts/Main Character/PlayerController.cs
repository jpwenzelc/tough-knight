using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isControllable = true;

    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float runningSpeed = 8f;

    private Rigidbody2D myRigidbody2D;
    private CapsuleCollider2D feetCollider;

    private bool isGrounded;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        feetCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    private void Update()
    {
        CheckGrounded();

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

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            ThrustUpwards();
        }
    }

    private void ThrustUpwards()
    {
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
    }

    private void MoveSideways()
    {
        if (Input.GetButton("Run"))
        {
            Run();
            return;
        }

        Walk();
    }

    private void Run()
    {
        float horizontalThrust = Input.GetAxisRaw("Horizontal");
        myRigidbody2D.velocity = new Vector2(horizontalThrust * runningSpeed

                                             , myRigidbody2D.velocity.y); ;
    }

    private void Walk()
    {
        float horizontalThrust = Input.GetAxisRaw("Horizontal");

        myRigidbody2D.velocity = new Vector2(horizontalThrust * walkingSpeed
                                             , myRigidbody2D.velocity.y); ;
    }
}
