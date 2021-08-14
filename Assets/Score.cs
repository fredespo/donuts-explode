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
    public DataStorage dataStorage;
    private float dispScore;
    private AudioSource soundEffect;
    private float delayBetweenSoundEffects = 0.1f;
    private float origVolume;
    private float origPitch;

    void Start()
    {
        text = GetComponent<Text>();
        soundEffect = GetComponent<AudioSource>();
        origPitch = soundEffect.pitch;
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
                soundEffect.pitch = origPitch;
            }
            else if(dispScore > score)
            {
                dispScore -= scoreChange;
                soundEffect.pitch = origPitch - 0.2f;
            }
            RefreshText();

            if(!soundEffect.isPlaying)
            {
                soundEffect.Play(0);
            }
        }
        else
        {
            soundEffect.Stop();
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
        if(timeToChange > this.maxTimeToChange)
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
