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
    List<Animator> animators;

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
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
    }

    public void OpenTile()
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.SetBool("open", true);
            }
        }
        //Crossable = true;
    }

    public void CloseTile()
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.SetBool("open", false);
            }
        }
        Crossable = false;
    }


    private void Update()
    {
        CheckAnimatorsState();
    }

    private void CheckAnimatorsState()
    {
        bool allAnimatorsOpened = true;
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("wa_opened"))
                {
                    allAnimatorsOpened = false;

                    if (Crossable != false) {
                        Crossable = false;
                    }

                    break;
                }
            }
        }

        if (allAnimatorsOpened)
        {
            Crossable = true;
        }
    }


}
