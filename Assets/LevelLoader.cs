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
    public GameObject pauseButton;
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
    public GameObject bonusLevelIndicator;
    public ScoreBonus scoreBonus;   
    public List<Reflector> pieceReflectors;
    public List<Level> levels;
    public float bonusLevelStartDelaySec;
    public BonusBombs bonusBombs;
    public List<BonusLevel> bonusLevels;
    private int bonusLevelIndex;
    private int currLevelIdx = -1;
    private float startDelaySec;
    private GameObject bomb;
    private DataStorage dataStorage;
    private bool shouldAnimatePiece;
    private bool loadingLevel;
    private bool isBonusLevel;
    private BonusLevel currBonusLevel;

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
        this.currLevelIdx = levelIndex;
        int bonusLevelsCompleted = dataStorage.GetBonusLevelsCompleted();
        this.isBonusLevel = bonusLevelsCompleted < this.bonusLevels.Count && levelIndex == this.bonusLevels[bonusLevelsCompleted].afterLevel;
        if (this.isBonusLevel)
        {
            this.currBonusLevel = this.bonusLevels[bonusLevelsCompleted];
        }
        ResetCurrentLevel();
        if (this.shouldAnimatePiece)
        {
            bombPieces.SetActive(false);
            pieceTutorialAnimator.SetAngles(this.levels[currLevelIdx].pieceAnimationAngles);
            pauseButton.SetActive(false);
            pieceTutorialAnimator.AnimatePieceAndThen(() =>
            {
                bombPieces.SetActive(true);
                pauseButton.SetActive(true);
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
        float[] pieceShooterAngles = { 0 };
        Level level = levels[currLevelIdx];
        pieceShooterAngleChangeMode = level.pieceShooterAngleChangeMode;
        pieceShooterAngles = level.pieceShooterAngles;

        if (!this.isBonusLevel)
        {
            timer.UnPause();
            bomb.SendMessage("StartBomb");
        }

        if (this.isBonusLevel)
        {
            StartCoroutine(StartPieceShooterAfterDelaySec(bonusLevelStartDelaySec, pieceShooterAngleChangeMode, pieceShooterAngles));
            this.bonusBombs.gameObject.SetActive(true);
            this.bonusBombs.Init(this.currBonusLevel.spawns);
        }
        else
        {
            StartPieceShooter(pieceShooterAngleChangeMode, pieceShooterAngles);
        }

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

    private IEnumerator StartPieceShooterAfterDelaySec(float delaySec, PieceShooter.AngleChangeMode angleChangeMode, float[] angles)
    {
        yield return new WaitForSeconds(delaySec);
        StartPieceShooter(angleChangeMode, angles);
    }

    private void StartPieceShooter(PieceShooter.AngleChangeMode angleChangeMode, float[] angles)
    {
        pieceShooter.SetActive(true);
        pieceShooterComp.SetShootingEnabled(true);
        pieceShooterComp.SetAngleChangeMode(angleChangeMode);
        pieceShooterComp.SetAngles(angles);
        pieceShooterComp.SetIsBonusLevel(this.isBonusLevel);
        pieceShooterComp.Init();
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

        if (this.isBonusLevel)
        {
            levelIndicator.gameObject.SetActive(false);
            bonusLevelIndicator.SetActive(true);
        }
        else
        {
            bomb = Instantiate(level.bomb);
            bomb.transform.SetParent(canvas.transform, false);
            timer.Init(bomb.GetComponent<Detonator>(), bomb.GetComponentInChildren<BombDefuzer>());
            timer.setTime(level.secondsOnTimer);
            timer.Pause();
            timer.gameObject.SetActive(true);
            bonusLevelIndicator.SetActive(false);
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
            if(!this.isBonusLevel)
            {
                ++currLevelIdx;
            }
            LoadLevel(currLevelIdx, delaySec);
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

    public void BonusLevelCompleted()
    {
        this.dataStorage.IncrementBonusLevelsCompleted();
        this.dataStorage.Save();
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
