using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class LevelLoader : MonoBehaviour
{
    public GameObject canvas;
    private ScreenManager screenManager;
    public GameMusic music;
    public textTimer timer;
    public GameObject bombPieces;
    public GameObject pieceShooter;
    public PieceTutorialAnimator pieceTutorialAnimator;
    public PieceShooter pieceShooterComp;
    public GameObject shootTapZone;
    public GameOverUI gameOverUI;
    public Score score;
    public GameObject levelObscurer;
    public LevelIndicator levelIndicator;
    public List<Reflector> pieceReflectors;
    public List<Level> levels;
    private int currLevelIdx = -1;
    private float startDelaySec;
    private GameObject bomb;
    private DataStorage dataStorage;
    private bool shouldAnimatePiece;
    private bool loadingLevel;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("ScreenManager").GetComponent<ScreenManager>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        pieceShooterComp = pieceShooter.GetComponent<PieceShooter>();
    }

    public void LoadLevel(int levelIndex, float startDelaySec)
    {
        this.loadingLevel = true;
        if (levelIndex == 0)
        {
            score.Reset();
        }
        currLevelIdx = levelIndex;
        ResetCurrentLevel();
        if(this.shouldAnimatePiece)
        {
            pieceTutorialAnimator.SetAngles(this.levels[currLevelIdx].pieceAnimationAngles);
            pieceTutorialAnimator.AnimatePieceAndThen(() =>
            {
                StartCoroutine(StartCurrentLevelAfterDelay(startDelaySec));
            });
        }
        else
        {
            StartCoroutine(StartCurrentLevelAfterDelay(startDelaySec));
        }
        this.loadingLevel = false;
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
        timer.UnPause();
        pieceShooter.SetActive(false);
        pieceShooter.GetComponent<PieceShooter>().SetShootingEnabled(true);
        bomb.SendMessage("StartBomb");
        music.Play();
        pieceShooter.SetActive(true);
        levelObscurer.SetActive(false);
        Level level = levels[currLevelIdx];
        pieceShooterComp.SetAngleChangeMode(level.pieceShooterAngleChangeMode);
        pieceShooterComp.SetAngles(level.pieceShooterAngles);
        pieceShooterComp.Init();
        if (!Application.isEditor)
        {
            AnalyticsEvent.LevelStart(currLevelIdx + 1, new Dictionary<string, object>
            {
                { "score", score.GetScore() }
            });
        }
        score.RefreshDispScore();
        pieceTutorialAnimator.DestroySpawnedPiece();
    }

    public void ResetCurrentLevel()
    {
        Level level = levels[currLevelIdx];
        pieceShooter.SetActive(false);
        this.shouldAnimatePiece = level.pieceAnimationAngles.Length > 0 && this.loadingLevel;
        levelObscurer.SetActive(this.shouldAnimatePiece);
        foreach (GameObject prevBomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            Destroy(prevBomb);
        }
        bomb = Instantiate(level.bomb);
        bomb.transform.SetParent(canvas.transform, false);
        timer.Init(bomb.GetComponent<Detonator>(), bomb.GetComponentInChildren<BombDefuzer>());
        timer.setTime(level.secondsOnTimer);
        timer.Pause();
        timer.gameObject.SetActive(true);
        foreach (Transform child in bombPieces.transform)
        {
            Destroy(child.gameObject);
        }
        bombPieces.SetActive(true);
        if (this.startDelaySec > 0)
        {
            pieceShooter.GetComponent<PieceShooter>().SetShootingEnabled(false);
        }
        shootTapZone.SetActive(true);
        gameOverUI.Hide();
        music.Reset();
        levelIndicator.Set(this.GetCurrentLevelIndex() + 1, this.LevelCount());
        score.RefreshDispScore();
    }

    public void LoadNextLevelAndStartAfterDelay(float delaySec)
    {
        if (currLevelIdx < LevelCount() - 1)
        {
            LoadLevel(++currLevelIdx, delaySec);
        }
        else
        {
            screenManager.ShowGameWonScreen();
        }
    }

    public void ResetToFirstLevel()
    {
        currLevelIdx = 0;
        dataStorage.SaveLevel(0);
    }

    public int GetCurrentLevelIndex()
    {
        return currLevelIdx;
    }

    public int LevelCount()
    {
        return levels.Count;
    }

    [System.Serializable]
    public class Level
    {
        public GameObject bomb;
        public float secondsOnTimer;
        public float[] pieceShooterAngles;
        public PieceShooter.AngleChangeMode pieceShooterAngleChangeMode = PieceShooter.AngleChangeMode.ON_SHOOT;
        public float[] pieceAnimationAngles;
    }
}
