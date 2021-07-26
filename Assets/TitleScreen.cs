using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject newGameConfirmationUI;

    public void MinimizeAppIfNotConfirmingNewGame()
    {
        if(!ConfirmingNewGame())
        {
            MinimizeApp();
        }
    }

    private bool ConfirmingNewGame()
    {
        return newGameConfirmationUI.activeInHierarchy;
    }

    private void MinimizeApp()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
    }
}
