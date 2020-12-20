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

    void Start()
    {
        text = GetComponent<Text>();
        RefreshText();
        startSeconds = seconds;
    }

    public void Init(Detonator detonator)
    {
        this.detonator = detonator;
    }

    void Update()
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
        RefreshText();
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
