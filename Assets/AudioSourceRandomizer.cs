using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceRandomizer : MonoBehaviour
{
    public float pitchSpread;
    public float volumeSpread;

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(audio.pitch - this.pitchSpread, audio.pitch + this.pitchSpread);
        audio.volume = Random.Range(audio.volume - this.volumeSpread, audio.volume + this.volumeSpread);
    }
}
