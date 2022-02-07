using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticsSetting : MonoBehaviour
{
    public Toggle toggle;
    public Animator anim;
    public DataStorage dataStorage;

    void Start()
    {
        if (this.dataStorage != null)
        {
            this.toggle.isOn = this.dataStorage.GetHapticSetting();
            Init();
        }
    }

    void OnEnable()
    {
        Init(); 
    }

    private void Init()
    {
        if (!this.toggle.isOn)
        {
            this.anim.SetTrigger("initOff");
        }
    }

    public void ResetToDefault()
    {
        if(!this.toggle.isOn)
        {
            this.anim.SetTrigger("toggle");
        }
        this.toggle.isOn = true;
        Taptic.tapticOn = this.toggle.isOn;
    }

    public void Save()
    {
        if (this.dataStorage != null)
        {
            this.dataStorage.SaveHapticSetting(this.toggle.isOn);
        }
    }

    public void Toggle()
    {
        this.toggle.isOn = !this.toggle.isOn;
        this.anim.SetTrigger("toggle");
        Taptic.tapticOn = this.toggle.isOn;
        Taptic.Heavy();
    }
}
