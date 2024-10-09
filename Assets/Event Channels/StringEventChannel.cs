using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StringEventChannel", menuName = "EventChannel/StringEventChannel", order = 0)]
public class StringEventChannel : EventChannel<string>
{
    public event UnityAction<string> OnEventRaised;

    public override void RaiseEvent(string stringEvent)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(stringEvent);
        }
    }
}
