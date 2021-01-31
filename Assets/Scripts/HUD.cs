using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text childrenFoundText;
    [SerializeField] Text childrenLostText;
    [SerializeField] Text childrenLeftText;

    private AudioSource source;
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void UpdateUI()
    {
        childrenFoundText.text = GameManager.childrenFound.ToString();
        childrenLostText.text = GameManager.childrenLost.ToString();
        childrenLeftText.text =
            (GameManager.totalChildren -
            GameManager.childrenFound -
            GameManager.childrenLost).ToString();
    }

    public void PlayUISound()
    {
        source.PlayOneShot(clip);
    }
}
