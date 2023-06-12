using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    public float idleDistance = 0.2f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < idleDistance)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }
}
