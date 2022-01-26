using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameWonExitButton : MonoBehaviour
{
    public GameWonScreen screen;
    public UnityEvent onExit;
    public UnityEvent confirmScoreErase;

    public void click()
    {
        if(screen.IsScoreSaved())
        {
            onExit.Invoke();
        }
        else
        {
            confirmScoreErase.Invoke();
        }
    }
}
