using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelIndicator : MonoBehaviour
{
    private AudioSource soundEffect;

    void Start()
    {
        this.soundEffect = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        this.soundEffect.Play();
    }
}
