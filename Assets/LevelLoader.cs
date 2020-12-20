﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject canvas;
    private ScreenManager screenManager;
    public GameMusic music;
    public textTimer timer;
    public GameObject bombPieces;
    public GameObject pieceShooter;
    public GameObject pauseButton;
    public GameObject shootTapZone;
    public GameObject gameOverUI;
    public GameObject pausedUI;
    public GamePauser gamePauser;
    public GameObject tutorialTextContainer;
    private GameObject tutorialText;
    public List<Level> levels;
    private int currLevel;
    private float startDelaySec;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectsWithTag("ScreenManager")[0].GetComponent<ScreenManager>();
    }

    public void LoadLevel(int levelIndex, float startDelaySec)
    {
        currLevel = levelIndex;
        ResetCurrentLevel();
        StartCoroutine(StartCurrentLevelAfterDelay(startDelaySec));
    }

    public void StartCurrentLevelAfterDelaySec(float delaySec)
    {
        this.startDelaySec = delaySec;
        StartCoroutine(StartCurrentLevelAfterDelay(delaySec));
    }

    public IEnumerator StartCurrentLevelAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        StartCurrentLevel();
    }

    public void StartCurrentLevel()
    {
        timer.enabled = true;
        pieceShooter.GetComponent<PieceShooter>().SetShootingEnabled(true);
        GameObject.FindGameObjectsWithTag("bomb")[0].GetComponent<Rotator>().enabled = true;
        GameObject.FindGameObjectsWithTag("BombFlare")[0].GetComponent<FuseFlareSpawner>().enabled = true;
        GameObject.FindGameObjectsWithTag("BombFuse")[0].GetComponent<Animator>().enabled = true;
    }

    public void ResetCurrentLevel()
    {
        Level level = levels[currLevel];
        foreach(GameObject prevBomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            Destroy(prevBomb);
        }
        GameObject bomb = Instantiate(level.bomb);
        bomb.transform.SetParent(canvas.transform, false);
        timer.Init(bomb.GetComponent<Detonator>());
        timer.gameObject.SetActive(true);
        timer.setTime(level.secondsOnTimer);
        timer.enabled = false;
        foreach (Transform child in bombPieces.transform)
        {
            Destroy(child.gameObject);
        }
        bombPieces.SetActive(true);
        pieceShooter.SetActive(true);
        pieceShooter.GetComponent<PieceShooter>().Init();
        if(this.startDelaySec > 0)
        {
            pieceShooter.GetComponent<PieceShooter>().SetShootingEnabled(false);
        }
        pauseButton.SetActive(true);
        shootTapZone.SetActive(true);
        gameOverUI.SetActive(false);
        pausedUI.SetActive(false);
        gamePauser.CancelQuit();
        gamePauser.ResumeGame();
        music.Reset();

        if (tutorialText != null)
        {
            Destroy(tutorialText);
        }
        if(level.tutorialText != null)
        {
            tutorialText = Instantiate(level.tutorialText);
            tutorialText.gameObject.transform.SetParent(tutorialTextContainer.gameObject.transform, false);
        }
        tutorialTextContainer.SetActive(true);
    }

    public void LoadNextLevel()
    {
        if(currLevel < levels.Count - 1)
        {
            ++currLevel;
            ResetCurrentLevel();
            StartCurrentLevelAfterDelaySec(0.0f);
        }
        else
        {
            screenManager.ShowTitleScreen();
        }
    }

    [System.Serializable]
    public class Level
    {
        public GameObject bomb;
        public float secondsOnTimer;
        public GameObject tutorialText;
    }

    [System.Serializable]
    public class GameObj
    {
        public GameObject gameObj;
    }
}
