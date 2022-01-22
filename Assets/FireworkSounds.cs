using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FireworkSounds : MonoBehaviour
{
    public AudioClip audioShot;
    public AudioClip audioExplosion;
    public AudioMixerGroup mixer;
    public float explosionMinVolume = 0.3f;
    public float explosionMaxVolume = 0.7f;
    public float explosionPitchMin = 0.75f;
    public float explosionPitchMax = 1.25f;
    public float shootMinVolume = 0.05f;
    public float shootMaxVolume = 0.1f;
    public float shootPitchMin = 0.75f;
    public float shootPitchMax = 1.25f;
    private bool playedExplosion;
    private bool playedShot;

    void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
        int length = GetComponent<ParticleSystem>().GetParticles(particles);
        for(int i = 0; i < length; ++i)
        {
            if (this.shootMaxVolume > 0 && !this.playedShot && particles[i].remainingLifetime >= particles[i].startLifetime - Time.deltaTime)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = this.audioShot;
                audioSource.volume = UnityEngine.Random.Range(shootMinVolume, shootMaxVolume);
                audioSource.pitch = UnityEngine.Random.Range(shootPitchMin, shootPitchMax);
                audioSource.outputAudioMixerGroup = this.mixer;
                audioSource.Play();
                this.playedShot = true;
            }

            if (!this.playedExplosion && particles[i].remainingLifetime < Time.deltaTime)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = audioExplosion;
                audioSource.volume = UnityEngine.Random.Range(explosionMinVolume, explosionMaxVolume);
                audioSource.pitch = UnityEngine.Random.Range(explosionPitchMin, explosionPitchMax);
                audioSource.outputAudioMixerGroup = this.mixer;
                audioSource.Play();
                this.playedExplosion = true;
            }
        }
    }
}
