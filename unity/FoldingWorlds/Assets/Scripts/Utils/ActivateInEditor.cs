using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInEditor : MonoBehaviour
{
    public GameObject gameObject;
    public bool activationValue = true;

    private void Start()
    {
        if (Application.isEditor) {
            gameObject.SetActive(activationValue);
        }
    }
}
