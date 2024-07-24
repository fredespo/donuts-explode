using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ContinueBtn : MonoBehaviour
{
    private LevelLoader levelLoader;
    private DonutEater donutEater;

    public void Start()
    {
        this.levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        this.donutEater = GameObject.FindGameObjectWithTag("DonutEater").GetComponent<DonutEater>();
    }

    public void Continue()
    {
        this.donutEater.EatDonutAndThen(() => this.levelLoader.LoadNextLevelAndStartAfterDelay(0.1f));
    }
}
