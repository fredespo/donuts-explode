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
        anim = GetComponent<Animator>();
        GameObject bombTimerObj = GameObject.FindGameObjectWithTag("BombTimer");
        if(bombTimerObj != null)
        {
            bombTimer = bombTimerObj.GetComponent<textTimer>();
            anim.speed = 1 / bombTimer.GetSecondsLeft();
        }
        else
        {
            anim.speed = 0;
        }
    }
}
