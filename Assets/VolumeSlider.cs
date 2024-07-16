using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public VolumeSetting volumeSetting;
    private Slider slider;

    void Start()
    {
        this.slider = gameObject.GetComponent<Slider>();
        this.slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    public void ValueChanged()
    {
        volumeSetting.setPct(this.slider.value);
    }
}
