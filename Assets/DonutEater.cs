using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DonutEater : MonoBehaviour
{
    public GameObject[] bites;
    public GameObject[] donut;

    public void EatDonutAndThen(Action andThen)
    {
        StartCoroutine(AnimateBitesCoroutine(andThen));
    }

    private IEnumerator AnimateBitesCoroutine(Action andThen)
    {
        ActivateBite(0);
        yield return new WaitForSeconds(0.5f);
        ActivateBite(1);
        yield return new WaitForSeconds(0.75f);
        ActivateBite(2);
        yield return new WaitForSeconds(1.5f);
        this.bites[this.bites.Length - 1].SetActive(false);
        foreach (GameObject obj in this.donut)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        andThen.Invoke();
    }

    private void ActivateBite(int index)
    {
        for (int i = 0; i < this.bites.Length; ++i)
        {
            this.bites[i].SetActive(i == index);
        }
    }
}
