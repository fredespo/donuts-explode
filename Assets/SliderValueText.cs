using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    public Slider slider;
    public bool pct;

    private Text text;
    private float sliderMin;
    private float sliderMax;

    public void Start()
    {
        this.text = gameObject.GetComponent<Text>();
        this.sliderMin = this.slider.minValue;
        this.sliderMax = this.slider.maxValue;
    }

    public void Update()
    {
        if(this.slider != null)
        {
            this.text.text = this.pct ? ((int)SliderPctVal()).ToString() + "%" : this.slider.value.ToString();
        }
    }

    private float SliderPctVal()
    {
        float val = this.slider.value;
        return (val - this.sliderMin) / (this.sliderMax - this.sliderMin) * 100;
    }
}
