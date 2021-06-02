using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public TitleMenu titleMenu;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        this.slider = gameObject.GetComponent<Slider>();
        if(this.slider != null && this.titleMenu != null)
        {
            this.slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
    }

    public void ValueChangeCheck()
    {
        this.titleMenu.ForceLevel((int)this.slider.value);
    }
}
