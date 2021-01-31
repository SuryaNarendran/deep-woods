using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [TextArea]
    public string[] texts;

    private bool dialogueActive;

    private int currentIndex = 0;

    private bool objectEnabled = true;

    public UnityEvent onDialogueComplete;

    public UnityEvent onDialogueStart;

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player" && objectEnabled)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        GameManager.SetPlayerMovement(false);
        dialogueActive = true;
        currentIndex = 0;
        onDialogueStart?.Invoke();
        GameManager.textBox.PopUp();
        GameManager.textBox.DisplayText(texts[currentIndex]);
    }

    private void Update()
    {
        if (dialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Return) ||
                Input.GetMouseButtonDown(0) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.textBox.PlayTextSound();

                currentIndex++;
                if(currentIndex >= texts.Length)
                {
                    dialogueActive = false;
                    currentIndex = 0;
                    GameManager.textBox.GoDown();
                    GameManager.SetPlayerMovement(true);
                    objectEnabled = false;
                    onDialogueComplete?.Invoke();
                }
                else
                {
                    GameManager.textBox.DisplayText(texts[currentIndex]);
                }
            }
        }
    }


}
