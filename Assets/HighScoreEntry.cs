using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreEntry : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetColor(Color color)
    {
        nameText.color = color;
        scoreText.color = color;
    }

    public void SetY(float yPos)
    {
        RectTransform t = GetComponent<RectTransform>();
        t.localPosition = new Vector3(t.localPosition.x, yPos, t.localPosition.z);
    }
}
