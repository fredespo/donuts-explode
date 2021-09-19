using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioSource : MonoBehaviour
{
    public float duration;
    public float targetVolume;

    public void StartFadeCoroutine()
    {
        StartCoroutine("StartFade");
    }

    public IEnumerator StartFade()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < this.duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, this.targetVolume, currentTime / this.duration);
            yield return null;
        }
        yield break;
    }
}
