using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringEventListener : MonoBehaviour
{
    [SerializeField] private StringEventChannel eventChannel = default;
    public UnityEvent<string> OnEventRaised;

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

    public void Respond(string stringEvent)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(stringEvent);
        }
    }
}
