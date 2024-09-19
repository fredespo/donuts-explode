using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SipCoffeeEventChannel", menuName = "EventChannel/SipCoffeeEventChannel", order = 0)]
public class SipCoffeeEventChannel : EventChannel<SipCoffeeEvent>
{
    public event UnityAction<SipCoffeeEvent> OnEventRaised;

    public override void RaiseEvent(SipCoffeeEvent sipCoffeeEvent)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(sipCoffeeEvent);
        }
    }
}
