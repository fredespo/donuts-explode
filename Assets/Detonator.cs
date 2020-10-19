using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public GameObject explosion;
    public GameObject pieces;
    public GameObject pieceShooter;

    public void activate()
    {
        Instantiate(explosion, new Vector3(0, 1.73f, 0), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(gameObject);
        pieces.SetActive(false);
        pieceShooter.SetActive(false);
    }
}
