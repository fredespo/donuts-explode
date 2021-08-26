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
    public GameObject mainMenu;
    public GameObject dontSaveConfirmMenu;

    public void Refresh()
    {
        scoreText.text = score.GetScore().ToString();
        if (CanSaveHighScore())
        {
            highScoreMsg.SetActive(true);
            saveScoreBtn.SetActive(true);
            nameField.gameObject.SetActive(true);
            TouchScreenKeyboard.hideInput = true;
            nameField.text = "";
            finishBtn.SetActive(false);
            mainMenu.SetActive(true);
            dontSaveConfirmMenu.SetActive(false);
        }
        else
        {
            highScoreMsg.SetActive(false);
            saveScoreBtn.SetActive(false);
            nameField.gameObject.SetActive(false);
            finishBtn.SetActive(true);
        }
    }

    public void OnExit()
    {
        score.Reset();
    }

    private bool CanSaveHighScore()
    {
        return highScoreTable.GetSize() < highScoreTable.GetCapacity()
            || score.GetScore() > dataStorage.GetLowestScore();
    }
}
