using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundFxVolumeSlider : MonoBehaviour
{
    public DataStorage dataStorage;
    private Slider slider;

    void Start()
    {
        this.slider = GetComponent<Slider>();
        if (this.dataStorage != null)
        {
            SetSliderValuePct(this.dataStorage.GetSoundFxVolumePct());
        }
    }

    public void SaveSoundFxVolume()
    {
        if (this.dataStorage != null)
        {
            this.dataStorage.SaveSoundFxVolumePct((int)SliderPctValue());
        }
    }

    public void ResetToDefault()
    {
        SetSliderValuePct(100);
    }

    private void SetSliderValuePct(int pct)
    {
        float range = this.slider.maxValue - this.slider.minValue;
        this.slider.value = this.slider.minValue + (range * (float)pct / 100);
    }

    private float SliderPctValue()
    {
        float val = this.slider.value;
        return (val - this.slider.minValue) / (this.slider.maxValue - this.slider.minValue) * 100;
    }
}
