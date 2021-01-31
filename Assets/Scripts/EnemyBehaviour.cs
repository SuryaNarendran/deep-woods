using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    Transform target;
    Vector3 previousFramePosition;

    [SerializeField] Animator animator;
    [SerializeField] Transform model;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(target == null)
        {
            FindNewTarget();
        }

        Vector3 frameDeltaPos = transform.position - previousFramePosition;
        float distance = frameDeltaPos.magnitude;
        bool isMoving = distance > 0.1f * Time.fixedDeltaTime;

        animator.SetBool("Run bool", isMoving);
        
        previousFramePosition = transform.position;

        if (distance > 0.05 * Time.fixedDeltaTime)
        {
            model.transform.rotation = Quaternion.LookRotation(frameDeltaPos, Vector3.up);
        }
    }

    private void FindNewTarget()
    {
        ChildBehaviour closest = null;
        foreach(ChildBehaviour child in GameManager.Children)
        {
            if (child.found) continue;

            if(closest == null)
            {
                closest = child;
                target = closest.transform;
                continue;
            }

            else if(Vector3.Distance(child.transform.position, transform.position) <= 
                Vector3.Distance(closest.transform.position, transform.position))
            {
                target = closest.transform;
                
            }
        }

        Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
        //Vector3 targetPosition = new Vector3(0, 0, 0);
        agent.SetDestination(targetPosition);
        Debug.Log("Chasing " + agent.destination);
    }

    public void ChildFound(ChildBehaviour childBehaviour)
    {
        FindNewTarget();
    }
}
