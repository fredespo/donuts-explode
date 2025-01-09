using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataStorage))]
public class DataStorageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataStorage dataStorage = (DataStorage)target;
        if (GUILayout.Button("Revoke Infinite Lives"))
        {
            dataStorage.RevokeInfiniteLives();
        }
    }
}