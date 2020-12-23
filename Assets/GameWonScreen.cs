using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWonScreen : MonoBehaviour
{
    public GameObject highScoreMsg;
    public InputField nameField;
    public GameObject saveScoreBtn;
    public GameObject finishBtn;
    public HighScoreTable highScoreTable;
    public DataStorage dataStorage;
    public Score score;
    public Text scoreText;

    public void Refresh()
    {
        scoreText.text = score.GetScore().ToString();
        if (CanSaveHighScore())
        {
            highScoreMsg.SetActive(true);
            saveScoreBtn.SetActive(true);
            nameField.gameObject.SetActive(true);
            nameField.text = "";
            finishBtn.SetActive(false);
        }
        else
        {
            highScoreMsg.SetActive(false);
            saveScoreBtn.SetActive(false);
            nameField.gameObject.SetActive(false);
            finishBtn.SetActive(true);
        }
    }

    private bool CanSaveHighScore()
    {
        return highScoreTable.GetSize() < highScoreTable.GetCapacity()
            || score.GetScore() > dataStorage.GetLowestScore();
    }
}
