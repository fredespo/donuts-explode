using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BombDefuzer : MonoBehaviour
{
    public bool isBonusBomb = false;
    private textTimer timer;
    public Rotator bombRotator;
    public Animator bombHighlightAnim;
    public AudioSource soundEffect;
    public GameObject defuzedUI;
    private GameObject pieces;
    private GameObject pieceShooter;
    private GameObject shootTapZone;
    public Animator fuseAnim;
    public GameObject fuseFlare;
    private GameMusic music;
    private bool defuzed = false;
    private DataStorage dataStorage;
    private Score score;
    private ScoreBonus scoreBonus;

    void Start()
    {
        if(!isBonusBomb)
        {
            timer = GameObject.FindGameObjectWithTag("BombTimer").GetComponent<textTimer>();
        }
        pieceShooter = GameObject.FindGameObjectWithTag("PieceShooter");
        shootTapZone = GameObject.FindGameObjectWithTag("ShootTapZone");
        music = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameMusic>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        scoreBonus = GameObject.FindGameObjectWithTag("ScoreBonus").GetComponent<ScoreBonus>();
    }

    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            Defuze();
        }
    }

    void Defuze()
    {
        if(defuzed)
        {
            return;
        }
        defuzed = true;

        if (this.isBonusBomb)
        {
            GameObject.FindGameObjectWithTag("BonusBombsManager").GetComponent<BonusBombs>().DefuzedBonusBomb();
            fuseFlare.SetActive(false);
            return;
        }

        StartCoroutine(AddTimeToScoreAfterDelay(1.0f));
        int newScore = score.GetScore() + GetPointsEarned() + this.scoreBonus.GetBonus();
        dataStorage.SaveScore(newScore);
        int currLevel = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().GetCurrentLevelIndex() + 1;
        if (!Application.isEditor)
        {
            AnalyticsEvent.LevelComplete(currLevel, new Dictionary<string, object>
            {
                { "score", newScore }
            });
        }

        music.WindDown();
        shootTapZone.SetActive(false);
        timer.Pause();
        fuseAnim.enabled = false;
        fuseFlare.SetActive(false);
        bombRotator.enabled = false;
        GameObject spawnedDefuzedUI = GameObject.Instantiate(defuzedUI);
        spawnedDefuzedUI.gameObject.transform.SetParent(gameObject.transform.parent.transform, false);
        spawnedDefuzedUI.gameObject.transform.rotation = Quaternion.identity;
        pieces = GameObject.FindGameObjectWithTag("PieceKeeper");
        pieces.SetActive(false);
        GameObject pieceShooter = GameObject.FindGameObjectWithTag("PieceShooter");
        bombHighlightAnim.enabled = false;
        bombHighlightAnim.enabled = true;
        soundEffect.volume = 0.8f;
        pieceShooter.GetComponent<PieceShooter>().MakeBonusSound(this.soundEffect, 0.473473f);
        pieceShooter.SetActive(false);
        dataStorage.SaveLevel(currLevel);
        dataStorage.Save();
    }

    private IEnumerator AddTimeToScoreAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        score.Add(GetPointsEarned());
        timer.CountDownFastAndThen(() => ApplyBonusAndShowWinUI());
    }

    private void ApplyBonusAndShowWinUI()
    {
        this.scoreBonus.AddToScoreWithAnimationAndThen(() => ShowWinUi());
    }

    private void ShowWinUi()
    {
        foreach (Transform child in GameObject.FindGameObjectWithTag("WinUI").transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private int GetPointsEarned()
    {
        return (int)Mathf.Max(0, Mathf.Round(timer.GetSecondsLeft() * 100));
    }

    public int GetNumHolesLeft()
    {
        return gameObject.transform.childCount;
    }

    public int GetNumUnfilledHoles()
    {
        int count = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (!child.gameObject.GetComponent<BombHole>().IsFilled())
            {
                ++count;
            }
        }
        return count;
    }

    public List<Transform> GetHoles()
    {
        List<Transform> holes = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            holes.Add(child);
        }
        return holes;
    }

    public bool IsDefuzed()
    {
        return this.defuzed;
    }
}
