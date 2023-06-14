using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VLB;

public class FlashlightController : MonoBehaviour
{

    public InputActionReference primaryButtonRef;
    Light _light;
    VolumetricLightBeamSD volumetricLightBeam;
    AudioSource audioSource;

    private void Start()
    {
        _light = GetComponentInChildren<Light>();
        volumetricLightBeam = GetComponentInChildren<VolumetricLightBeamSD>();
        audioSource = GetComponent<AudioSource>();
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
        _light.enabled = !_light.enabled;
        volumetricLightBeam.enabled = _light.enabled;
        audioSource.Play();
    }

}
