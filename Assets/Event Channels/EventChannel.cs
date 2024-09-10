using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel : ScriptableObject
{
    public abstract void RaiseEvent();
}
