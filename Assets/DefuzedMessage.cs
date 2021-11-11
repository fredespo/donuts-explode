using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefuzedMessage : MonoBehaviour
{
    public AudioSource extinguishSound;

    public void PlayExtinguishSound()
    {
        this.extinguishSound.Play();
    }
}
