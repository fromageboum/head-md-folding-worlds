using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLogger : MonoBehaviour
{
    void Update()
    {
        Debug.Log(gameObject.name + " Rotation " + transform.rotation);
        Debug.Log(gameObject.name + " Local Position: " + transform.localPosition);
        Debug.Log(gameObject.name + " Global Position: " + transform.position);
    }

}
