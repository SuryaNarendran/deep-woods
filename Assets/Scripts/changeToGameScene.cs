using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeToGameScene : MonoBehaviour
{
    public void ChangeScene()
    {
        SimpleSceneFader.ChangeSceneWithFade("mixcharwithforest");
    }
}
