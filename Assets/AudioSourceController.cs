using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour
{
    private AudioSource audio;

    void Start()
    {
        this.audio = GetComponent<AudioSource>();        
    }

    public void PlayIfNotPlaying()
    {
        if(!this.audio.isPlaying)
        {
            this.audio.Play();
        }
    }
}
