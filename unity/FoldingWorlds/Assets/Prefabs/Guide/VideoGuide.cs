using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VLB;
using UnityEngine.Video;

public class VideoGuide : MonoBehaviour
{
    // Start is called before the first frame update
    // Check when button is pressed remove the thing

    public InputActionReference sideButtonReference;
    GameObject videoGuide;

    public static GameObject videoGuideSingleton;

    // todo get the global direction vector to see if perpendicular to the table or not

    private void Start()
    {
        videoGuideSingleton = gameObject;
        videoGuide = GetComponentInChildren<VideoPlayer>().gameObject;
    }

    private void OnEnable()
    {
        sideButtonReference.action.started += CalibrateAndStart;
    }

    private void OnDisable()
    {
        sideButtonReference.action.started -= CalibrateAndStart;
    }


    private void CalibrateAndStart(InputAction.CallbackContext callbackContext)
    {
        //videoGuide.SetActive(false);
        Debug.Log("SHOULD START THE CALIBRATION NOW");
        
  
    }

}
