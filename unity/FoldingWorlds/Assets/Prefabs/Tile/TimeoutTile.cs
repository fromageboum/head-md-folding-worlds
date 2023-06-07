using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TimeoutTile : MonoBehaviour
{
    public float timeout = 5f;

    XRSimpleInteractable xrSimpleInteractable;
    Tile tile;

    Coroutine closeCoroutine;

    void Start()
    {
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
        tile = GetComponent<Tile>();

        xrSimpleInteractable.hoverEntered.AddListener(OnFlashlightEntered);
        xrSimpleInteractable.hoverExited.AddListener(OnFlashlightExited);
    }

    private void OnFlashlightEntered(HoverEnterEventArgs arg0)
    {
        tile.OpenTile();
    }

    public void OnFlashlightExited(HoverExitEventArgs arg0)
    {
        if (closeCoroutine != null) {
            StopCoroutine(closeCoroutine);
        }
        closeCoroutine = StartCoroutine(_CloseAfterDelay());
    }

    private IEnumerator _CloseAfterDelay() {
        yield return new WaitForSeconds(timeout);
        tile.CloseTile();
        closeCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
