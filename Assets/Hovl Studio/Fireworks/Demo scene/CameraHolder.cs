using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    //GUI
    float windowDpi;
    private GUIStyle screenGUI = new GUIStyle();
    public GameObject[] Prefabs;
    private int Prefab;
    private GameObject Instance;

    void Start ()
    {
        if (Screen.dpi < 1) windowDpi = 1;
        if (Screen.dpi < 200) windowDpi = 1;
        else windowDpi = Screen.dpi / 200f;
        screenGUI.fontSize = (int)(15f * windowDpi);
        screenGUI.normal.textColor = new Color(0.5f, 0f, 0f);
		Counter(0);
    }
	
    private void OnGUI()
    {
        if (GUI.Button(new Rect(5 * windowDpi, 5 * windowDpi, 110 * windowDpi, 30 * windowDpi), "Previous effect"))
        {
            Counter(-1);
        }
        if (GUI.Button(new Rect(120 * windowDpi, 5 * windowDpi, 110 * windowDpi, 30 * windowDpi), "Next effect"))
        {
            Counter(+1);
        }
    }

    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
        if (Instance != null) Destroy(Instance);
        {
            Instance = Instantiate(Prefabs[Prefab]);
        }
    }
}
