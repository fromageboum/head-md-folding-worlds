using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TimeoutTile : MonoBehaviour
{
    public float timeout = 5f;
    bool inContactWithPlayer = false;
    bool flashedWithFlashLight = false;

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

    public void OnFlashlightEntered(HoverEnterEventArgs arg0)
    {
        flashedWithFlashLight = true;
        tile.OpenTile();
        CancelCountdown();
    }

    public void OnFlashlightExited(HoverExitEventArgs arg0)
    {
        flashedWithFlashLight = false;

        if (!inContactWithPlayer && !flashedWithFlashLight)
        {
            StartCountdown();
        }
    }

    private IEnumerator _CloseAfterDelay() {
        yield return new WaitForSeconds(timeout);

        tile.CloseTile();
        Debug.Log(name + " sent close time msg");

        closeCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inContactWithPlayer = true;
            CancelCountdown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inContactWithPlayer = false;

            if (!inContactWithPlayer && !flashedWithFlashLight) {
                StartCountdown();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

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

    void CancelCountdown() {
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
    }

    void StartCountdown() {
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
        closeCoroutine = StartCoroutine(_CloseAfterDelay());
    }

}
