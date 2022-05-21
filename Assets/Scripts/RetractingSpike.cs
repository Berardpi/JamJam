using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractingSpike : MonoBehaviour
{
    [SerializeField]
    float animationStartDelay = 0f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetFloat("cycleOffset", animationStartDelay);
    }
}
