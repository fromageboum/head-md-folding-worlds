using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestModule : MonoBehaviour
{
    public bool crossable;
    MeshRenderer mr;
    NavMeshSourceTag navMeshSourceTag;

    bool hovering;

    XRSimpleInteractable xrSimpleInteractable;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        navMeshSourceTag = GetComponent<NavMeshSourceTag>();
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();

        xrSimpleInteractable.hoverEntered.AddListener(OnFlashlightEntered);
        xrSimpleInteractable.hoverExited.AddListener(OnFlashlightExited);
    }

    private void OnFlashlightEntered(HoverEnterEventArgs arg0)
    {
        hovering = true;
    }

    public void OnFlashlightExited(HoverExitEventArgs arg0) {
        hovering = false;
    }

    void Update()
    {
        if (xrSimpleInteractable.enabled) {
            crossable = hovering;
        }

        mr.material.color = crossable ? Color.cyan : Color.magenta;
        navMeshSourceTag.enabled = crossable;
    }

}
