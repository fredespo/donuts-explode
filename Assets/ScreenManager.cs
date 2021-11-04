using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject titleScreenBomb;
    public GameObject startBtn;
    public GameObject startBtnText;
    public GameObject newGameBtn;
    public GameObject newGameBtnText;
    public GameObject title;
    public GameObject gameScreen;
    public TitleMenu titleMenu;
    public GameObject removeAdsButton;
    public AudioSource music;
    public GameObject gameWonScreen;
    public GameObject settingsScreen;

    public void ShowTitleScreen()
    {
        gameScreen.SetActive(false);
        gameWonScreen.SetActive(false);
        settingsScreen.SetActive(false);
        titleScreen.SetActive(true);
        titleScreenBomb.SetActive(true);
        startBtn.GetComponent<Image>().enabled = true;
        startBtn.GetComponent<EventTrigger>().enabled = true;
        newGameBtn.GetComponent<Image>().enabled = true;
        newGameBtn.GetComponent<EventTrigger>().enabled = true;
        startBtnText.GetComponent<Text>().enabled = true;
        newGameBtnText.GetComponent<Text>().enabled = true;
        title.SetActive(true);
        removeAdsButton.SetActive(true);
        titleMenu.Init();
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }
    }

    public void ShowSettingsScreen()
    {
        gameScreen.SetActive(false);
        gameWonScreen.SetActive(false);
        titleScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void ShowGameScreen()
    {
        titleScreen.SetActive(false);
        gameScreen.SetActive(true);
        gameWonScreen.SetActive(false);
        settingsScreen.SetActive(false);
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }
    }

    public void ShowGameWonScreen(bool showAnimation = true)
    {
        gameWonScreen.SetActive(true);
        titleScreen.SetActive(false);
        gameScreen.SetActive(false);
        settingsScreen.SetActive(false);
        gameWonScreen.GetComponent<GameWonScreen>().Init(showAnimation);
    }
}
