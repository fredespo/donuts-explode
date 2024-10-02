using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelWinUI : MonoBehaviour
{
    public GameObject accuracyText;
    public GameObject accuracyBonusText;

    public void ShowAccuracy(float initialDelaySec, Action andThen = null)
    {
        StartCoroutine(ShowAccuracyCoroutine(initialDelaySec, andThen));
    }

    private IEnumerator ShowAccuracyCoroutine(float initialDelaySec, Action andThen)
    {
        yield return new WaitForSeconds(initialDelaySec);
        this.accuracyText.SetActive(true);
        this.accuracyBonusText.SetActive(true);
        if (andThen != null) andThen.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
