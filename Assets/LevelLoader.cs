using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject canvas;
    private ScreenManager screenManager;
    public AudioSource music;
    public textTimer timer;
    public GameObject bombPieces;
    public GameObject pieceShooter;
    public GameObject pauseButton;
    public GameObject shootTapZone;
    public GameObject gameOverUI;
    public GameObject pausedUI;
    public GamePauser gamePauser;
    public GameObject gameWonUI;
    public GameObject tutorialTextContainer;
    private GameObject tutorialText;
    public List<Level> levels;
    private int currLevel;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectsWithTag("ScreenManager")[0].GetComponent<ScreenManager>();
    }

    public void LoadLevel(int levelIndex)
    {
        currLevel = levelIndex;
        RestartCurrentLevel();
    }

    public void RestartCurrentLevel()
    {
        Level level = levels[currLevel];
        foreach(GameObject prevBomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            Destroy(prevBomb);
        }
        GameObject bomb = Instantiate(level.bomb);
        bomb.transform.SetParent(canvas.transform, false);
        timer.gameObject.SetActive(true);
        timer.gameObject.GetComponent<textTimer>().enabled = true;
        timer.setTime(level.secondsOnTimer);
        foreach(Transform child in bombPieces.transform)
        {
            Destroy(child.gameObject);
        }
        bombPieces.SetActive(true);
        pieceShooter.SetActive(true);
        pieceShooter.GetComponent<PieceShooter>().Init();
        pauseButton.SetActive(true);
        shootTapZone.SetActive(true);
        gameOverUI.SetActive(false);
        foreach (Transform child in gameWonUI.transform)
        {
            child.gameObject.SetActive(false);
        }
        pausedUI.SetActive(false);
        gamePauser.CancelQuit();
        gamePauser.ResumeGame();
        if (!music.isPlaying)
        {
            music.pitch = 1.0f;
            music.Play(0);
        }

        if(tutorialText != null)
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
            RestartCurrentLevel();
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
