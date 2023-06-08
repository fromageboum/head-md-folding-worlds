using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Tile))]
[RequireComponent(typeof(XRSimpleInteractable))]
public class AlwaysOpenTile : MonoBehaviour
{
    // Once it is opened, it stays open
    XRSimpleInteractable xrSimpleInteractable;
    Tile tile;

    void Start()
    {
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
        tile = GetComponent<Tile>();

        xrSimpleInteractable.hoverEntered.AddListener(OnFlashlightEntered);
    }

    private void OnFlashlightEntered(HoverEnterEventArgs arg0)
    {
        tile.OpenTile();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 globalScale = GetGlobalScale(transform);
        float objectSize = globalScale.magnitude;
        float sphereRadius = objectSize * 0.1f;  // 10% of object's size

        //Gizmos.DrawSphere(transform.position, sphereRadius);
    }

    private Vector3 GetGlobalScale(Transform transform)
    {
        if (transform.parent == null)
        {
            return transform.localScale;
        }
        else
        {
            return Vector3.Scale(transform.parent.lossyScale, transform.localScale);
        }
    }

}
