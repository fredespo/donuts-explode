using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonLocationSetting : MonoBehaviour
{
    public DataStorage dataStorage;
    public TMPro.TMP_Dropdown dropdown;

    void Start()
    {
        this.dropdown.value = this.dropdown.options.FindIndex(option => option.text == this.dataStorage.GetPauseButtonLocation());
    }

    public void Save()
    {
        this.dataStorage.SavePauseButtonLocation(this.dropdown.options[this.dropdown.value].text);
    }

    public void ResetToDefault()
    {
        this.dropdown.value = this.dropdown.options.FindIndex(option => option.text == "Left");
    }
}
