using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ContinueBtn : MonoBehaviour
{
    private LevelLoader levelLoader;
    private DonutEater donutEater;
    public GameObject[] buttons;
    public GameObject[] removeOnLastBite;

    public void Start()
    {
        this.levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        GameObject donutEaterObj = GameObject.FindGameObjectWithTag("DonutEater");
        if (donutEaterObj != null)
        {
            this.donutEater = donutEaterObj.GetComponent<DonutEater>();
        }
    }

    public void Continue()
    {
        Action loadNextLevelAction = () => this.levelLoader.LoadNextLevelAndStartAfterDelay(0.1f);
        if (this.donutEater != null)
        {
            this.donutEater.EatDonutAndThen(loadNextLevelAction, () => onLastBite());
        }
        else
        {
            loadNextLevelAction.Invoke();
        }

        foreach (GameObject button in this.buttons)
        {
            button.SetActive(false);
        }
    }

    private void onLastBite() {
        foreach (GameObject obj in this.removeOnLastBite)
        {
            obj.SetActive(false);
        }
    }
}
