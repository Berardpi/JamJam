using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField]
    float waitTimeBeforeReset = 2f;

    Animator animator;
    Rigidbody2D myRigidbody;
    PowerUpManager powerUpManager;
    HealthManager healthManager;

    bool isAlive = true;

    public bool getIsAlive()
    {
        return isAlive;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
        healthManager = FindObjectOfType<HealthManager>();
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

        if (healthManager.getCurrentHealth() > 1)
        {
            healthManager.LoseHealth();
            HandlePowerUp(dangerObject);
        }
        else
        {
            healthManager.ResetHealth();
            powerUpManager.Reset();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            powerUpManager.ActivatePowerUp(danger.GetGainedPowerUp());
        }
    }
}
