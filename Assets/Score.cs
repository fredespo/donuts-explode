using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text text;
    public int score;
    public int scoreChangePerSec = 500;
    private int currScoreChangePerSec;
    public int maxTimeToChange = 4;
    public AudioSource pointGainedSound;
    public AudioSource pointLostSound;
    public DataStorage dataStorage;
    private float dispScore;

    void Start()
    {
        text = GetComponent<Text>();
        score = dataStorage.GetScore();
        dispScore = score;
        currScoreChangePerSec = scoreChangePerSec;
        RefreshText();
    }

    void Update()
    {
        if(dispScore != score)
        {
            float scoreChange = Time.deltaTime * currScoreChangePerSec;
            if (Mathf.Abs(dispScore - score) < scoreChange)
            {
                dispScore = score;
            }
            else if(dispScore < score)
            {
                dispScore += scoreChange;
                PlayOnLoop(this.pointGainedSound);
            }
            else if(dispScore > score)
            {
                dispScore -= scoreChange;
                PlayOnLoop(this.pointLostSound);
            }
            RefreshText();
        }
        else
        {
            StopMakingSoundEffects();
        }
    }

    private void StopMakingSoundEffects()
    {
        this.pointGainedSound.loop = false;
        this.pointLostSound.loop = false;
    }

    private void PlayOnLoop(AudioSource audio)
    {
        if(!audio.isPlaying)
        {
            audio.loop = true;
            audio.Play();
        }
    }

    private void RefreshText()
    {
        if (text != null)
        {
            text.text = ((int)dispScore).ToString();
        }
    }

    public void AddAfterDelay(int amt, float delaySec)
    {
        StartCoroutine(AddAfterDelayCoroutine(amt, delaySec));
    }

    private IEnumerator AddAfterDelayCoroutine(int amt, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        Add(amt);
    }

    public void Add(int amt)
    {
        float timeToChange = Mathf.Abs(amt) / scoreChangePerSec;
        if(amt < 0 && timeToChange > this.maxTimeToChange)
        {
            currScoreChangePerSec = Mathf.Abs(amt) / this.maxTimeToChange;
        }
        else
        {
            currScoreChangePerSec = scoreChangePerSec;
        }

        score += amt;
        if(score < 0)
        {
            score = 0;
        }
        SaveScore();
    }

    private void SaveScore()
    {
        dataStorage.SaveScore(score);
    }

    public void Reset()
    {
        score = 0;
        dispScore = 0;
        RefreshText();
        SaveScore();
    }

    public int GetScore()
    {
        return score;
    }

    public void RefreshDispScore()
    {
        dispScore = score;
        RefreshText();
    }
}
