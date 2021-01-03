using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BombDefuzer : MonoBehaviour
{
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
    private Score score;
    private DataStorage dataStorage;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("BombTimer").GetComponent<textTimer>();
        pieces = GameObject.FindGameObjectWithTag("PieceKeeper");
        pieceShooter = GameObject.FindGameObjectWithTag("PieceShooter");
        shootTapZone = GameObject.FindGameObjectWithTag("ShootTapZone");
        music = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameMusic>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
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
        if (!defuzed)
        {
            StartCoroutine(AddTimeToScoreAfterDelay(1.0f));
            int newScore = score.GetScore() + GetPointsEarned();
            dataStorage.SaveScore(newScore);
            int currLevel = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().GetCurrentLevel();
            AnalyticsEvent.LevelComplete(currLevel + 1, new Dictionary<string, object> 
            {
                { "score", newScore }
            });
            music.WindDown();
            shootTapZone.SetActive(false);
            timer.Pause();
            fuseAnim.enabled = false;
            fuseFlare.SetActive(false);
            bombRotator.enabled = false;
            GameObject spawnedDefuzedUI = GameObject.Instantiate(defuzedUI);
            spawnedDefuzedUI.gameObject.transform.SetParent(gameObject.transform.parent.transform, false);
            spawnedDefuzedUI.gameObject.transform.rotation = Quaternion.identity;
            pieces.SetActive(false);
            pieceShooter.SetActive(false);
            bombHighlightAnim.enabled = false;
            bombHighlightAnim.enabled = true;
            soundEffect.Play(0);
            dataStorage.SaveLevel(currLevel + 1);
            defuzed = true;
        }
    }

    private IEnumerator AddTimeToScoreAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        score.Add(GetPointsEarned());
        timer.CountDownFast();
    }

    private int GetPointsEarned()
    {
        return (int)Mathf.Round(timer.GetSecondsLeft() * 100);
    }
}
