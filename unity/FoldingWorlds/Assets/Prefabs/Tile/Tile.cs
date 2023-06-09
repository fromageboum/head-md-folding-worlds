using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool crossable;

    [SerializeField]
    private bool hidden;

    private bool inContactWithPlayer;

    bool pointedByController;

    XRSimpleInteractable xrSimpleInteractable;

    MeshRenderer meshRenderer;
    NavMeshSourceTag navMeshSourceTag;
    List<Animator> animators;

    public bool openAtStart = false;
    public bool hiddenOnStart = false;


    public bool Hidden {
        get => hidden;
        set
        {
            hidden = value;
            float target = hidden ? 0f : 1f;

            if (Application.isPlaying)
            {
                //transform.DOScale(target, 0.3f).SetEase(Ease.InOutQuad);

                // Create the sequence
                Sequence sequence = DOTween.Sequence();
                sequence.Append(transform.DOScale(new Vector3(1f, 1f - target, 1f), 0.001f));
                sequence.Append(transform.DOScale(new Vector3(target, target, target), 0.2f));

                sequence.Play();
            }
            else {
                transform.localScale = Vector3.one * target;
            }
            
        }
    }

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
        Hidden = hiddenOnStart;

        if (openAtStart) {
            JumpToEndOfAnimation();
        }
    }

    public void JumpToEndOfAnimation()
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool("open", true);
            animator.Play("REopening", 0, 1);  //0 is the layer, 1 is the normalized time. 
        }
    }

    // This method is called whenever a script is loaded or a value is changed in the Inspector.
    private void OnValidate()
    {
        Crossable = crossable; 
        Hidden = hidden;
    }

    private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        navMeshSourceTag = GetComponentInChildren<NavMeshSourceTag>();
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
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("REopened"))
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
