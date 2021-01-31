using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    private Text text;
    private Animator animator;

    Timer timer;

    private void Awake()
    {
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void ShowText(string textContent)
    {
        text.text = textContent;
        animator.Play("FadeIn");
        timer = Timer.Register(4, Hide);
    }

    public void Hide()
    {
        animator.Play("FadeOut");
    }
}
