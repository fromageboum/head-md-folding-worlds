using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VLB;

namespace CreepyCutouts
{
    public class FlashlightController : MonoBehaviour
    {

        public InputActionReference primaryButtonRef;
        Light _light;
        VolumetricLightBeamSD volumetricLightBeam;
        AudioSource audioSource;

        public GameObject[] ObjectsToToggle;

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
            foreach (var item in ObjectsToToggle)
            {
                item.SetActive(!item.activeSelf);
            }
        }

    }
}