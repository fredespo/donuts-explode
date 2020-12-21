using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text text;
    public int score;
    public int scoreChangePerSec = 500;
    private float dispScore;
    private AudioSource soundEffect;
    private float lastSoundEffectTime;
    private float delayBetweenSoundEffects = 0.1f;

    void Start()
    {
        text = GetComponent<Text>();
        soundEffect = GetComponent<AudioSource>();
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
                soundEffect.pitch = 1.0f;
                delayBetweenSoundEffects = 0.1f;
            }
            else if(dispScore > score)
            {
                dispScore -= scoreChange;
                soundEffect.pitch = 0.8f;
                delayBetweenSoundEffects = 0.1f;
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
    }

    public void Reset()
    {
        score = 0;
        dispScore = 0;
        RefreshText();
    }

    public int GetScore()
    {
        return score;
    }
}
