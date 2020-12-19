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
        GameObject[] bombTimers = GameObject.FindGameObjectsWithTag("BombTimer");
        if(bombTimers.Length > 0)
        {
            bombTimer = GameObject.FindGameObjectsWithTag("BombTimer")[0].GetComponent<textTimer>();
            anim.speed = 1 / bombTimer.GetSecondsLeft();
        }
        else
        {
            anim.speed = 0;
        }
    }
}
