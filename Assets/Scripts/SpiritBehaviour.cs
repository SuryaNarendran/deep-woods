using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;

    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Chase()
    {
        agent.destination = target.position;
    }

    public void SetChildAsTarget()
    {
        foreach(ChildBehaviour child in GameManager.Children)
        {
            if (child.gameObject.activeInHierarchy && child.found == false)
            {
                SetTarget(child.transform);
                child.onFound.AddListener(Disappear);
            }
        }
    }

    public void Disappear()
    {
        animator.Play("Disappear");
    }

    public void Descend()
    {
        animator = GetComponent<Animator>();
        animator.Play("Descend");
    }

    public void ChasePosition(Vector3 position)
    {
        agent.destination = position;
    }
}
