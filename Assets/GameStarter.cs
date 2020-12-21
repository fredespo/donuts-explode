using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public ScreenManager screenManager;
    public LevelLoader levelLoader;
    public bool forceStartLevel = false;
    public int startLevelIndex;
    public Text startButtonText;

    public void Start()
    {
        if(forceStartLevel) PlayerPrefs.SetInt("Level", startLevelIndex);
        Init();
    }

    public void Init()
    {
        if (HasValidSavedLevel())
        {
            startButtonText.text = "Continue";
        }
        else
        {
            startButtonText.text = "Start";
        }
    }

    public void StartGameAfterDelay(float loadDelaySec)
    {
        StartGameAfterDelay(loadDelaySec, 0.75f);
    }

    public void StartGameAfterDelay(float loadDelaySec, float startDelaySec)
    {
        StartCoroutine(StartGameAfterDelayImpl(loadDelaySec, startDelaySec));
    }

    IEnumerator StartGameAfterDelayImpl(float loadDelaySec, float startDelaySec)
    {
        yield return new WaitForSeconds(loadDelaySec);
        screenManager.ShowGameScreen();
        if(HasValidSavedLevel())
        {
            levelLoader.LoadLevel(PlayerPrefs.GetInt("Level"), startDelaySec);
        }
        else
        {
            levelLoader.LoadLevel(0, startDelaySec);
        }
    }

    private bool HasValidSavedLevel()
    {
        return PlayerPrefs.HasKey("Level") && PlayerPrefs.GetInt("Level") > 0 && PlayerPrefs.GetInt("Level") < levelLoader.LevelCount();
    }
}
