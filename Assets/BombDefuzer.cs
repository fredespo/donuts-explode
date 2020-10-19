using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDefuzer : MonoBehaviour
{
    public textTimer timer;
    public Rotator bombRotator;
    public GameObject defuzedUI;
    public GameObject pieces;
    public GameObject pieceShooter;

    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            Defuze();
        }
    }

    void Defuze()
    {
        timer.enabled = false;
        bombRotator.enabled = false;
        defuzedUI.SetActive(true);
        pieces.SetActive(false);
        pieceShooter.SetActive(false);
    }
}
