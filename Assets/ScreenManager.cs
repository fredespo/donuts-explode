using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject gameScreen;
    public AudioSource music;

    public void ShowTitleScreen()
    {
        gameScreen.SetActive(false);
        titleScreen.SetActive(true);
        if(!music.isPlaying)
        {
            music.Play(0);
        }
    }

    public void ShowGameScreen()
    {
        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
        if (!music.isPlaying)
        {
            music.Play(0);
        }
    }
}
