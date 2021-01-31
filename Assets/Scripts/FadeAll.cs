using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAll : MonoBehaviour
{
    [SerializeField] Animator[] animators;

    public void FadeIn()
    {
        foreach(Animator animator in animators)
        {
            animator.Play("FadeIn");
        }
    }

    public void FadeOut()
    {
        foreach (Animator animator in animators)
        {
            animator.Play("FadeOut");
        }
    }
}
