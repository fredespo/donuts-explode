﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class textTimer : MonoBehaviour
{
    private Detonator detonator;
    private BombDefuzer defuzer;
    private Bomb bomb;
    public GameObject gameOverUI;
    public PieceShooter pieceShooter;
    public GameObject pauseButton;
    public Animator camAnim;
    public float bombDetonationDelay = 0.1f;
    public float seconds = 10;
    public float countDownFastPerSecond = 4.0f;
    private float startSeconds;
    public GameObject shootTapZone;
    public AudioSource music;
    private Text text;
    public float minMusicPitch = 1.0f;
    public float maxMusicPitch = 2.0f;
    [SerializeField] public AnimationCurve musicPitchCurve;
    public float currCurveVal;
    public float slowMoTimeScale = 0.2f;
    private bool countingDownFast = false;
    private bool paused = false;
    private bool slowMo = false;
    public float slowMoMusicPitch = 0.5f;
    private bool fading;
    private Action onDoneCountingDownFast;

    void Start()
    {
        text = GetComponent<Text>();
        RefreshText();
    }

    public void Init(Detonator detonator, BombDefuzer defuzer, Bomb bomb)
    {
        this.detonator = detonator;
        this.defuzer = defuzer;
        this.bomb = bomb;
        paused = false;
        this.slowMo = false;
        this.fading = false;
        this.pauseButton.SetActive(true);
    }

    void Update()
    {
        HandleSlowMo();
        HandleFading();

        if(countingDownFast)
        {
            float timeChange = countDownFastPerSecond * Time.deltaTime;
            if (seconds < timeChange)
            {
                seconds = 0.0f;
                this.fading = true;
                countingDownFast = false;
                this.onDoneCountingDownFast.Invoke();
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
            if (seconds < -bombDetonationDelay)
            {
                DetonateBomb();
            }
        }
        RefreshText();
    }

    private void DetonateBomb()
    {
        if(this.paused)
        {
            return;
        }

        shootTapZone.SetActive(false);
        detonator.activate();
        music.Pause();
        gameOverUI.SetActive(true);
        this.pieceShooter.gameObject.SetActive(false);
        gameObject.SetActive(false);
        this.pieceShooter.DestroySpawnedPiece();
        this.pauseButton.SetActive(false);
    }

    private void HandleFading()
    {
        if(this.fading && this.GetAlpha() > 0f)
        {
            this.SetAlpha(this.GetAlpha() - 0.014f);
        }

        if(this.text != null && !this.fading)
        {
            this.SetAlpha(1.0f);
        }
    }

    private float GetAlpha()
    {
        return this.text.color.a;
    }

    private void SetAlpha(float alpha)
    {
        Color color = this.text.color;
        color = new Color(color.r, color.g, color.b, alpha);
        this.text.color = color;
    }

    private void HandleSlowMo()
    {
        
        if(!this.slowMo && ShouldEnterSlowMo())
        {
            this.slowMo = true;
            Time.timeScale = this.slowMoTimeScale;
            this.camAnim.SetBool("slowmo", this.slowMo);
        }
        
        if(this.slowMo && shouldExitSlowMo())
        {
            this.slowMo = false;
            Time.timeScale = 1.0f;
            this.camAnim.SetBool("slowmo", this.slowMo);
        }
    }

    private bool ShouldEnterSlowMo()
    {
        return this.seconds < 1.0f && this.seconds > 0
            && this.defuzer != null
            && this.defuzer.GetNumHolesLeft() == 1
            && this.defuzer.GetNumUnfilledHoles() == 1
            && this.pieceShooter != null
            && !this.pieceShooter.IsSpawnedPieceReadyToShoot()
            && this.pieceShooter.GetPiece() != null
            && !this.pieceShooter.GetPiece().GetComponent<BombPiece>().IsCaughtInMagnet()
            && this.bomb.PieceWillGoInHole(this.pieceShooter.GetPiece(), this.defuzer.GetHoles()[0].gameObject, this.seconds + this.bombDetonationDelay);
    }

    private bool shouldExitSlowMo()
    {
        if(this.defuzer == null || this.pieceShooter == null || this.pieceShooter.GetPiece() == null)
        {
            return false;
        }

        return this.seconds <= 0
            || this.defuzer.IsDefuzed()
            || this.pieceShooter.GetPiece().GetComponent<BombPiece>().IsCaughtInMagnet()
            || this.pieceShooter.IsSpawnedPieceReadyToShoot();
    }

    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
        if(this.seconds <= 0)
        {
            DetonateBomb();
        }
    }

    public void CountDownFastAndThen(Action andThen)
    {
        countingDownFast = true;
        this.onDoneCountingDownFast = andThen;
    }

    private void RefreshText()
    {
        if(text != null)
        {
            float secToDisplay = Mathf.Max(0f, this.seconds);
            text.text = secToDisplay.ToString("00.00").Replace(".", ":");
        }
    }


    private void AdjustMusic()
    {
        float timeElapsed = GetTimeElapsed();
        currCurveVal = musicPitchCurve.Evaluate(timeElapsed / startSeconds);
        music.pitch = this.slowMo ? this.slowMoMusicPitch : minMusicPitch + ((maxMusicPitch - minMusicPitch) * currCurveVal);
    }

    public float GetTimeElapsed()
    {
        return startSeconds - seconds;
    }

    public float GetStartSeconds()
    {
        return this.startSeconds;
    }

    public float GetSecondsLeft()
    {
        return seconds - bombDetonationDelay;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    public void setTime(float seconds)
    {
        this.seconds = seconds;
        this.startSeconds = seconds;
        RefreshText();
    }
}
