using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTimer : MonoBehaviour
{
    private Detonator detonator;
    private BombDefuzer defuzer;
    public GameObject gameOverUI;
    public PieceShooter pieceShooter;
    public float gameOverDelaySec;
    public float seconds = 10;
    public float countDownFastPerSecond = 4.0f;
    private float startSeconds;
    public GameObject shootTapZone;
    public AudioSource music;
    public Ads ads;
    private Text text;
    public float minMusicPitch = 1.0f;
    public float maxMusicPitch = 2.0f;
    [SerializeField] public AnimationCurve musicPitchCurve;
    public float currCurveVal;
    public float slowMoTimeScale = 0.2f;
    private bool countingDownFast = false;
    private bool paused = false;
    private bool slowMo = false;

    void Start()
    {
        text = GetComponent<Text>();
        RefreshText();
        startSeconds = seconds;
    }

    public void Init(Detonator detonator, BombDefuzer defuzer)
    {
        this.detonator = detonator;
        this.defuzer = defuzer;
        paused = false;
    }

    void Update()
    {
        if (seconds < 1 && seconds > 0 && !paused && !countingDownFast && !this.pieceShooter.IsSpawnedPieceReadyToShoot() && this.defuzer.GetNumHolesLeft() == 1)
        {
            this.slowMo = true;
        }

        if(paused || countingDownFast)
        {
            this.slowMo = false;
        }

        if (this.slowMo)
        {
            Time.timeScale = this.slowMoTimeScale;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        if (seconds <= 0)
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
                ads.ShowInterstitialAd();
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
                shootTapZone.SetActive(false);
                detonator.activate();
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

    public void UnPause()
    {
        paused = false;
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
