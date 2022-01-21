using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWizard : MonoBehaviour
{
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
