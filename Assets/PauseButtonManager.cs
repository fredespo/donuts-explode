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
        Debug.Log("Show left pause button");
        leftPauseBtn.SetActive(true);
        rightPauseBtn.SetActive(false);
        gamePauser.SetButton(leftPauseBtn.GetComponent<Button>());
    }

    public void ShowRight()
    {
        Debug.Log("Show right pause button");
        leftPauseBtn.SetActive(false);
        rightPauseBtn.SetActive(true);
        gamePauser.SetButton(rightPauseBtn.GetComponent<Button>());
    }

    public void ShowNone()
    {
        Debug.Log("Show no pause button");
        leftPauseBtn.SetActive(false);
        rightPauseBtn.SetActive(false);
    }

    public void ApplyLocationSetting(string value)
    {
        Debug.Log("Pause button manager applying location setting: " + value);
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
