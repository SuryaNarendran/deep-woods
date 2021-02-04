using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text speakerNameText;
    [SerializeField] AudioClip textClip;

    bool isUp;

    private Animator animator;

    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PopUp()
    {
        if (!isUp)
        {
            animator.Play("Up");
            isUp = true;
        }
    }

    public void GoDown()
    {
        if (isUp)
        {
            animator.Play("Down");
            isUp = false;
        }
    }

    public void DisplayText(string textContent)
    {
        text.text = textContent;
    }

    public void DisplayText(DialogueScreen content)
    {
        text.text = content.text;
        speakerNameText.text = content.name;
    }

    public void PlayTextSound()
    {
        audioSource.PlayOneShot(textClip);
    }

    


}
