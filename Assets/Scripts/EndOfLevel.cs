using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    Animator animator;

    Animation anim;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("activate");
        }
    }

    void OnActivateAnimationEnd()
    {
        gameManager.LoadNextLevel();
    }
}
