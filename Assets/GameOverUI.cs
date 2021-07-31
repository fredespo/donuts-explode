using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverUI : MonoBehaviour
{
    public void ShowAfterDelay(float delaySec)
    {
        StartCoroutine(ShowAfterDelayCoroutine(delaySec));
    }

    private IEnumerator ShowAfterDelayCoroutine(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        Show();
    }

    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
