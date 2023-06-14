using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public float minTimeSoundTriggerTime = 3f;
    public float maxTimeSoundTriggerTime = 10f;

    public RandomSound randomSoundIdle;
    public RandomSound randomSoundMoving;

    private Animator animator;
    private float nextSoundTriggerTime;

    IEnumerator Start()
    {
        animator = GetComponentInChildren<Animator>();
        nextSoundTriggerTime = Random.Range(minTimeSoundTriggerTime, maxTimeSoundTriggerTime);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSoundTriggerTime, maxTimeSoundTriggerTime));

            Debug.Log("Trigger");

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                Debug.Log("Play random sound idle");
                randomSoundIdle.PlayRandomSound();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk 1"))
            {
                Debug.Log("Play random sound walk 1");
                randomSoundMoving.PlayRandomSound();
            }
        }
    }


}
