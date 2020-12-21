using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameMenu : MonoBehaviour
{
    public GameObject buttons;

    public void HideButtons()
    {
        buttons.SetActive(false);
    }

    public void ShowButtonsAfterDelay(float delaySec)
    {
        StartCoroutine(ShowButtonsCoroutine(delaySec));
    }

    private IEnumerator ShowButtonsCoroutine(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        buttons.SetActive(true);
    }
}
