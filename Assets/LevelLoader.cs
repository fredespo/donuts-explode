using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using BinaryCharm.SemanticColorPalette;
using System;

public class LevelLoader : MonoBehaviour
{
    public GameObject canvas;
    private ScreenManager screenManager;
    public GameMusic music;
    public textTimer timer;
    public GameObject pauseButton;
    public GameObject bombPieces;
    public SCP_PaletteProvider donutPaletteProvider;
    public GameObject pieceShooter;
    public PieceTutorialAnimator pieceTutorialAnimator;
    private PieceShooter pieceShooterComp;
    public GameObject shootTapZone;
    public GameOverUI levelLostUI;
    public GameOverUI gameOverUI;
    public Score score;
    public Lives lives;
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
    private GameObject bomb;
    private DataStorage dataStorage;
    private bool shouldAnimatePiece;
    private bool loadingLevel;
    private bool isBonusLevel;
    private BonusLevel currBonusLevel;
    private Action startBombAction;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("ScreenManager").GetComponent<ScreenManager>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        pieceShooterComp = pieceShooter.GetComponent<PieceShooter>();
    }

    public void LoadLevel(int levelIndex, float startDelaySec, bool fromTitle = false)
    {
        this.loadingLevel = true;
        this.setDonutPaletteForLevel(levelIndex);
        if (levelIndex == 0)
        {
            score.Reset();
            lives.Reset();
        }
        this.currLevelIdx = levelIndex;
        int bonusLevelsCompleted = dataStorage.GetBonusLevelsCompleted();
        this.isBonusLevel = bonusLevelsCompleted < this.bonusLevels.Count && levelIndex == this.bonusLevels[bonusLevelsCompleted].afterLevel;
        if (this.isBonusLevel)
        {
            this.currBonusLevel = this.bonusLevels[bonusLevelsCompleted];
        }


        if (this.shouldAnimatePiece)
        {
            bombPieces.SetActive(false);
            pauseButton.SetActive(false);
        }
        ResetCurrentLevel(() =>
        {
            if (this.shouldAnimatePiece)
            {
                bombPieces.SetActive(true);
                pauseButton.SetActive(true);
                GameObject firstPiece = pieceTutorialAnimator.GetSpawnedPiece();
                StartCoroutine(StartCurrentLevelAfterDelay(0, firstPiece));
            }
            else
            {
                StartCurrentLevel();
            }

            this.loadingLevel = false;
        },
        () =>
        {
            pieceTutorialAnimator.SetAngles(this.levels[currLevelIdx].pieceAnimationAngles);
            pieceTutorialAnimator.AnimatePieceAndThen(this.startBombAction);
        },
        fromTitle, startDelaySec);
    }

    private void setDonutPaletteForLevel(int levelIndex)
    {
        int paletteIndex = getDonutPaletteForLevel(levelIndex);
        this.donutPaletteProvider.SetActivePaletteIndex(paletteIndex);
    }

    private int getDonutPaletteForLevel(int levelIndex)
    {
        if (levelIndex == 0) return 0;
        return UnityEngine.Random.Range(0, this.donutPaletteProvider.GetNumPalettes());
    }

    public void StartCurrentLevelAfterDelaySec(float delaySec)
    {
        StartCoroutine(StartCurrentLevelAfterDelay(delaySec));
    }

    public IEnumerator StartCurrentLevelAfterDelay(float delaySec, GameObject firstPiece = null)
    {
        yield return new WaitForSeconds(delaySec);
        StartCurrentLevel(firstPiece);
    }

    public void StartCurrentLevel(GameObject firstPiece = null)
    {
        PieceShooter.AngleChangeMode pieceShooterAngleChangeMode = PieceShooter.AngleChangeMode.ON_SHOOT;
        float[] pieceShooterAngles = { 0 };
        Level level = levels[currLevelIdx];
        pieceShooterAngleChangeMode = level.pieceShooterAngleChangeMode;
        pieceShooterAngles = level.pieceShooterAngles;

        if (!this.isBonusLevel)
        {
            timer.UnPause();
        }

        if (this.isBonusLevel)
        {
            StartCoroutine(StartPieceShooterAfterDelaySec(bonusLevelStartDelaySec, pieceShooterAngleChangeMode, pieceShooterAngles));
            this.bonusBombs.gameObject.SetActive(true);
            this.bonusBombs.Init(this.currBonusLevel.spawns);
        }
        else
        {
            StartPieceShooter(pieceShooterAngleChangeMode, pieceShooterAngles, firstPiece);
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

    private void StartPieceShooter(PieceShooter.AngleChangeMode angleChangeMode, float[] angles, GameObject firstPiece = null)
    {
        pieceShooter.SetActive(true);
        pieceShooterComp.SetShootingEnabled(true);
        pieceShooterComp.SetAngleChangeMode(angleChangeMode);
        pieceShooterComp.SetAngles(angles);
        pieceShooterComp.SetIsBonusLevel(this.isBonusLevel);
        pieceShooterComp.Init(firstPiece);
        pieceTutorialAnimator.DestroySpawnedPiece();
    }

    public void ResetCurrentLevel(Action andThen, Action animatePiece, bool fromTitle = false, float startDelaySec = 0f)
    {
        Level level = levels[currLevelIdx];
        pieceShooter.SetActive(false);
        this.shouldAnimatePiece = level.pieceAnimationAngles.Length > 0 && this.loadingLevel && !fromTitle;
        foreach (GameObject prevBomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            Destroy(prevBomb);
        }

        if (this.isBonusLevel)
        {
            levelIndicator.gameObject.SetActive(false);
            bonusLevelIndicator.SetActive(true);
            bombPieces.SetActive(true);
            shootTapZone.SetActive(true);
        }
        else
        {
            bomb = Instantiate(level.bomb);
            bomb.transform.SetParent(canvas.transform, false);
            Bomb bombComp = bomb.GetComponent<Bomb>();
            bombComp.SetPalette(this.donutPaletteProvider.GetActivePaletteIndex());
            timer.Pause();
            var defuzer = bomb.GetComponentInChildren<BombDefuzer>();
            var detonator = bomb.GetComponent<Detonator>();
            timer.Init(detonator, defuzer, bombComp);
            timer.setTime(level.secondsOnTimer);
            timer.gameObject.SetActive(false);
            if (fromTitle)
            {
                levelObscurer.SetActive(this.shouldAnimatePiece);
                StartCoroutine(StartTimerAfterDelaySec(startDelaySec, defuzer, detonator, bombComp, andThen));
            }
            else
            {
                levelObscurer.SetActive(false);
                bomb.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 700);
                this.startBombAction = () =>
                {
                    bombComp.StartBomb();
                    bomb.GetComponent<Bomb>().AnimateInAndThen(() => StartTimer(defuzer, detonator, bombComp, andThen));
                };
                if (this.shouldAnimatePiece)
                {
                    animatePiece.Invoke();
                }
                else
                {
                    this.startBombAction.Invoke();
                }
            }
        }

        foreach (Transform child in bombPieces.transform)
        {
            Destroy(child.gameObject);
        }


        levelLostUI.Hide();
        gameOverUI.Hide();
        music.Reset();
        score.RefreshDispScore();
        this.scoreBonus.Reset();

        if (this.isBonusLevel)
        {
            andThen.Invoke();
        }
    }

    private IEnumerator StartTimerAfterDelaySec(float delaySec, BombDefuzer defuzer, Detonator detonator, Bomb bombComp, Action andThen)
    {
        yield return new WaitForSeconds(delaySec);
        StartTimer(defuzer, detonator, bombComp, andThen);
    }

    private void StartTimer(BombDefuzer defuzer, Detonator detonator, Bomb bombComp, Action andThen)
    {
        bombComp.StartBomb();
        timer.gameObject.SetActive(true);
        bonusLevelIndicator.SetActive(false);
        levelIndicator.gameObject.SetActive(true);
        levelIndicator.Set(this.GetCurrentLevelIndex() + 1, this.LevelCount());
        bombPieces.SetActive(true);
        shootTapZone.SetActive(true);
        defuzer.Init(timer, shootTapZone, this.pieceShooter);
        detonator.Init(this.pieceShooter);
        bombComp.Init(timer);
        andThen.Invoke();
    }

    public void LoadNextLevelAndStartAfterDelay(float delaySec)
    {
        if (currLevelIdx < LevelCount() - 1)
        {
            if (!this.isBonusLevel)
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
