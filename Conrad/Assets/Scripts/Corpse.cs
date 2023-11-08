using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseController : MonoBehaviour
{
    private Animator animator;

    public float animationSpeed = 0.5f;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
        animator.Play("s_tendrill_death");
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.enabled = false;
        }
    }
}