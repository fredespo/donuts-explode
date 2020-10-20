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
    public GameObject pauseButton;
    public GameObject shootTapZone;
    public Animator fuseAnim;
    public GameObject fuseFlare;

    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            Defuze();
        }
    }

    void Defuze()
    {
        pauseButton.SetActive(false);
        shootTapZone.SetActive(false);
        timer.enabled = false;
        fuseAnim.enabled = false;
        fuseFlare.SetActive(false);
        bombRotator.enabled = false;
        defuzedUI.SetActive(true);
        pieces.SetActive(false);
        pieceShooter.SetActive(false);
    }
}
