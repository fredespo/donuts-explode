using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject pauseBtn;
    public AudioSource music;

    public void ShowTitleScreen()
    {
        gameScreen.SetActive(false);
        titleScreen.SetActive(true);
        pauseBtn.SetActive(false);
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }
    }

    public void ShowGameScreen()
    {
        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
        pauseBtn.SetActive(true);
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }
    }
}
