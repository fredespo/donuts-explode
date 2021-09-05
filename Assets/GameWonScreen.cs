using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameWonScreen : MonoBehaviour
{
    public UnityEvent onInit;
    public UnityEvent onAutoSaveScore;
    public UnityEvent onFailedAutoSaveScore;
    public UnityEvent onScoreSaved;
    public UnityEvent onScoreSaveError;
    public UnityEvent onExit;
    public Score score;
    public Text scoreText;
    public GooglePlayServices playServices;
    private bool autoSavedHighScore;

    public void Init()
    {
        onInit.Invoke();
        scoreText.text = score.GetScore().ToString();
        autoSavedHighScore = false;
        if (playServices.IsSignedIn())
        {
            playServices.PostHighScoreAndThen(score.GetScore(), (success) =>
            {
                if (success)
                {
                    this.autoSavedHighScore = true;
                    onAutoSaveScore.Invoke();
                }
                else
                {
                    onFailedAutoSaveScore.Invoke();
                }
            });
        }
        else
        {
            onFailedAutoSaveScore.Invoke();
        }
    }

    public void PostScore()
    {
        playServices.PostHighScoreAndThen(score.GetScore(), (success) =>
        {
            if (success)
            {
                onScoreSaved.Invoke();
            }
            else
            {
                onScoreSaveError.Invoke();
            }
        });
    }

    public void OnExit()
    {
        onExit.Invoke();
    }
}
