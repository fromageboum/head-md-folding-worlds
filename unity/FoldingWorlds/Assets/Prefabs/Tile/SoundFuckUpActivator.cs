using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFuckUpActivator : MonoBehaviour
{

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("on trigger enter");

        if (other.gameObject.tag == "Player")
        {
            GranularSynth.instance.FuckUpSound();
            col.enabled = false;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);


    }

}
