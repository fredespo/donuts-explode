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
    public Rotator bombRotator;
    public textTimer bombTimer;
    private bool tutTextWasActive;

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        tutTextWasActive = tutorialText.activeSelf;
        tutorialText.SetActive(false);
        shootTapZone.SetActive(false);
        bombRotator.enabled = false;
        bombTimer.enabled = false;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        if(tutTextWasActive)
        {
            tutorialText.SetActive(true);
        }
        shootTapZone.SetActive(true);
        bombRotator.enabled = true;
        bombTimer.enabled = true;
    }

    public void ShowConfirmQuitUI()
    {
        pausedButtons.SetActive(false);
        quitConfirmUI.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void CancelQuit()
    {
        pausedButtons.SetActive(true);
        quitConfirmUI.SetActive(false);
    }
}
