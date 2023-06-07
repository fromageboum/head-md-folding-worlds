using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInEditor : MonoBehaviour
{
    public GameObject gameObject;

    private void Start()
    {
        if (Application.isEditor) {
            gameObject.SetActive(true);
        }
    }
}
