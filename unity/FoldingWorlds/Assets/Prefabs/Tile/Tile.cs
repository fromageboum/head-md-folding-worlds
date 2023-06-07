using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tile : MonoBehaviour
{
    private bool crossable;
    bool pointedByController;

    XRSimpleInteractable xrSimpleInteractable;

    MeshRenderer meshRenderer;
    NavMeshSourceTag navMeshSourceTag;
    Animator animator;

    public bool Crossable {
        get
        {
            return crossable;
        }
        set
        {
            crossable = value;
            meshRenderer.material.color = value ? Color.green : Color.red;

            if (navMeshSourceTag != null)
            {
                navMeshSourceTag.enabled = value;
            }
        }
    }

    private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        navMeshSourceTag = GetComponent<NavMeshSourceTag>();
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
        animator = GetComponentInChildren<Animator>();
    }

    public void OpenTile()
    {
        if (animator != null) {
            animator.SetBool("open", true);
        }
        Crossable = true;
    }

    public void CloseTile()
    {
        //meshRenderer.material.color = Color.red;
        if (animator != null) {
            animator.SetBool("open", false);
        }
        Crossable = false;
    }

    public void OnTileFullyOpen()
    {
        Debug.Log("The tile is fully open");
    }


}
