using UnityEngine;
using UnityEngine.InputSystem;
using static PowerUpManager;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;

    [SerializeField]
    private float runFasterMoveSpeedDelta;
    public float jumpSpeed = 20;
    public int maxJump = 2;

    [SerializeField]
    private float blinkDistance;
    public Rigidbody2D body;
    public BoxCollider2D feetCollider;
    public SpriteRenderer sprite;
    public Animator animator;
    PlayerDeathManager deathManager;

    private Vector2 inputVect = Vector2.zero;
    private int nbJump = 0;
    private PowerUpManager powerUpManager;

    private void Awake()
    {
        deathManager = GetComponent<PlayerDeathManager>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    void Update()
    {
        if (deathManager.getIsAlive())
        {
            Run();
            UpdateAnimations();
        }
    }

    void OnMove(InputValue value)
    {
        inputVect = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (deathManager.getIsAlive())
        {
            if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                nbJump = 1;
            }
            else if (nbJump < maxJump && powerUpManager.IsPowerUpActive(PowerUp.DoubleJump))
            {
                nbJump++;
            }
            else
            {
                return;
            }
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            animator.SetTrigger("jump");
        }
    }

    void OnUse(InputValue value)
    {
        if (powerUpManager.IsPowerUpActive(PowerUp.Blink))
        {
            Blink();
        }
    }

    void Run()
    {
        float currentMoveSpeed = moveSpeed;
        if (powerUpManager.IsPowerUpActive(PowerUp.RunFaster))
        {
            currentMoveSpeed += runFasterMoveSpeedDelta;
        }

        body.velocity = new Vector2(inputVect.x * currentMoveSpeed, body.velocity.y);
    }

    void Blink()
    {
        if (deathManager.getIsAlive())
        {
            int direction = body.velocity.x > 0 ? 1 : -1;
            transform.position = new Vector2(
                transform.position.x + (blinkDistance * direction),
                transform.position.y
            );
        }
    }

    void UpdateAnimations()
    {
        animator.SetFloat("speed", Mathf.Abs(body.velocity.x));
        animator.SetFloat("verticalSpeed", body.velocity.y);
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
