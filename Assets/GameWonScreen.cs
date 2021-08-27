using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWonScreen : MonoBehaviour
{
    public GameObject highScoreMsg;
    public InputField nameField;
    public GameObject saveScoreBtn;
    public HighScoreTable highScoreTable;
    public DataStorage dataStorage;
    public LevelLoader levelLoader;
    public Score score;
    public Text scoreText;
    public GameObject mainMenu;
    public GameObject dontSaveConfirmMenu;
    public GameObject noScoreMenu;

    public void Refresh()
    {
        scoreText.text = score.GetScore().ToString();
        dontSaveConfirmMenu.SetActive(false);
        if (CanSaveHighScore())
        {
            highScoreMsg.SetActive(true);
            saveScoreBtn.SetActive(true);
            nameField.gameObject.SetActive(true);
            TouchScreenKeyboard.hideInput = true;
            nameField.text = "";
            mainMenu.SetActive(true);
            noScoreMenu.SetActive(false);
        }
        else
        {
            highScoreMsg.SetActive(false);
            mainMenu.SetActive(false);
            noScoreMenu.SetActive(true);
            nameField.gameObject.SetActive(false);
        }
    }

    public void OnExit()
    {
        score.Reset();
        levelLoader.ResetToFirstLevel();
    }

    private bool CanSaveHighScore()
    {
        return highScoreTable.GetSize() < highScoreTable.GetCapacity()
            || score.GetScore() > dataStorage.GetLowestScore();
    }
}
