using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauser : MonoBehaviour
{
    public GameObject tutorialText;
    public GameObject pauseUI;
    public GameObject pausedButtons;
    public GameObject quitConfirmUI;
    public GameObject shootTapZone;
    private bool tutTextWasActive;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        tutTextWasActive = tutorialText.activeSelf;
        tutorialText.SetActive(false);
        shootTapZone.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        if(tutTextWasActive)
        {
            tutorialText.SetActive(true);
        }
        shootTapZone.SetActive(true);
    }

    public void ShowConfirmQuitUI()
    {
        pausedButtons.SetActive(false);
        quitConfirmUI.SetActive(true);
    }

    public void CancelQuit()
    {
        pausedButtons.SetActive(true);
        quitConfirmUI.SetActive(false);
    }
}
