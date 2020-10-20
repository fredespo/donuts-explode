using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject shootTapZone;
    public Rotator bombRotator;
    public textTimer bombTimer;

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        shootTapZone.SetActive(false);
        bombRotator.enabled = false;
        bombTimer.enabled = false;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        shootTapZone.SetActive(true);
        bombRotator.enabled = true;
        bombTimer.enabled = true;
    }
}
