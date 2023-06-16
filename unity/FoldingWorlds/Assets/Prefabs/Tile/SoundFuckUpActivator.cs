using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFuckUpActivator : MonoBehaviour
{

    public List<Tile> tiles;

    public bool targetVisibility;
    public float delay = 0f;
    public float stagger = 0.25f;

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
        Gizmos.color = !targetVisibility ? Color.red : Color.green;
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        foreach (Tile tile in tiles)
        {
            if (tile != null)
            {
                Gizmos.DrawLine(transform.position, tile.transform.position);
            }
        }
    }

}
