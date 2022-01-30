using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonManager : MonoBehaviour
{
    public GameObject leftPauseBtn;
    public GameObject rightPauseBtn;
    public GamePauser gamePauser;

    public void ShowLeft()
    {
        leftPauseBtn.SetActive(true);
        rightPauseBtn.SetActive(false);
        gamePauser.SetButton(leftPauseBtn.GetComponent<Button>());
    }

    public void ShowRight()
    {
        leftPauseBtn.SetActive(false);
        rightPauseBtn.SetActive(true);
        gamePauser.SetButton(rightPauseBtn.GetComponent<Button>());
    }

    public void ShowNone()
    {
        leftPauseBtn.SetActive(false);
        rightPauseBtn.SetActive(false);
    }

    public void ApplyLocationSetting(string value)
    {
        switch(value)
        {
            case "Right":
                ShowRight();
                break;

            case "None":
                ShowNone();
                break;

            case "Left":
            default:
                ShowLeft();
                break;
        }
    }
}
