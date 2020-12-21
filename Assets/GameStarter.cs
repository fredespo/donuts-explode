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
    private DataStorage dataStorage;

    public void Start()
    {
        if (forceStartLevel) dataStorage.SaveLevel(startLevelIndex);
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        Init();
    }

    public void Init()
    {
        if (dataStorage.GetLevel() > 0 && dataStorage.GetLevel() < levelLoader.LevelCount())
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
        int savedLevel = dataStorage.GetLevel();
        if (savedLevel >= 0 && savedLevel < levelLoader.LevelCount())
        {
            levelLoader.LoadLevel(savedLevel, startDelaySec);
        }
        else
        {
            levelLoader.LoadLevel(0, startDelaySec);
        }
    }
}
