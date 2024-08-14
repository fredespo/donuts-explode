using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using BinaryCharm.SemanticColorPalette;

public class DonutEater : MonoBehaviour
{
    public SCP_PaletteProvider uiPaletteProvider;
    public SCP_PaletteProvider donutPaletteProvider;
    public DonutBite[] bites;
    public GameObject[] donut;
    public AudioSource biteSound;

    public void EatDonutAndThen(Action andThen, Action onLastBite)
    {
        for (int i = 0; i < this.bites.Length; ++i)
        {
            SCP_Palette uiPal = this.uiPaletteProvider.GetPalette();
            SCP_Palette donutPal = this.donutPaletteProvider.GetPalette();
            this.bites[i].setPalettes(uiPal, donutPal);
        }
        StartCoroutine(AnimateBitesCoroutine(andThen, onLastBite));
    }

    private IEnumerator AnimateBitesCoroutine(Action andThen, Action onLastBite)
    {
        ActivateBite(0);
        yield return new WaitForSeconds(0.6f);
        ActivateBite(1);
        yield return new WaitForSeconds(0.6f);
        ActivateBite(2);
        yield return new WaitForSeconds(0.8f);
        playBiteSound();
        this.bites[this.bites.Length - 1].gameObject.SetActive(false);
        foreach (GameObject obj in this.donut)
        {
            obj.SetActive(false);
        }
        onLastBite.Invoke();
        yield return new WaitForSeconds(1.0f);
        andThen.Invoke();
    }

    private void ActivateBite(int index)
    {
        playBiteSound();
        for (int i = 0; i < this.bites.Length; ++i)
        {
            this.bites[i].gameObject.SetActive(i == index);
        }
    }

    private void playBiteSound()
    {
        biteSound.pitch = UnityEngine.Random.Range(0.9f, 1.0f);
        biteSound.Play();
    }
}
