using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
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
#endif
