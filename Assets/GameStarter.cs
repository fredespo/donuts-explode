using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public ScreenManager screenManager;
    public LevelLoader levelLoader;

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
        levelLoader.LoadLevel(0, startDelaySec);
    }
}
