using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GamePauser : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button btn;
    public AudioSource music;
    private bool musicWasPlayingBeforePause;
    private float prevTimeScale;

    public void PauseGame()
    {
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        btn.enabled = false;
        musicWasPlayingBeforePause = music.isPlaying;
        music.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = prevTimeScale;
        pauseMenu.SetActive(false);
        btn.enabled = true;
        if (musicWasPlayingBeforePause)
        {
            music.Play();
        }
    }
}
