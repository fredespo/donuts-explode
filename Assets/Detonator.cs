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
    private Animator camAnim;

    public void Start()
    {
        explosionSound = GameObject.FindGameObjectWithTag("ExplosionSound").GetComponent<AudioSource>();
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    public void Init(GameObject pieceShooter)
    {
        this.pieceShooter = pieceShooter;
    }

    public void activate()
    {
        Taptic.Heavy();
        Time.timeScale = 1.0f;
        if (camAnim != null) camAnim.SetBool("slowmo", false);

        GameObject spawnedExplosion = Instantiate(explosion, gameObject.transform.parent, false);
        if (explosionParent != null)
        {
            spawnedExplosion.gameObject.transform.SetParent(explosionParent.gameObject.transform);
        }
        explosionSound.Play(0);
        if (pieceShooter != null)
        {
            PieceShooter pieceShooterComp = pieceShooter.GetComponent<PieceShooter>();
            pieceShooterComp.ResetConsecutiveShots();
            pieceShooterComp.Inactivate();
        }
        pieces = GameObject.FindGameObjectWithTag("PieceKeeper");
        if (pieces != null)
        {
            foreach (Transform child in pieces.transform)
            {
                BlowAway(child.GetComponent<Rigidbody2D>());
            }
        }
        if (destroyOnDetonation)
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
