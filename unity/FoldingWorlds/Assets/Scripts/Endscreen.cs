using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnTrigger : MonoBehaviour
{
    public GameObject textCanvas;
    public string displayText = "This is the displayed text.";
    private Text textComponent;

    private void Start()
    {
        textComponent = textCanvas.GetComponentInChildren<Text>();
        textComponent.text = displayText;
        textCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change the tag to match the object that will trigger the text.
        {
            textCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Change the tag to match the object that will trigger the text.
        {
            textCanvas.SetActive(false);
        }
    }
}

