using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public float moveSpeed = 10;
    public float jumpSpeed = 20;
    public Animator animator;

    private Vector2 inputVect = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(inputVect.x * moveSpeed, body.velocity.y);
        UpdateAnimations();
    }

    void OnMove(InputValue value)
    {
        inputVect = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
    }

    void UpdateAnimations()
    {
        animator.SetFloat("speed", Mathf.Abs(body.velocity.x));
        if (body.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (body.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }
}
