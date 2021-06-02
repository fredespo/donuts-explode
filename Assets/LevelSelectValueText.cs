using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectValueText : MonoBehaviour
{
    public Slider levelSelectSlider;
    private Text text;

    public void Start()
    {
        this.text = gameObject.GetComponent<Text>();
    }

    public void Update()
    {
        if(this.levelSelectSlider != null)
        {
            this.text.text = this.levelSelectSlider.value.ToString();
        }
    }
}
