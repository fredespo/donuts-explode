﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Animator scoreBonusAnimator;
    private DataStorage dataStorage;
    private Score score;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("BombTimer").GetComponent<textTimer>();
        pieceShooter = GameObject.FindGameObjectWithTag("PieceShooter");
        shootTapZone = GameObject.FindGameObjectWithTag("ShootTapZone");
        music = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<GameMusic>();
        scoreBonusAnimator = GameObject.FindGameObjectWithTag("ScoreBonus").GetComponent<Animator>();
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
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
            int currLevel = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().GetCurrentLevelIndex() + 1;
                
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
            pieceShooter.GetComponent<PieceShooter>().MakeBonusSound(this.soundEffect, 0.673473f);
            pieceShooter.SetActive(false);
            dataStorage.SaveLevel(currLevel);
            dataStorage.Save();
            defuzed = true;
        }
    }

    private IEnumerator AddTimeToScoreAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        score.Add(GetPointsEarned());
        timer.CountDownFastAndThen(() => this.scoreBonusAnimator.SetTrigger("Apply"));
    }

    private int GetPointsEarned()
    {
        return (int)Mathf.Max(0, Mathf.Round(timer.GetSecondsLeft() * 100));
    }

    public int GetNumHolesLeft()
    {
        return gameObject.transform.childCount;
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
}
