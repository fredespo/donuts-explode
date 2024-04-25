using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryBtn : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void Retry()
    {
        levelLoader.ResetCurrentLevel();
        levelLoader.StartCurrentLevelAfterDelaySec(0.1f);
    }
}
