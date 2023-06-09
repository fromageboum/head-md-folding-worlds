using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightController : MonoBehaviour
{
    public ActionBasedController controller = null; // Assign your XRController here in the inspector
    ControllerRaycast controllerRaycast;
    bool lightOn = true;

    private void Start()
    {
        controllerRaycast = GetComponent<ControllerRaycast>();
    }

    void Update()
    {

        if (controller.activateAction.action.triggered) {
            lightOn = !lightOn;
            controllerRaycast.enabled = lightOn;
            Debug.Log("Primary button was pressed.");
        }
    }
}
