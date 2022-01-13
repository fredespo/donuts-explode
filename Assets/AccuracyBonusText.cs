using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyBonusText : MonoBehaviour
{
    void Start()
    {
        int bonusPoints = GameObject.FindWithTag("LevelShotStats").GetComponent<LevelStats>().AccuracyBonus();
        GetComponent<Text>().text = "Bonus: " + bonusPoints + " points";
    }
}
