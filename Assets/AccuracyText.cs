using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyText : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Accuracy: " + GameObject.FindWithTag("LevelShotStats").GetComponent<LevelStats>().FormattedLevelAccuracy();
    }
}
