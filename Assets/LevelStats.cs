using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats : MonoBehaviour
{
    private int totalShots;
    private int totalGoodShots;

    public void Reset()
    {
        this.totalShots = 0;
        this.totalGoodShots = 0;
    }

    public void RecordShot()
    {
        ++this.totalShots;
    }

    public void RecordGoodShot()
    {
        ++this.totalGoodShots;
    }

    public string FormattedLevelAccuracy()
    {
        return RoundedAccuracyPercent() + "%";
    }

    private int RoundedAccuracyPercent()
    {
        float accuracy = (float)this.totalGoodShots / this.totalShots;
        return (int)System.Math.Round(accuracy * 100, 0);
    }

    public int AccuracyBonus()
    {
        return RoundedAccuracyPercent() * 5;
    }
}
