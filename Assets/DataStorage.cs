using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class DataStorage : MonoBehaviour
{
    public static int LIVES_UNLIMITED = 9999;
    public VolumeSetting volumeSettingSfx;
    public VolumeSetting volumeSettingMusic;
    private SaveData saveData;
    private BinaryFormatter formatter;
    private string saveFilePath;
    private string checksumFilePath;
    private byte[] pepper;
    private static string KEY_VOLUME_MUSIC = "MusicVolume";
    private static string KEY_VOLUME_SOUNDFX = "SoundFxVolume";
    private static string KEY_PAUSE_BUTTON_LOCATION = "PauseButtonLocation";
    private static string KEY_HAPTICS = "Haptics";

    public void Awake()
    {
        this.saveFilePath = Application.persistentDataPath + "/save.dat";
        this.checksumFilePath = Application.persistentDataPath + "/checksum.dat";
        this.pepper = Encoding.UTF8.GetBytes("j=2kE9-/Q7HTM-:U!MqygFAzkYcZvw");
        this.formatter = new BinaryFormatter();
        this.saveData = new SaveData();
        Debug.Log("Save data has perfect accuracy: " + saveData.hasPerfectAccuracy);
        LoadSaveData();
        Debug.Log("Save data has perfect accuracy: " + saveData.hasPerfectAccuracy);
    }

    private void LoadSaveData()
    {
        if (File.Exists(this.saveFilePath))
        {
            using (FileStream saveFile = new FileStream(this.saveFilePath, FileMode.Open))
            {
                try
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
                catch (Exception e)
                {
                    Debug.Log("Corrupt save file!");
                }
            }
        }
        Taptic.tapticOn = GetHapticSetting();
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
        LoadMusicVol();
    }

    public void OnLevelLost()
    {
        if (GetLives() > 0)
        {
            SetLives(GetLives() - 1);
            SaveScore(Score.CalcScoreAfterLoss(GetScore()));
        }
        else
        {
            ResetGame();
        }
        Save();
    }

    public void ResetGame()
    {
        SaveLevel(0);
        SaveScore(0);
        ResetBonusLevelsCompleted();
        SetLives(3);
        SetHasPerfectAccuracy(true);
        Save();
    }

    public int GetScore()
    {
        return this.saveData.score;
    }

    public void SaveScore(int score)
    {
        this.saveData.score = score;
    }

    public int GetLives()
    {
        return this.saveData.lives;
    }

    public void SetLives(int lives)
    {
        if (this.saveData.lives != LIVES_UNLIMITED)
        {
            this.saveData.lives = lives;
        }
    }

    public void SetLivesToInfinite()
    {
        SetLives(LIVES_UNLIMITED);
        Save();
    }

    public void RevokeInfiniteLives()
    {
        this.saveData.lives = 3;
        Save();
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

    public void ResetBonusLevelsCompleted()
    {
        this.saveData.bonusLevelsCompleted = 0;
    }

    public void IncrementBonusLevelsCompleted()
    {
        ++this.saveData.bonusLevelsCompleted;
    }

    public int GetBonusLevelsCompleted()
    {
        return this.saveData.bonusLevelsCompleted;
    }

    public void LoadMusicVol()
    {
        float musicVolPct = GetMusicVolumePct() > 0 ? (float)GetMusicVolumePct() / 100 : 0.0001f;
        float soundFxVolPct = GetSoundFxVolumePct() > 0 ? (float)GetSoundFxVolumePct() / 100 : 0.0001f;
        volumeSettingMusic.setPct(musicVolPct);
        volumeSettingSfx.setPct(soundFxVolPct);
    }

    public void SaveMusicVolumePct(int pct)
    {
        PlayerPrefs.SetString(KEY_VOLUME_MUSIC, pct.ToString());
    }

    public int GetMusicVolumePct()
    {
        int vol = 100;
        if (PlayerPrefs.HasKey(KEY_VOLUME_MUSIC))
        {
            try
            {
                vol = int.Parse(PlayerPrefs.GetString(KEY_VOLUME_MUSIC));
            }
            catch (Exception e)
            {
            }

            if (vol < 0)
            {
                vol = 0;
            }
            else if (vol > 100)
            {
                vol = 100;
            }
        }
        return vol;
    }

    public void SaveSoundFxVolumePct(int pct)
    {
        PlayerPrefs.SetString(KEY_VOLUME_SOUNDFX, pct.ToString());
    }

    public int GetSoundFxVolumePct()
    {
        int vol = 100;
        if (PlayerPrefs.HasKey(KEY_VOLUME_SOUNDFX))
        {
            try
            {
                vol = int.Parse(PlayerPrefs.GetString(KEY_VOLUME_SOUNDFX));
            }
            catch (Exception e)
            {
            }

            if (vol < 0)
            {
                vol = 0;
            }
            else if (vol > 100)
            {
                vol = 100;
            }
        }
        return vol;
    }

    public void SavePauseButtonLocation(string value)
    {
        PlayerPrefs.SetString(KEY_PAUSE_BUTTON_LOCATION, value);
    }

    public string GetPauseButtonLocation()
    {
        string location = "Left";
        if (PlayerPrefs.HasKey(KEY_PAUSE_BUTTON_LOCATION))
        {
            location = PlayerPrefs.GetString(KEY_PAUSE_BUTTON_LOCATION);
        }
        return location;
    }

    public void SaveHapticSetting(bool value)
    {
        PlayerPrefs.SetInt(KEY_HAPTICS, value ? 1 : 0);
    }

    public bool GetHapticSetting()
    {
        bool isOn = true;
        if (PlayerPrefs.HasKey(KEY_HAPTICS))
        {
            isOn = PlayerPrefs.GetInt(KEY_HAPTICS) == 1;
        }
        return isOn;
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
        return checksum;
    }

    public bool HasPerfectAccuracy()
    {
        return this.saveData.hasPerfectAccuracy;
    }

    public void SetHasPerfectAccuracy(bool value)
    {
        this.saveData.hasPerfectAccuracy = value;
    }
}
