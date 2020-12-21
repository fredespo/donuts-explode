using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDefuzer : MonoBehaviour
{
    private textTimer timer;
    public Rotator bombRotator;
    public Animator bombHighlightAnim;
    public AudioSource soundEffect;
    public GameObject defuzedUI;
    private GameObject pieces;
    private GameObject pieceShooter;
    private GameObject pauseButton;
    private GameObject shootTapZone;
    public Animator fuseAnim;
    public GameObject fuseFlare;
    private GameMusic music;
    private bool defuzed = false;
    private Score score;

    void Start()
    {
        timer = GameObject.FindGameObjectsWithTag("BombTimer")[0].GetComponent<textTimer>();
        pieces = GameObject.FindGameObjectsWithTag("PieceKeeper")[0];
        pieceShooter = GameObject.FindGameObjectsWithTag("PieceShooter")[0];
        pauseButton = GameObject.FindGameObjectsWithTag("PauseButton")[0];
        shootTapZone = GameObject.FindGameObjectsWithTag("ShootTapZone")[0];
        music = GameObject.FindGameObjectsWithTag("GameMusic")[0].GetComponent<GameMusic>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
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
            timer.Pause();
            StartCoroutine(AddTimeToScoreAfterDelay(1.0f));
            fuseAnim.enabled = false;
            fuseFlare.SetActive(false);
            bombRotator.enabled = false;
            GameObject spawnedDefuzedUI = GameObject.Instantiate(defuzedUI);
            spawnedDefuzedUI.gameObject.transform.SetParent(gameObject.transform.parent.transform, false);
            spawnedDefuzedUI.gameObject.transform.rotation = Quaternion.identity;
            pieces.SetActive(false);
            pieceShooter.SetActive(false);
            bombHighlightAnim.enabled = false;
            bombHighlightAnim.enabled = true;
            soundEffect.Play(0);
            defuzed = true;
        }
    }

    private IEnumerator AddTimeToScoreAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        score.Add((int)Mathf.Round(timer.GetSecondsLeft() * 100));
        timer.CountDownFast();
    }
}
