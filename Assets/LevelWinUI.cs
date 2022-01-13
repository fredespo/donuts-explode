using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelWinUI : MonoBehaviour
{
    private Animator anim;
    public GameObject accuracyText;
    public GameObject accuracyBonusText;
    public AudioSource accuracyRevealSound;

    public void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    public void ShowAccuracy(float initialDelaySec, Action andThen)
    {
        StartCoroutine(ShowAccuracyCoroutine(initialDelaySec, andThen));
    }

    private IEnumerator ShowAccuracyCoroutine(float initialDelaySec, Action andThen)
    {
        yield return new WaitForSeconds(initialDelaySec);
        this.accuracyText.SetActive(true);
        this.accuracyRevealSound.Play();
        yield return new WaitForSeconds(0.5f);
        this.accuracyBonusText.SetActive(true);
        this.accuracyRevealSound.Play();
        andThen.Invoke();
    }
}
