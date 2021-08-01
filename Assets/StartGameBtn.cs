using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBtn : MonoBehaviour
{
    public Ads ads;
    public float delay = 0.4f;
    public TitleMenu titleMenu;
    public Detonator detonator;
    public GameObject title;
    public GameObject mainMenu;
    public GameObject newGameConfirmMenu;

    public void StartGame()
    {
        ads.ShowInterstitialAdAndThen((wasAdShown) => {
            titleMenu.StartGameAfterDelay(delay);
            detonator.activate();
            title.SetActive(false);
            mainMenu.SetActive(false);
            newGameConfirmMenu.SetActive(false);
        });
    }
}
