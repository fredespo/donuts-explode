using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text text;
    public int score;
    public int scoreChangePerSec = 500;
    public DataStorage dataStorage;
    private float dispScore;
    private AudioSource soundEffect;
    private float lastSoundEffectTime;
    private float delayBetweenSoundEffects = 0.1f;
    private float origVolume;
    private float origPitch;

    void Start()
    {
        text = GetComponent<Text>();
        soundEffect = GetComponent<AudioSource>();
        origVolume = soundEffect.volume;
        origPitch = soundEffect.pitch;
        score = dataStorage.GetScore();
        dispScore = score;
        RefreshText();
    }

    void Update()
    {
        if(dispScore != score)
        {
            float scoreChange = Time.deltaTime * scoreChangePerSec;
            if (Mathf.Abs(dispScore - score) < scoreChange)
            {
                dispScore = score;
            }
            else if(dispScore < score)
            {
                dispScore += scoreChange;
                soundEffect.pitch = origPitch;
                soundEffect.volume = origVolume;
            }
            else if(dispScore > score)
            {
                dispScore -= scoreChange;
                soundEffect.pitch = origPitch - 0.2f;
                soundEffect.volume = origVolume + 0.1f;
            }
            RefreshText();
            if(Time.time - lastSoundEffectTime >= delayBetweenSoundEffects)
            {
                soundEffect.Play(0);
                lastSoundEffectTime = Time.time;
            }
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
}
