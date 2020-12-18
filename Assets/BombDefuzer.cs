using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDefuzer : MonoBehaviour
{
    private textTimer timer;
    public Rotator bombRotator;
    public GameObject defuzedUI;
    private GameObject pieces;
    private GameObject pieceShooter;
    private GameObject pauseButton;
    private GameObject shootTapZone;
    public Animator fuseAnim;
    public GameObject fuseFlare;
    private GameMusic music;
    private bool defuzed = false;

    void Start()
    {
        timer = GameObject.FindGameObjectsWithTag("BombTimer")[0].GetComponent<textTimer>();
        pieces = GameObject.FindGameObjectsWithTag("PieceKeeper")[0];
        pieceShooter = GameObject.FindGameObjectsWithTag("PieceShooter")[0];
        pauseButton = GameObject.FindGameObjectsWithTag("PauseButton")[0];
        shootTapZone = GameObject.FindGameObjectsWithTag("ShootTapZone")[0];
        music = GameObject.FindGameObjectsWithTag("GameMusic")[0].GetComponent<GameMusic>();
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
        if (!defuzed)
        {
            music.WindDown();
            pauseButton.SetActive(false);
            shootTapZone.SetActive(false);
            timer.enabled = false;
            fuseAnim.enabled = false;
            fuseFlare.SetActive(false);
            bombRotator.enabled = false;
            GameObject spawnedDefuzedUI = GameObject.Instantiate(defuzedUI);
            spawnedDefuzedUI.gameObject.transform.SetParent(gameObject.transform.parent.transform, false);
            spawnedDefuzedUI.gameObject.transform.rotation = Quaternion.identity;
            foreach (Transform child in spawnedDefuzedUI.transform)
            {
                child.gameObject.SetActive(true);
            }
            pieces.SetActive(false);
            pieceShooter.SetActive(false);
            defuzed = true;
        }
    }
}
