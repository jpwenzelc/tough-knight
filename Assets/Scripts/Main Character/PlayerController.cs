using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isControllable = true;
    [SerializeField] float walkingSpeed = 5f;

    private Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isControllable)
        {
            MoveSideways(walkingSpeed);
        }
    }

    private void MoveSideways(float movingSpeed)
    {
        float horizontalThrust = Input.GetAxisRaw("Horizontal");

        myRigidbody2D.velocity = new Vector2(horizontalThrust * movingSpeed
                                             ,myRigidbody2D.velocity.y);
    }
}
