using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveData
{
    public int score;
    public int level;
    public bool adsEnabled;
    public int bonusLevelsCompleted;

    public SaveData()
    {
        this.score = 0;
        this.level = 0;
        this.adsEnabled = true;
        this.bonusLevelsCompleted = 0;
    }
}
