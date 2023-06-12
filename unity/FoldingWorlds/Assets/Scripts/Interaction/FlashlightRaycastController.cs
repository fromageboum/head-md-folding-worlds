using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightRaycastController : MonoBehaviour
{
    public InputActionReference primaryButtonRef;
    ControllerRaycast controllerRaycast;

    private void Start()
    {
        controllerRaycast = GetComponent<ControllerRaycast>();
    }

    private void OnEnable()
    {
        primaryButtonRef.action.started += Toggle;
    }

    private void OnDisable()
    {
        primaryButtonRef.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext callbackContext)
    {
        controllerRaycast.enabled = !controllerRaycast.enabled;
    }

}
