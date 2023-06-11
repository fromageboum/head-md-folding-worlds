using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{

    public InputActionReference primaryButtonRef;
    Light _light;

    private void Start()
    {
        _light = GetComponentInChildren<Light>();
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
        //Debug.Log("PRIMARY BUTTON PRESSED");
        _light.enabled = !_light.enabled;
    }

}
