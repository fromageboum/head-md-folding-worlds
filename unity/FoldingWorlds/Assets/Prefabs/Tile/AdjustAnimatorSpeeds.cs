using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAnimatorSpeeds : MonoBehaviour
{

    [SerializeField]
    float animationSpeed = 1.0f;

    List<Animator> animators;

    void Start()
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
        UpdateAnimationSpeed(animationSpeed);

    }

    // Called whenever a script is loaded or a value is changed in the Inspector.
    void OnValidate()
    {
        if (animators == null || animators.Count == 0)
        {
            Start();
        }

        UpdateAnimationSpeed(animationSpeed);
    }

    public void UpdateAnimationSpeed(float speed)
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.speed = speed;
            }
        }
    }

}
