﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    private LevelLoader levelLoader;

    public void Start()
    {
        this.levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    public void Continue()
    {
        this.levelLoader.LoadNextLevelAndStartAfterDelay(0.1f);
    }
}
