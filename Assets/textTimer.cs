﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTimer : MonoBehaviour
{
    public Detonator detonator;
    public GameObject gameOverUI;
    public float gameOverDelaySec;
    public float seconds = 10;
    public GameObject pauseButton;
    public GameObject shootTapZone;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        seconds -= Time.deltaTime;
        if (seconds < 0)
        {
            seconds = 0;
            pauseButton.SetActive(false);
            shootTapZone.SetActive(false);
            detonator.activate();
            Destroy(gameObject);
            gameOverUI.SetActive(true);
        }
        text.text = seconds.ToString("00.00").Replace(".", ":");
    }

    public float GetSecondsLeft()
    {
        return seconds;
    }
}
