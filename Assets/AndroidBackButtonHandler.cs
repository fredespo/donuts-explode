using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AndroidBackButtonHandler : MonoBehaviour
{
    public UnityEvent onBackButtonPressed;

    void Update()
    {
        if (BackButtonWasPressedThisFrame())
        {
            onBackButtonPressed.Invoke();
        }
    }

    public void MinimizeApp()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }

    private bool BackButtonWasPressedThisFrame()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
