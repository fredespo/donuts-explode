using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    [SerializeField] private VoidEventChannel eventChannel = default;
    public UnityEvent OnEventRaised;

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

    public void Respond()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
