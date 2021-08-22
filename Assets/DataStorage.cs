using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DataStorage : MonoBehaviour
{
    public bool deleteHighScores = false;
    public HighScoreTable highScoreTable;
    public AudioMixer mixer;
    private static string KEY_SCORE = "Score";
    private static string KEY_LEVEL = "Level";
    private static string KEY_HIGH_SCORES = "HighScores";
    private static string KEY_VOLUME_MUSIC = "MusicVolume";
    private static string KEY_VOLUME_SOUNDFX = "SoundFxVolume";
    private static string KEY_ADS_ENABLED = "AdsEnabled";

    public void Start()
    {
        if(deleteHighScores)
        {
            PlayerPrefs.DeleteKey(KEY_HIGH_SCORES);
        }
        LoadMusicVol();
    }

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
            highScores = DeserializeHighScores(PlayerPrefs.GetString(KEY_HIGH_SCORES));
        }
        highScores.Sort(delegate (HighScoreTable.Entry entry1, HighScoreTable.Entry entry2)
        {
            if (entry2.GetScore() != entry1.GetScore())
            {
                return entry2.GetScore().CompareTo(entry1.GetScore());
            }
            else
            {
                return entry1.GetName().CompareTo(entry2.GetName());
            }
        });
        return highScores;
    }

    public void SaveHighScore(string name, int score)
    {
        List<HighScoreTable.Entry> entries = GetHighScoreEntries();
        if(entries.Count == highScoreTable.GetCapacity() && score <= entries[entries.Count - 1].GetScore())
        {
            return;
        }

        if(entries.Count >= highScoreTable.GetCapacity())
        {
            entries.RemoveAt(entries.Count - 1);
        }
        entries.Add(new HighScoreTable.Entry(name, score));
        PlayerPrefs.SetString(KEY_HIGH_SCORES, SerializeHighScores(entries));
    }

    public int GetLowestScore()
    {
        List<HighScoreTable.Entry> entries = GetHighScoreEntries();
        if(entries.Count == 0)
        {
            return 0;
        }
        return entries[entries.Count - 1].GetScore();
    }

    public void LoadMusicVol()
    {
        float musicVol = GetMusicVolumePct() > 0 ? (float)GetMusicVolumePct() / 100 : 0.0001f;
        float soundFxVol = GetSoundFxVolumePct() > 0 ? (float)GetSoundFxVolumePct() / 100 : 0.0001f;
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
        mixer.SetFloat("SoundFxVolume", Mathf.Log10(soundFxVol) * 20);
    }

    public void SaveMusicVolumePct(int pct)
    {
        PlayerPrefs.SetString(KEY_VOLUME_MUSIC, pct.ToString());
    }

    public int GetMusicVolumePct()
    {
        if (PlayerPrefs.HasKey(KEY_VOLUME_MUSIC))
        {
            return int.Parse(PlayerPrefs.GetString(KEY_VOLUME_MUSIC));
        }
        else
        {
            return 100;
        }
    }

    public void SaveSoundFxVolumePct(int pct)
    {
        PlayerPrefs.SetString(KEY_VOLUME_SOUNDFX, pct.ToString());
    }

    public int GetSoundFxVolumePct()
    {
        if (PlayerPrefs.HasKey(KEY_VOLUME_SOUNDFX))
        {
            return int.Parse(PlayerPrefs.GetString(KEY_VOLUME_SOUNDFX));
        }
        else
        {
            return 100;
        }
    }

    private string SerializeHighScores(List<HighScoreTable.Entry> entries)
    {
        string serialized = "";
        for(int i = 0; i < entries.Count; ++i)
        {
            serialized += entries[i].GetName() + "|" + entries[i].GetScore();
            if(i < entries.Count - 1)
            {
                serialized += ",";
            }
        }
        return serialized;
    }

    private List<HighScoreTable.Entry> DeserializeHighScores(string serialized)
    {
        List<HighScoreTable.Entry> highScores = new List<HighScoreTable.Entry>();
        string[] entries = serialized.Split(',');
        foreach (string entry in entries)
        {
            string[] entryInfo = entry.Split('|');
            if (entryInfo.Length == 2)
            {
                string name = entryInfo[0];
                int score;
                if (int.TryParse(entryInfo[1], out score))
                {
                    highScores.Add(new HighScoreTable.Entry(name, score));
                }
            }
        }
        return highScores;
    }

    public void SaveAdsEnabled(bool adsEnabled)
    {
        PlayerPrefs.SetString(KEY_ADS_ENABLED, adsEnabled.ToString());
    }

    public bool GetAdsEnabledOrDefault(bool defaultVal)
    {
        if (PlayerPrefs.HasKey(KEY_ADS_ENABLED))
        {
            return bool.Parse(PlayerPrefs.GetString(KEY_ADS_ENABLED));
        }
        else
        {
            return defaultVal;
        }
    }
}
