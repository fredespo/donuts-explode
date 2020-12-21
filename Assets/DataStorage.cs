using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    private static string KEY_SCORE = "Score";
    private static string KEY_LEVEL = "Level";

    public int GetScore()
    {
        if (PlayerPrefs.HasKey(KEY_SCORE))
        {
            return PlayerPrefs.GetInt(KEY_SCORE);
        }
        else
        {
            return 0;
        }
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(KEY_SCORE, score);
    }

    public int GetLevel()
    {
        if (PlayerPrefs.HasKey(KEY_LEVEL))
        {
            return PlayerPrefs.GetInt(KEY_LEVEL);
        }
        else
        {
            return 0;
        }
    }

    public void SaveLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(KEY_LEVEL, levelIndex);
    }
}
