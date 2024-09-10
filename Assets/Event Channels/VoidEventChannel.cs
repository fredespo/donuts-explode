using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "EventChannel/VoidEventChannel", order = 0)]
public class VoidEventChannel : EventChannel
{
    public event UnityAction OnEventRaised;

    public override void RaiseEvent()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised();
        }
    }
}
