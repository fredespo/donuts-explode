using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelIndicator : MonoBehaviour
{
    private AudioSource soundEffect;
    [SerializeField] private VoidEventChannel bonusLevelStartedChannel;

    void Start()
    {
        this.soundEffect = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        this.soundEffect.Play();
    }

    public void DoneAnimating()
    {
        gameObject.SetActive(false);
        this.bonusLevelStartedChannel.RaiseEvent();
    }
}
