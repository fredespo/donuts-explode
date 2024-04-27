using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBtn : MonoBehaviour
{
    public float delay = 0.4f;
    public TitleMenu titleMenu;
    public Detonator detonator;
    public GameObject title;
    public GameObject mainMenu;
    public GameObject newGameConfirmMenu;
    private bool pressedBefore = false;

    public void StartGame()
    {
        title.SetActive(false);
        mainMenu.SetActive(false);
        newGameConfirmMenu.SetActive(false);
        titleMenu.StartGameAfterDelay(delay);
        detonator.activate();
    }
}
