using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavmeshSafety : MonoBehaviour
{
    private NavMeshAgent agent;

    public float sampleRadius = 1.5f;  // The radius within which to search for a nearby point on the NavMesh
    public float allowedOffMeshDistance = 1f;  // Allowed distance to be off the NavMesh

    public bool isOnNavmesh;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();

        while (true)
        {
            isOnNavmesh = agent.isOnNavmesh;
            if (!agent.isOnNavMesh)
            {
                Debug.Log("Agent is off the NavMesh. Attempting to reposition...");

                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, sampleRadius, NavMesh.AllAreas))
                {
                    agent.Warp(hit.position);
                    Debug.Log("Agent successfully repositioned.");
                }
                else
                {
                    Debug.LogWarning("Failed to find a point on the NavMesh within the specified radius.");
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }


}
