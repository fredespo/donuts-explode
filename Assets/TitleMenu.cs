using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public ScreenManager screenManager;
    public LevelLoader levelLoader;
    public GameObject newGameBtn;
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public bool forceStartLevel = false;
    public int startLevelIndex;
    public Text startButtonText;
    private DataStorage dataStorage;
    private RectTransform mainMenuTransform;

    public void Start()
    {
        if (forceStartLevel) dataStorage.SaveLevel(startLevelIndex);
        dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        mainMenuTransform = mainMenu.GetComponent<RectTransform>();
        Init();
    }

    public void Init()
    {
        mainMenu.SetActive(true);
        newGameMenu.SetActive(false);
        if (dataStorage.GetLevel() > 0 && dataStorage.GetLevel() < levelLoader.LevelCount())
        {
            newGameBtn.SetActive(true);
            startButtonText.text = "Continue";
            mainMenuTransform.localPosition = new Vector3(0, -3, 0);
        }
        else
        {
            newGameBtn.SetActive(false);
            startButtonText.text = "Start";
            mainMenuTransform.localPosition = new Vector3(0, 66, 0);
        }
    }

    public void ResetGame()
    {
        dataStorage.SaveLevel(0);
        dataStorage.SaveScore(0);
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
