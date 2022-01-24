using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsScreen : MonoBehaviour
{
    public UnityEvent onBack;
    public UnityEvent onBackToTitle;
    public UnityEvent onBackToGame;
    private bool fromPauseMenu;

    public void Init(bool fromPauseMenu)
    {
        this.fromPauseMenu = fromPauseMenu;
        if(fromPauseMenu)
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Back()
    {
        this.onBack.Invoke();
        if (this.fromPauseMenu)
        {
            Time.timeScale = 0;
            this.onBackToGame.Invoke();
        }
        else
        {
            this.onBackToTitle.Invoke();
        }
    }
}
