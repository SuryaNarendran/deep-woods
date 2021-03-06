﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [TextArea]
    public string[] texts;

    public DialogueScreen[] dialogueScreens;

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
        //GameManager.textBox.DisplayText(texts[currentIndex]);
        GameManager.textBox.DisplayText(dialogueScreens[currentIndex]);
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
                    //GameManager.textBox.DisplayText(texts[currentIndex]);
                    GameManager.textBox.DisplayText(dialogueScreens[currentIndex]);
                }
            }
        }
    }


}
[System.Serializable]
public class DialogueScreen
{
    [TextArea]
    public string text;
    public string name = "You";
    public Color color = new Color(201,201,201,141);
}
