using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rotator rotator;
    public FuseFlareSpawner fuseFlareSpawner;
    public Animator fuseAnimator;

    public void StartBomb()
    {
        rotator.enabled = true;
        fuseFlareSpawner.enabled = true;
        fuseAnimator.enabled = true;
    }
}
