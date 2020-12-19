using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public ScreenManager screenManager;
    public LevelLoader levelLoader;


    public void StartGameAfterDelay(float delaySec)
    {
        StartCoroutine(StartGameAfterDelayImpl(delaySec));
    }

    IEnumerator StartGameAfterDelayImpl(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        screenManager.ShowGameScreen();
        levelLoader.LoadLevel(0);
    }
}
