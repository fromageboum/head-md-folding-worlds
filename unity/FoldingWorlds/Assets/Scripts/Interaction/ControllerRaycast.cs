using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class ControllerRaycast : MonoBehaviour
{
    public GameObject movementTarget;

    XRRayInteractor rayInteractor;
    RaycastHit hit;
    XRInteractorLineVisual lineVisual;
    // turn this on or off

    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();

        Assert.IsNotNull(movementTarget);
    }

    private void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit)) {
            movementTarget.transform.position = raycastHit.point;
        }
    }

    private void OnEnable()
    {
        lineVisual.enabled = true;
        rayInteractor.enabled = true;
    }

    private void OnDisable()
    {
        lineVisual.enabled = false;
        rayInteractor.enabled = false;
    }
}
