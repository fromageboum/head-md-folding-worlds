using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTransform : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        transform.position = target.transform.position;
        target.rotation = target.transform.rotation;
    }

}
