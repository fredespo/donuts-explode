using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerParam;
    public float boost = 0f;
    private Slider slider;

    void Start()
    {
        this.slider = gameObject.GetComponent<Slider>();
        this.slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    public void ValueChanged()
    {
        SetVolume(this.slider.value);
        Taptic.Light();
    }

    private void SetVolume(float val)
    {
        mixer.SetFloat(mixerParam, Mathf.Log10(val) * 20 + this.boost);
    }
}
