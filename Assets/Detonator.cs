using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionParent;
    public bool destroyOnDetonation = true;
    private AudioSource explosionSound;
    private GameObject pieces;
    private GameObject pieceShooter;
    private Score score;
    private GameOverUI gameOverUI;
    private Animator camAnim;

    public void Start()
    {
        explosionSound = GameObject.FindGameObjectWithTag("ExplosionSound").GetComponent<AudioSource>();
        pieces = GameObject.FindGameObjectWithTag("PieceKeeper");
        pieceShooter = GameObject.FindGameObjectWithTag("PieceShooter");
        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        if (scoreObj != null)
        {
            score = scoreObj.GetComponent<Score>();
        }
        GameObject gameOverUiObj = GameObject.FindGameObjectWithTag("GameOverUI");
        if(gameOverUiObj != null)
        {
            gameOverUI = gameOverUiObj.GetComponent<GameOverUI>();
        }
    }

    public void activate()
    {
        Time.timeScale = 1.0f;
        camAnim.SetBool("slowmo", false);
        if (score != null && score.GetScore() > 0)
        {
            score.AddAfterDelay(-200, 1.5f);
            if (gameOverUI != null) gameOverUI.ShowAfterDelay(2.5f);
        }
        else if (gameOverUI != null) gameOverUI.ShowAfterDelay(1.0f);

        GameObject spawnedExplosion = Instantiate(explosion, gameObject.transform.parent, false);
        if(explosionParent != null)
        {
            spawnedExplosion.gameObject.transform.SetParent(explosionParent.gameObject.transform);
        }
        explosionSound.Play(0);
        if(pieceShooter != null) pieceShooter.GetComponent<PieceShooter>().Inactivate();
        if (pieces != null)
        {
            foreach(Transform child in pieces.transform)
            {
                BlowAway(child.GetComponent<Rigidbody2D>());
            }
        }
        if(destroyOnDetonation)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void BlowAway(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        Vector2 force = (rb.gameObject.transform.position - gameObject.transform.position).normalized * 20;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
