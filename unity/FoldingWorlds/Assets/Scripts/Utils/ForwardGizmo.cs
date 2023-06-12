using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardGizmo : MonoBehaviour
{
    public float gizmoLength = 1.0f; // Length of the gizmo
    public Color gizmoColor = Color.green; // Color of the gizmo

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawRay(transform.position, transform.forward * gizmoLength);
    }
}