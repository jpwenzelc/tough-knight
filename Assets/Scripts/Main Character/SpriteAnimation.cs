using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] Transform body;

    Rigidbody2D myRigidbody;
    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetSpriteOrientation();
    }

    private void GetSpriteOrientation()
    {
        bool isMovingHorizontally = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (isMovingHorizontally)
        {
            FlipSprite();
            //myAnimator.SetBool("IsRunning", true);
        }
        else
        {
            //myAnimator.SetBool("IsRunning", false);
        }
    }

    private void FlipSprite()
    {
        body.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * 3f, 3f);
    }
}
