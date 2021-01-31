using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ChildBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip foundClip;
    [SerializeField] AudioClip deathClip;
    private AudioSource source;

    [SerializeField] Animator animator;
    [SerializeField] Transform model;

    private NavMeshAgent agent;
    Transform target;
    Vector3 previousFramePosition;

    Vector3 offset;

    public bool found { get; private set; }

    public UnityEvent onFound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetFound(string foundBy)
    {
        found = true;
        if(foundBy == "Enemy")
        {
            source.PlayOneShot(deathClip);
            transform.Rotate(new Vector3(90, 0, 0));
        }
        else
        {
            source.PlayOneShot(foundClip);
            onFound?.Invoke();
            target = GameManager.player.model;

            offset = Vector3.back * (GameManager.childrenFound / 3 + 1) * 1.2f + Vector3.left * (GameManager.childrenFound % 3 - 2) * 1f;
        }
    }

    private void Update()
    {
        if(target != null)
            agent.SetDestination(target.position + target.rotation * offset);

        Vector3 frameDeltaPos = transform.position - previousFramePosition;
        float distance = frameDeltaPos.magnitude;
        bool isMoving = distance > 0.1f * Time.fixedDeltaTime;

        animator.SetBool("Run bool", isMoving);

        previousFramePosition = transform.position;

        if (distance > 0.5f * Time.fixedDeltaTime)
        {
            model.rotation = Quaternion.LookRotation(frameDeltaPos, Vector3.up);
        }
    }

    public void SetTarget(Transform myTarget)
    {
        target = myTarget;
    }

    public void RotateToFacePlayer()
    {
        Transform player = GameManager.player.transform;
        Vector3 towardsEntity = player.position - model.position;
        model.rotation = Quaternion.LookRotation(towardsEntity, Vector3.up);
    }
}
