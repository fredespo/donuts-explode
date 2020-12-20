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

    public void Start()
    {
        explosionSound = GameObject.FindGameObjectsWithTag("ExplosionSound")[0].GetComponent<AudioSource>();
        GameObject[] pieceKeepers = GameObject.FindGameObjectsWithTag("PieceKeeper");
        if(pieceKeepers.Length > 0)
        {
            pieces = GameObject.FindGameObjectsWithTag("PieceKeeper")[0];
            pieceShooter = GameObject.FindGameObjectsWithTag("PieceShooter")[0];
        }
    }

    public void activate()
    {
        GameObject spawnedExplosion = Instantiate(explosion, gameObject.transform.parent, false);
        if(explosionParent != null)
        {
            spawnedExplosion.gameObject.transform.SetParent(explosionParent.gameObject.transform);
        }
        explosionSound.Play(0);
        if(pieces != null) pieces.SetActive(false);
        if(pieceShooter != null) pieceShooter.SetActive(false);
        if(destroyOnDetonation)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
