using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text text;
    public int score;
    public int scoreChangePerSec = 500;
    public Vector2 textCenter;
    private RectTransform pos;
    private int currScoreChangePerSec;
    public int maxTimeToChange = 4;
    public AudioSource pointGainedSound;
    public AudioSource pointLostSound;
    public DataStorage dataStorage;
    public float dispScore;
    private bool snapToCenter;

    void Start()
    {
        text = GetComponent<Text>();
        score = dataStorage.GetScore();
        dispScore = score;
        currScoreChangePerSec = scoreChangePerSec;
        pos = GetComponent<RectTransform>();
        RefreshText();
    }

    void Update()
    {
        if (dispScore != score)
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

        if(this.snapToCenter)
        {
            this.pos.anchoredPosition = new Vector2(CalcXPosNeededToCenter(), this.pos.anchoredPosition.y);
        }
    }

    public void SetSnapToCenter(bool value, float delay = 0)
    {
        StartCoroutine(SetSnapToCenterCoroutine(value, delay));
    }

    private IEnumerator SetSnapToCenterCoroutine(bool value, float delay)
    {
        yield return new WaitForSeconds(delay);
        this.snapToCenter = value;
    }

    public void StartMoveToHorizontalCenterCoroutineAfterDelay(float moveDurationSec, float delaySec = 0)
    {
        StartCoroutine(StartMoveToHorizontalCenterCoroutine(moveDurationSec, delaySec));
    }

    public IEnumerator StartMoveToHorizontalCenterCoroutine(float moveDurationSec, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        StartCoroutine(StartMoveToHorizontalCenter(moveDurationSec));
    }

    private IEnumerator StartMoveToHorizontalCenter(float durationSec)
    {
        float currentTime = 0;
        float startX = this.pos.anchoredPosition.x;

        while (currentTime < durationSec)
        {
            currentTime += Time.deltaTime;
            float targetX = Mathf.Lerp(startX, CalcXPosNeededToCenter(), currentTime / durationSec);
            this.pos.anchoredPosition = new Vector2(targetX, this.pos.anchoredPosition.y);
            yield return null;
        }
        yield break;
    }

    public void MoveYPos(float targetY, float moveDurationSec, float delaySec)
    {
        StartCoroutine(StartMoveYPosCoroutine(targetY, moveDurationSec, delaySec));
    }

    private IEnumerator StartMoveYPosCoroutine(float targetY, float moveDurationSec, float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        StartCoroutine(StartMoveYPos(targetY, moveDurationSec));
    }

    private IEnumerator StartMoveYPos(float targetY, float durationSec)
    {
        float currentTime = 0;
        float startY = this.pos.anchoredPosition.y;

        while (currentTime < durationSec)
        {
            currentTime += Time.deltaTime;
            float newY = Mathf.Lerp(startY, targetY, currentTime / durationSec);
            this.pos.anchoredPosition = new Vector2(this.pos.anchoredPosition.x, newY);
            yield return null;
        }
        yield break;
    }

    private float CalcXPosNeededToCenter()
    {
        float centerX = this.pos.anchoredPosition.x - (this.text.preferredWidth / 2);
        return this.pos.anchoredPosition.x - (centerX + 400);
    }

    public void TweenFontSize(int targetSize, float duration, float delay)
    {
        StartCoroutine(StartTweenFontSizeCoroutine(targetSize, duration, delay));
    }

    private IEnumerator StartTweenFontSizeCoroutine(int targetSize, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(TweenFontSizeCoroutine(targetSize, duration));
    }

    private IEnumerator TweenFontSizeCoroutine(int targetSize, float duration)
    {
        float currentTime = 0;
        float startSize = this.text.fontSize;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            int newSize = (int)Mathf.Lerp(startSize, targetSize, currentTime / duration);
            this.text.fontSize = newSize;
            yield return null;
        }
        yield break;
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
