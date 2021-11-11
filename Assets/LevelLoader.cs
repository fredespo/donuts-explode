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
    public AnimatedCountDown countDown;
    public LevelIndicator levelIndicator;
    public ScoreBonus scoreBonus;
    public List<Reflector> pieceReflectors;
    public List<Level> levels;
    public List<BonusLevel> bonusLevels;
    private int bonusLevelIndex;
    private int currLevelIdx = -1;
    private float startDelaySec;
    private GameObject bomb;
    private DataStorage dataStorage;
    private bool shouldAnimatePiece;
    private bool loadingLevel;
    private bool isBonusLevel;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("ScreenManager").GetComponent<ScreenManager>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        pieceShooterComp = pieceShooter.GetComponent<PieceShooter>();
    }

    public void LoadLevel(int levelIndex, float startDelaySec, int bonusLevelsCompleted = 0)
    {
        this.loadingLevel = true;
        if (levelIndex == 0)
        {
            score.Reset();
        }
        this.currLevelIdx = levelIndex;
        this.isBonusLevel = bonusLevelsCompleted < this.bonusLevels.Count && levelIndex == this.bonusLevels[bonusLevelsCompleted].afterLevel;
        ResetCurrentLevel();
        if(this.shouldAnimatePiece)
        {
            bombPieces.SetActive(false);
            pieceTutorialAnimator.SetAngles(this.levels[currLevelIdx].pieceAnimationAngles);
            pieceTutorialAnimator.AnimatePieceAndThen(() =>
            {
                bombPieces.SetActive(true);
                pieceTutorialAnimator.DestroySpawnedPiece();
                StartCoroutine(StartCurrentLevelAfterDelay(0));
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
        PieceShooter.AngleChangeMode pieceShooterAngleChangeMode = PieceShooter.AngleChangeMode.ON_SHOOT;
        float[] pieceShooterAngles = {0};
        if (!this.isBonusLevel)
        {
            timer.UnPause();
            Level level = levels[currLevelIdx];
            pieceShooterAngleChangeMode = level.pieceShooterAngleChangeMode;
            pieceShooterAngles = level.pieceShooterAngles;
            bomb.SendMessage("StartBomb");
        }
        
        pieceShooter.SetActive(true);
        pieceShooterComp.SetShootingEnabled(true);
        pieceShooterComp.SetAngleChangeMode(pieceShooterAngleChangeMode);
        pieceShooterComp.SetAngles(pieceShooterAngles);
        pieceShooterComp.SetIsBonusLevel(this.isBonusLevel);
        pieceShooterComp.Init();
        music.Play();
        levelObscurer.SetActive(false);
        if (!Application.isEditor)
        {
            AnalyticsEvent.LevelStart(currLevelIdx + 1, new Dictionary<string, object>
            {
                { "score", score.GetScore() }
            });
        }
        score.RefreshDispScore();
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

        if(this.isBonusLevel)
        {
            levelIndicator.gameObject.SetActive(false);
        }
        else
        {
            bomb = Instantiate(level.bomb);
            bomb.transform.SetParent(canvas.transform, false);
            timer.Init(bomb.GetComponent<Detonator>(), bomb.GetComponentInChildren<BombDefuzer>());
            timer.setTime(level.secondsOnTimer);
            timer.Pause();
            timer.gameObject.SetActive(true);
            levelIndicator.gameObject.SetActive(true);
            levelIndicator.Set(this.GetCurrentLevelIndex() + 1, this.LevelCount());
        }
        
        foreach (Transform child in bombPieces.transform)
        {
            Destroy(child.gameObject);
        }

        bombPieces.SetActive(true);
        shootTapZone.SetActive(true);
        gameOverUI.Hide();
        music.Reset();
        score.RefreshDispScore();
        this.scoreBonus.Reset();
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

    [System.Serializable]
    public class BonusLevel
    {
        public int afterLevel;
    }
}
