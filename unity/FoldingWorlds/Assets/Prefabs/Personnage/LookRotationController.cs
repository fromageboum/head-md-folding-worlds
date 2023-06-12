using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class LookRotationController : MonoBehaviour
{
    public float rotationSpeed = 5f; // Control the speed of rotation.

    private NavMeshAgent agent;

    public Transform target; 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void LateUpdate()
    {
        /*
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon) // if the agent is moving
        {
            Quaternion lookRotation = Quaternion.LookRotation(agent.desiredVelocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }*/
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero) // if the agent needs to rotate
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}