﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameWonScreen : MonoBehaviour
{
    public UnityEvent onInit;
    public UnityEvent onPostInit;
    public UnityEvent onSkipAnimation;
    public UnityEvent onAutoSaveScore;
    public UnityEvent onFailedAutoSaveScore;
    public UnityEvent onScoreSaved;
    public UnityEvent onScoreSaveError;
    public UnityEvent onNewHighScoreDetected;
    public UnityEvent onExit;
    public Score score;
    public GameObject levelIndicator;
    public Text scoreText;
    public GooglePlayServices playServices;
    public AudioSource gameMusic;
    public AudioSource victoryMusic;
    public AudioSource metalImpactSound;
    public AudioSource extinguishSound;
    private float initGameMusicVolume;
    private float initVictoryMusicVolume;
    private bool savedScore;
    private Animator anim;

    void Awake()
    {
        this.initGameMusicVolume = this.gameMusic.volume;
        this.initVictoryMusicVolume = this.victoryMusic.volume;
        this.anim = GetComponent<Animator>();
    }

    public void Init(bool showAnimation = true)
    {
        StartCoroutine(InitCoroutine(showAnimation));
    }

    private IEnumerator InitCoroutine(bool showAnimation)
    {
        onInit.Invoke();
        scoreText.text = score.GetScore().ToString();
        this.savedScore = false;
        if (showAnimation)
        {
            this.anim.enabled = true;
            this.anim.SetTrigger("Intro");
            StartCoroutine(FadeAudioSource(this.gameMusic, 3, 0));
            this.victoryMusic.volume = 0;
            this.victoryMusic.Play();
            StartCoroutine(FadeAudioSource(this.victoryMusic, 3, this.initVictoryMusicVolume));
        }
        else
        {
            this.anim.enabled = false;
            this.gameMusic.Stop();
            this.victoryMusic.volume = this.initVictoryMusicVolume;
            this.victoryMusic.Play();
            this.onSkipAnimation.Invoke();
            while (!playServices.IsInitialized)
            {
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2f);
            playServices.SignIn((res) => PostInit());
        }
    }

    public void PostInit()
    {
        this.onPostInit.Invoke();
        if (playServices.IsSignedIn())
        {
            playServices.PostHighScoreAndThen(score.GetScore(), (success) =>
            {
                if (success)
                {
                    this.savedScore = true;
                    onAutoSaveScore.Invoke();
                    playServices.ProcessCurrentHighScore(highscore =>
                    {
                        if (score.GetScore() == highscore)
                        {
                            onNewHighScoreDetected.Invoke();
                        }
                    });
                }
                else
                {
                    onFailedAutoSaveScore.Invoke();
                }
            });
        }
        else
        {
            onFailedAutoSaveScore.Invoke();
        }
    }

    public void MakeLevelIndicatorBold()
    {
        this.levelIndicator.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void MakeLevelIndicatorNormal()
    {
        this.levelIndicator.GetComponent<Text>().fontStyle = FontStyle.Normal;
    }

    public void PostScore()
    {
        playServices.PostHighScoreAndThen(score.GetScore(), (success) =>
        {
            if (success)
            {
                this.savedScore = true;
                onScoreSaved.Invoke();
                playServices.ProcessCurrentHighScore(highscore =>
                {
                    if (score.GetScore() == highscore)
                    {
                        onNewHighScoreDetected.Invoke();
                    }
                });
            }
            else
            {
                Taptic.Failure();
                onScoreSaveError.Invoke();
            }
        });
    }

    public void OnExit()
    {
        this.victoryMusic.Stop();
        this.gameMusic.volume = this.initGameMusicVolume;
        this.gameMusic.Play();
        onExit.Invoke();
    }

    public bool IsScoreSaved()
    {
        return this.savedScore;
    }

    IEnumerator FadeAudioSource(AudioSource audio, float duration, float targetVolume)
    {
        //Calculate the steps
        int volumeChangesPerSecond = 10;
        int numSteps = (int)(volumeChangesPerSecond * duration);
        float stepTime = duration / numSteps;
        float stepSize = (targetVolume - audio.volume) / numSteps;

        //Fade now
        for (int i = 1; i < numSteps; i++)
        {
            audio.volume += stepSize;
            yield return new WaitForSeconds(stepTime);
        }

        //Make sure the targetVolume is set
        audio.volume = targetVolume;
    }

    public void PlayMetalImpactSound()
    {
        this.metalImpactSound.Play();
    }

    public void PlayExtinguishSound()
    {
        this.extinguishSound.Play();
    }
}
