using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "VolumeSetting", menuName = "Audio/VolumeSetting")]
public class VolumeSetting : ScriptableObject
{
    public AudioMixer mixer;
    public string mixerParam;
    public float offsetInDecibels = 0f;

    public void setPct(float val) {
        this.mixer.SetFloat(this.mixerParam, (Mathf.Log10(val) * 20) + this.offsetInDecibels);
    }
}
