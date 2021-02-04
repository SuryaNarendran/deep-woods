using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedEnding : MonoBehaviour
{
    public void TimedEnd()
    {
        Timer.Register(4, SceneTransition);
    }

    public void SceneTransition()
    {
        SimpleSceneFader.ChangeSceneWithFade("Main Menu");
    }
}
