using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedEnding : MonoBehaviour
{
    public void TimedText()
    {
        Timer.Register(7,ShowText);
    }

    public void ShowText()
    {
        GameManager.textBox.PopUp();
        GameManager.textBox.DisplayText("Hey, what are you still doing here?? The show's over. No fancy credits or anything. Shoo.");
    }
}
