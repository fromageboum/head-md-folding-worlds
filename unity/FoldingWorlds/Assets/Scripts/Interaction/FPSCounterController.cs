using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class FPSCounterController : MonoBehaviour
{
    public InputActionReference fpsToggleReference;
    public GameObject fpsCanvas;

    private void OnEnable()
    {
        fpsToggleReference.action.started += Toggle;
    }

    private void OnDisable()
    {
        fpsToggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext callbackContext) {

        bool isActive = fpsCanvas.activeSelf;
        fpsCanvas.SetActive(!isActive);
    }

}
