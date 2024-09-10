using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventChannel), true)]
public class EventChannelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EventChannel eventChannel = (EventChannel)target;
        if (GUILayout.Button("Raise Event"))
        {
            eventChannel.RaiseEvent();
        }
    }
}
