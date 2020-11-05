using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    private textTimer bombTimer;
    private Animator anim;

    void Start()
    {
        bombTimer = GameObject.FindGameObjectsWithTag("BombTimer")[0].GetComponent<textTimer>();
        anim = GetComponent<Animator>();
        anim.speed = 1 / bombTimer.GetSecondsLeft();
    }
}
