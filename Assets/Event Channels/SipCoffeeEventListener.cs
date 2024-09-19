using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SipCoffeeEventListener : MonoBehaviour
{
    [SerializeField] private SipCoffeeEventChannel eventChannel = default;
    public UnityEvent<SipCoffeeEvent> OnEventRaised;

    public void OnEnable()
    {
        if (eventChannel != null)
        {
            eventChannel.OnEventRaised += Respond;
        }
    }

    public void OnDisable()
    {
        if (eventChannel != null)
        {
            eventChannel.OnEventRaised -= Respond;
        }
    }

    public void Respond(SipCoffeeEvent sipCoffeeEvent)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(sipCoffeeEvent);
        }
    }
}
