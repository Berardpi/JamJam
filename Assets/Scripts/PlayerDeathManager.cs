using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField]
    float waitTimeBeforeReset = 2f;

    Animator animator;
    Rigidbody2D myRigidbody;
    GameManager gameManager;

    bool isAlive = true;

    public bool getIsAlive()
    {
        return isAlive;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap")
        {
            Die(other.gameObject);
        }
    }

    void Die(GameObject dangerObject)
    {
        if (isAlive)
        {
            isAlive = false;
            TriggerAnimation();
            StartCoroutine(HandleDeath(dangerObject));
        }
    }

    IEnumerator HandleDeath(GameObject dangerObject)
    {
        yield return new WaitForSecondsRealtime(waitTimeBeforeReset);

        if (HealthManager.Instance.getCurrentHealth() > 1)
        {
            HealthManager.Instance.LoseHealth();
            HandlePowerUp(dangerObject);
            gameManager.ResetLevel();
        }
        else
        {
            gameManager.HardResetLevel();
        }
    }

    void TriggerAnimation()
    {
        myRigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead", true);
    }

    void HandlePowerUp(GameObject dangerObject)
    {
        Danger danger = dangerObject.GetComponent<Danger>();

        if (danger != null)
        {
            PowerUpManager.Instance.ActivatePowerUp(danger.GetGainedPowerUp());
        }
    }
}
