using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rotator rotator;
    public FuseFlareSpawner fuseFlareSpawner;
    public Animator fuseAnimator;
    public RectTransform fuzeTransform;

    public void StartBomb()
    {
        rotator.enabled = true;
        fuseFlareSpawner.enabled = true;
        fuseAnimator.enabled = true;
    }

    public void SetFuzePct(float fuzePct)
    {
        float newY = Mathf.Lerp(-47.9f, -48.15f, fuzePct);
        Vector3 pos = this.fuzeTransform.anchoredPosition;
        this.fuzeTransform.anchoredPosition = new Vector3(pos.x, newY, pos.z);
    }
}
