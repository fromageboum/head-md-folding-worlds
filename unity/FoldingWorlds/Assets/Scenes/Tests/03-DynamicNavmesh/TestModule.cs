using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModule : MonoBehaviour
{
    [SerializeField] bool crossable;
    MeshRenderer mr;
    NavMeshSourceTag navMeshSourceTag;

    // Set material color
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        navMeshSourceTag = GetComponent<NavMeshSourceTag>();
    }

    void Update()
    {
        mr.material.color = crossable ? Color.cyan : Color.magenta;
        navMeshSourceTag.enabled = crossable;
    }
}
