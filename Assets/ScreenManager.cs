﻿using System.Collections;
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
    public GameObject title;
    public GameObject gameScreen;
    public GameStarter gameStarter;
    public AudioSource music;

    public void ShowTitleScreen()
    {
        gameScreen.SetActive(false);
        titleScreen.SetActive(true);
        titleScreenBomb.SetActive(true);
        startBtn.GetComponent<Image>().enabled = true;
        startBtn.GetComponent<EventTrigger>().enabled = true;
        startBtnText.GetComponent<Text>().enabled = true;
        title.SetActive(true);
        gameStarter.Init();
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
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }
    }
}
