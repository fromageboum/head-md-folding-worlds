using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool crossable;
    private bool inContactWithPlayer;

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
            if (meshRenderer != null) {
                meshRenderer.material.color = value ? Color.green : Color.red;

            }

            if (navMeshSourceTag != null)
            {
                navMeshSourceTag.enabled = value;
            }
        }
    }

    private void Start()
    {
        Crossable = crossable; // Trigger the setter
    }

    // This method is called whenever a script is loaded or a value is changed in the Inspector.
    private void OnValidate()
    {
        Crossable = crossable; // Trigger the setter
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
