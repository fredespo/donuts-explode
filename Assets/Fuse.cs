using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    public textTimer bombTimer;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1 / bombTimer.GetSecondsLeft();
    }
}
