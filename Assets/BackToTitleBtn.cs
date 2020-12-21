using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTitleBtn : MonoBehaviour
{
    private ScreenManager screenManager;

    public void Start()
    {
        screenManager = GameObject.FindGameObjectWithTag("ScreenManager").GetComponent<ScreenManager>();
    }

    public void BackToTitle()
    {
        screenManager.ShowTitleScreen();
    }
}
