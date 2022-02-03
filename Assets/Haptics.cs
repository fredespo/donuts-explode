using UnityEngine;
using Lofelt.NiceVibrations;
using System.Collections.Generic;
using System;

public class Haptics : MonoBehaviour
{
    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    public enum HapticType
    {
        Warning = 0,
        Failure = 1,
        Success = 2,
        Light = 3,
        Medium = 4,
        Heavy = 5,
        Default = 6,
        Vibrate = 7,
        Selection = 8
    }

    private static IDictionary<HapticType, Action> ActionPerHapticType = new Dictionary<HapticType, Action>
    {
        { HapticType.Warning, () => { Taptic.Warning();} },
        { HapticType.Failure, () => { Taptic.Failure();} },
        { HapticType.Success, () => { Taptic.Success();} },
        { HapticType.Light, () => { Taptic.Light();} },
        { HapticType.Medium, () => { Taptic.Medium();} },
        { HapticType.Heavy, () => { Taptic.Heavy();} },
        { HapticType.Default, () => { Taptic.Default();} },
        { HapticType.Vibrate, () => { Taptic.Vibrate();} },
        { HapticType.Selection, () => { Taptic.Selection();} },
    };

    public void TriggerHaptic(HapticType type)
    {
        ActionPerHapticType[type].Invoke();
    }

    public void TriggerHaptic(string type)
    {
        TriggerHaptic((HapticType)Enum.Parse(typeof(HapticType), type));
    }
}
