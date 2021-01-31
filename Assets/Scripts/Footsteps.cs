using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float gapTime;

    private AudioSource source;

    private Timer footstepTimer;

    private bool _running;
    public bool running
    {
        get => _running;
        set
        {
            if (_running == false && value == true) Footstep();
            else if (_running = true && value == false) CancelFootstep();
            _running = value;
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Footstep()
    {
        source.PlayOneShot(clip);
        footstepTimer = Timer.Register(gapTime, Footstep);
    }

    private void CancelFootstep()
    {
        if (footstepTimer != null)
            footstepTimer.Cancel();
    }
}
