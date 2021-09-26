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
    public GameObject removeAdsBtn;
    private bool pressedBefore = false;

    public void StartGame()
    {
        title.SetActive(false);
        mainMenu.SetActive(false);
        newGameConfirmMenu.SetActive(false);
        removeAdsBtn.SetActive(false);

        if (this.pressedBefore)
        {
            ads.ShowInterstitialAdAndThen((wasAdShown) => {
                JustStartGame();
            });
        }
        else
        {
            JustStartGame();
        }
        
        this.pressedBefore = true;
    }

    private void JustStartGame()
    {
        titleMenu.StartGameAfterDelay(delay);
        detonator.activate();
    }
}
