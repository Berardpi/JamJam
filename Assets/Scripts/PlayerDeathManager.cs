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

    bool isAlive = true;

    public bool getIsAlive()
    {
        return isAlive;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap")
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        TriggerAnimation();
        StartCoroutine(ResetLevel());
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSecondsRealtime(waitTimeBeforeReset);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TriggerAnimation()
    {
        myRigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead", true);
    }
}
