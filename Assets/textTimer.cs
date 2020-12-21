using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTimer : MonoBehaviour
{
    private Detonator detonator;
    public GameObject gameOverUI;
    public float gameOverDelaySec;
    public float seconds = 10;
    public float countDownFastPerSecond = 4.0f;
    private float startSeconds;
    public GameObject pauseButton;
    public GameObject shootTapZone;
    public GameObject tutorialText;
    public AudioSource music;
    private Text text;
    public float minMusicPitch = 1.0f;
    public float maxMusicPitch = 2.0f;
    [SerializeField] public AnimationCurve musicPitchCurve;
    public float currCurveVal;
    private bool countingDownFast = false;
    private bool paused = false;

    void Start()
    {
        text = GetComponent<Text>();
        RefreshText();
        startSeconds = seconds;
    }

    public void Init(Detonator detonator)
    {
        this.detonator = detonator;
        paused = false;
    }

    void Update()
    {
        if(seconds <= 0)
        {
            return;
        }

        if(countingDownFast)
        {
            float timeChange = countDownFastPerSecond * Time.deltaTime;
            if (seconds < timeChange)
            {
                seconds = 0.0f;
                countingDownFast = false;
                foreach (Transform child in GameObject.FindGameObjectWithTag("WinUI").transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                seconds -= timeChange;
            }
        }
        else if(!paused)
        {
            seconds -= Time.deltaTime;
            AdjustMusic();
            if (seconds < 0)
            {
                seconds = 0;
                pauseButton.SetActive(false);
                shootTapZone.SetActive(false);
                detonator.activate();
                tutorialText.SetActive(false);
                music.Pause();
                gameOverUI.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        RefreshText();
    }

    public void Pause()
    {
        paused = true;
    }

    public void CountDownFast()
    {
        countingDownFast = true;
    }

    private void RefreshText()
    {
        if(text != null)
        {
            text.text = seconds.ToString("00.00").Replace(".", ":");
        }
    }


    private void AdjustMusic()
    {
        float timeElapsed = startSeconds - seconds;
        currCurveVal = musicPitchCurve.Evaluate(timeElapsed / startSeconds);
        music.pitch = minMusicPitch + ((maxMusicPitch - minMusicPitch) * currCurveVal);
    }

    public float GetSecondsLeft()
    {
        return seconds;
    }

    public void setTime(float seconds)
    {
        this.seconds = seconds;
        RefreshText();
    }
}
