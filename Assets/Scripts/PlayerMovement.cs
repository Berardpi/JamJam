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

    [SerializeField]
    float runFasterBlinkDistanceDelta;
    Vector2 inputVect = Vector2.zero;
    int nbJump = 0;

    [Header("Components")]
    public Rigidbody2D body;
    public BoxCollider2D feetCollider;
    public SpriteRenderer sprite;
    public Animator animator;

    [Header("Managers")]
    PlayerDeathManager deathManager;
    SoundEffectManager soundEffectManager;

    [Header("Particules")]
    [SerializeField]
    ParticleSystem jumpDust;

    [SerializeField]
    ParticleSystem speedDust;

    [SerializeField]
    ParticleSystem blinkDustStart;

    [SerializeField]
    ParticleSystem blinkDustEnd;

    void Awake()
    {
        deathManager = GetComponent<PlayerDeathManager>();
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
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
            else if (
                nbJump < maxJump && PowerUpManager.Instance.IsPowerUpActive(PowerUp.DoubleJump)
            )
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
        if (PowerUpManager.Instance.IsPowerUpActive(PowerUp.Blink))
        {
            Blink();
        }
    }

    void Run()
    {
        float currentMoveSpeed = moveSpeed;
        if (PowerUpManager.Instance.IsPowerUpActive(PowerUp.RunFaster))
        {
            currentMoveSpeed += runFasterMoveSpeedDelta;
        }

        body.velocity = new Vector2(inputVect.x * currentMoveSpeed, body.velocity.y);
    }

    void Blink()
    {
        if (deathManager.getIsAlive())
        {
            float distance = blinkDistance;
            if (PowerUpManager.Instance.IsPowerUpActive(PowerUp.RunFaster))
            {
                distance += runFasterBlinkDistanceDelta;
            }
            float direction = transform.localScale.x;
            Vector2 destination = new Vector2(
                transform.position.x + (distance * direction),
                transform.position.y
            );
            // heightDelta is use to center raycast on player body
            Vector2 heightDelta = new Vector2(0f, 1f);

            if (
                Physics2D.OverlapPoint(destination - heightDelta, LayerMask.GetMask("Ground"))
                != null
            )
            {
                RaycastHit2D hit = Physics2D.Linecast(
                    (Vector2)transform.position - heightDelta,
                    destination - heightDelta,
                    LayerMask.GetMask("Ground")
                );

                if (hit)
                {
                    float xMargin = 0.3f;
                    destination = new Vector2(
                        hit.point.x - (xMargin * direction),
                        transform.position.y
                    );
                }
                else
                {
                    destination = transform.position;
                }
            }

            transform.position = destination;
            blinkDustStart.transform.localPosition = new Vector3(
                -distance,
                blinkDustStart.transform.localPosition.y,
                0f
            );
            blinkDustStart.Play();
            blinkDustEnd.Play();
            soundEffectManager.PlayBlinkEffect();
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
