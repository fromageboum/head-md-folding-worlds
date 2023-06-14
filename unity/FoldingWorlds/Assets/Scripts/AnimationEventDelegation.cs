using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDelegation : MonoBehaviour
{
    CurveAnimation curveAnimation;

    void Start()
    {
        curveAnimation = GetComponentInChildren<CurveAnimation>();
    }
    public void StartRandomRotation(){
            Debug.Log("hepiro"); 
        curveAnimation.StartAnimation();
    }

}
