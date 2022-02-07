using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GamePauser : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject btn;
    public AudioSource music;
    private bool musicWasPlayingBeforePause;
    private float prevTimeScale;

    public void PauseGame()
    {
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        btn.SetActive(false);
        musicWasPlayingBeforePause = music.isPlaying;
        music.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = prevTimeScale;
        pauseMenu.SetActive(false);
        btn.SetActive(true);
        if (musicWasPlayingBeforePause)
        {
            music.Play();
        }
    }
}
