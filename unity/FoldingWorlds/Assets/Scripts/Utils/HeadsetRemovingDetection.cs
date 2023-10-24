using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class HeadsetRemovingDetection : MonoBehaviour
{
    [Header("Oculus presence is immediate on headset removal.\nWorks only on Meta devices")]
    [SerializeField] private bool useOculusPresence;
    [Header("OpenXR event is fired 15 seconds after removal\n(when entering sleep mode)")]
    [SerializeField] private bool reloadSceneOnRemove;
    [SerializeField] private bool reloadSceneOnPutBack;
    // [Header("Or calls OnHeadsetRemoved :")]
    [SerializeField] private UnityEvent OnHeadsetRemoved;
    [SerializeField] private UnityEvent OnHeadsetPutBack;
    
    private InputDevice headset;
    private bool headsetIsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);
        // no headset, we're most probably in the editor
        if (devices.Count == 0)
        {
            enabled = false;
        }
        else
        {
            headset = devices[0];
            if (useOculusPresence) headsetIsOn = OVRManager.instance.isUserPresent;
            else headset.TryGetFeatureValue(CommonUsages.userPresence, out headsetIsOn);
        }
    }

    void Update()
    {
        // test for headset removal
        bool userPresence;
        if (useOculusPresence) userPresence = OVRManager.instance.isUserPresent;
        else headset.TryGetFeatureValue(CommonUsages.userPresence, out userPresence);
        
        if (headsetIsOn && !userPresence)
        {
            HeadsetRemoved();
        }
        // on headset is put back
        else if (!headsetIsOn && userPresence)
        {
            HeadsetPutBack();
        }
    }

    private void HeadsetRemoved()
    {
        headsetIsOn = false;
        OnHeadsetRemoved?.Invoke();
        if (reloadSceneOnRemove) ReloadScene();
    }

    private void HeadsetPutBack()
    {
        headsetIsOn = true;
        OnHeadsetPutBack?.Invoke();
        if (reloadSceneOnPutBack) ReloadScene();
    }

    public void ReloadScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
