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
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
