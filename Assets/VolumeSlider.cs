using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerParam;
    private Slider slider;

    void Start()
    {
        this.slider = gameObject.GetComponent<Slider>();        
    }

    void Update()
    {
        mixer.SetFloat(mixerParam, Mathf.Log10(this.slider.value) * 20);
    }
}
