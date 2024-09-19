using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel : ScriptableObject
{
    public abstract void RaiseEvent();
}

public abstract class EventChannel<T> : ScriptableObject
{
    public abstract void RaiseEvent(T eventData);
}
