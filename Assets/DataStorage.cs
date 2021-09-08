using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

public class DataStorage : MonoBehaviour
{
    public bool deleteHighScores = false;
    public HighScoreTable highScoreTable;
    public AudioMixer mixer;
    private SaveData saveData;
    private BinaryFormatter formatter;
    private string saveFilePath;
    private string checksumFilePath;
    private byte[] pepper;
    private static string KEY_HIGH_SCORES = "HighScores";
    private static string KEY_VOLUME_MUSIC = "MusicVolume";
    private static string KEY_VOLUME_SOUNDFX = "SoundFxVolume";

    public void Awake()
    {
        this.saveFilePath = Application.persistentDataPath + "/save.dat";
        this.checksumFilePath = Application.persistentDataPath + "/checksum.dat";
        this.pepper = Encoding.UTF8.GetBytes("j=2kE9-/Q7HTM-:U!MqygFAzkYcZvw");
        Debug.Log(this.saveFilePath);
        this.formatter = new BinaryFormatter();
        this.saveData = new SaveData();
        LoadSaveData();
    }

    private void LoadSaveData()
    {
        if(File.Exists(this.saveFilePath))
        {
            using (FileStream saveFile = new FileStream(this.saveFilePath, FileMode.Open))
            {
                SaveData saveData = this.formatter.Deserialize(saveFile) as SaveData;
                if (IsSaveDataValid(saveData))
                {
                    this.saveData = saveData;
                }
                else
                {
                    Debug.Log("Invalid checksum!");
                }
            }
        }
    }

    private bool IsSaveDataValid(SaveData saveData)
    {
        string checksum = CalcChecksum(AddPepper(ObjectToByteArray(saveData)));
        return File.Exists(this.checksumFilePath) && File.ReadAllText(this.checksumFilePath).Equals(checksum);
    }

    private byte[] ObjectToByteArray(object obj)
    {
        using (var stream = new MemoryStream())
        {
            this.formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
    }

    private byte[] AddPepper(byte[] data)
    {
        return Concat(data, this.pepper);
    }

    private T[] Concat<T>(T[] first, T[] second)
    {
        if (first == null)
        {
            return second;
        }
        if (second == null)
        {
            return first;
        }

        T[] result = new T[first.Length + second.Length];
        first.CopyTo(result, 0);
        second.CopyTo(result, first.Length);

        return result;
    }

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
        return this.saveData.score;
    }

    public void SaveScore(int score)
    {
        this.saveData.score = score;
    }

    public void Save()
    {
        using (FileStream saveFile = new FileStream(this.saveFilePath, FileMode.Create))
        {
            this.formatter.Serialize(saveFile, this.saveData);
        }
        string checksum = CalcChecksum(AddPepper(File.ReadAllBytes(this.saveFilePath)));
        File.WriteAllText(this.checksumFilePath, checksum);
    }

    public int GetLevel()
    {
        return this.saveData.level;
    }

    public void SaveLevel(int levelIndex)
    {
        this.saveData.level = levelIndex;
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
            int vol = int.Parse(PlayerPrefs.GetString(KEY_VOLUME_MUSIC));
            if (vol < 0)
            {
                return 0;
            }
            else if (vol > 100)
            {
                return 100;
            }
            else
            {
                return vol;
            }
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
            int vol = int.Parse(PlayerPrefs.GetString(KEY_VOLUME_SOUNDFX));
            if(vol < 0)
            {
                return 0;
            }
            else if (vol > 100)
            {
                return 100;
            }
            else
            {
                return vol;
            }
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
        this.saveData.adsEnabled = adsEnabled;
    }

    public bool GetAdsEnabled()
    {
        return this.saveData.adsEnabled;
    }

    private string CalcChecksum(byte[] data)
    {
        SHA256Managed crypt = new SHA256Managed();
        string checksum = string.Empty;
        byte[] hash = crypt.ComputeHash(data);
        foreach (byte bit in hash)
        {
            checksum += bit.ToString("x2");
        }
        Debug.Log("checksum = " + checksum);
        return checksum;
    }
}
