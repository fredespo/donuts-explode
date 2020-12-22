using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    private static string KEY_SCORE = "Score";
    private static string KEY_LEVEL = "Level";
    private static string KEY_HIGH_SCORES = "HighScores";

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

    public List<HighScoreTable.Entry> GetHighScoreEntries()
    {
        List<HighScoreTable.Entry> highScores = new List<HighScoreTable.Entry>();
        if(PlayerPrefs.HasKey(KEY_HIGH_SCORES))
        {
            string[] entries = PlayerPrefs.GetString(KEY_HIGH_SCORES).Split(',');
            foreach(string entry in entries)
            {
                string[] entryInfo = entry.Split('|');
                if(entryInfo.Length == 2)
                {
                    string name = entryInfo[0];
                    int score;
                    if (int.TryParse(entryInfo[1], out score))
                    {
                        highScores.Add(new HighScoreTable.Entry(name, score));
                    }
                }
            }
        }
        return highScores;
    }

    public void SaveHighScore(string name, int score)
    {
        string highScores = "";
        if(PlayerPrefs.HasKey(KEY_HIGH_SCORES))
        {
            highScores = PlayerPrefs.GetString(KEY_HIGH_SCORES);
        }
        if(highScores.Length > 0)
        {
            highScores += ",";
        }
        highScores += name + "|" + score;
        PlayerPrefs.SetString(KEY_HIGH_SCORES, highScores);
    }
}
