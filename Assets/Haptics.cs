using UnityEngine;
using Lofelt.NiceVibrations;

public class Haptics : MonoBehaviour
{
    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    public void PlayEmphasis()
    {
        HapticPatterns.PlayEmphasis(1.0f, 0.5f);
    }

    public void PlayConstant()
    {
        HapticPatterns.PlayConstant(1.0f, 0.5f, 1.0f);
    }

    public void PlayWarningPreset()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
    }
}
