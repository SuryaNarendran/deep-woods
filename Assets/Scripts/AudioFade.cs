using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] float speed;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }

    private IEnumerator FadingOut()
    {
        while(source.volume > 0)
        {
            source.volume -= speed * Time.deltaTime;
            yield return null;
        }
    }
}
