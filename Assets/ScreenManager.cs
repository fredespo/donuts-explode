using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject gameScreen;

    public void ShowTitleScreen()
    {
        gameScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void ShowGameScreen()
    {
        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}
