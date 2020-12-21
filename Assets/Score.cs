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

    void Start()
    {
        text = GetComponent<Text>();
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
            }
            else if(dispScore > score)
            {
                dispScore -= scoreChange;
            }
            RefreshText();
        }
    }

    private void RefreshText()
    {
        if (text != null)
        {
            text.text = ((int)dispScore).ToString();
        }
    }

    public void Add(int amt)
    {
        score += amt;
    }
}
