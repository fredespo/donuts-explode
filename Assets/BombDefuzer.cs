using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDefuzer : MonoBehaviour
{
    private textTimer timer;
    public Rotator bombRotator;
    private GameObject defuzedUI;
    private GameObject pieces;
    private GameObject pieceShooter;
    private GameObject pauseButton;
    private GameObject shootTapZone;
    public Animator fuseAnim;
    public GameObject fuseFlare;

    void Start()
    {
        timer = GameObject.FindGameObjectsWithTag("BombTimer")[0].GetComponent<textTimer>();
        defuzedUI = GameObject.FindGameObjectsWithTag("WinUI")[0];
        pieces = GameObject.FindGameObjectsWithTag("PieceKeeper")[0];
        pieceShooter = GameObject.FindGameObjectsWithTag("PieceShooter")[0];
        pauseButton = GameObject.FindGameObjectsWithTag("PauseButton")[0];
        shootTapZone = GameObject.FindGameObjectsWithTag("ShootTapZone")[0];
    }

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
        foreach(Transform child in defuzedUI.transform)
        {
            child.gameObject.SetActive(true);
        }
        pieces.SetActive(false);
        pieceShooter.SetActive(false);
    }
}
