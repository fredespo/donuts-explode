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
    public AudioMixer mixer;
    private SaveData saveData;
    private BinaryFormatter formatter;
    private string saveFilePath;
    private string checksumFilePath;
    private byte[] pepper;
    private static string KEY_VOLUME_MUSIC = "MusicVolume";
    private static string KEY_VOLUME_SOUNDFX = "SoundFxVolume";

    public void Awake()
    {
        this.saveFilePath = Application.persistentDataPath + "/save.dat";
        this.checksumFilePath = Application.persistentDataPath + "/checksum.dat";
        this.pepper = Encoding.UTF8.GetBytes("j=2kE9-/Q7HTM-:U!MqygFAzkYcZvw");
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
                catch(Exception e)
                {
                    Debug.Log("Corrupt save file!");
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
            } catch(Exception e)
            {
            }

            if(vol < 0)
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
}
