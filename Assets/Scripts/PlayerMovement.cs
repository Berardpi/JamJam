using UnityEngine;
using UnityEngine.InputSystem;
using static PowerUpManager;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;

    [SerializeField]
    float runFasterMoveSpeedDelta;
    public float jumpSpeed = 20;
    public int maxJump = 2;

    [SerializeField]
    float blinkDistance;
    Vector2 inputVect = Vector2.zero;
    int nbJump = 0;

    [Header("Components")]
    public Rigidbody2D body;
    public BoxCollider2D feetCollider;
    public SpriteRenderer sprite;
    public Animator animator;

    [Header("Managers")]
    PlayerDeathManager deathManager;
    PowerUpManager powerUpManager;

    [Header("Particules")]
    [SerializeField]
    ParticleSystem jumpDust;

    [SerializeField]
    ParticleSystem speedDust;

    void Awake()
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
                jumpDust.Play();
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
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (body.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Mathf.Abs(body.velocity.x) > moveSpeed && speedDust.isStopped)
        {
            speedDust.Play();
        }
        else if (Mathf.Abs(body.velocity.x) < 0.1 && speedDust.isPlaying)
        {
            speedDust.Stop();
        }
    }
}
