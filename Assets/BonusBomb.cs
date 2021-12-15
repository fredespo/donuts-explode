using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBomb : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSound;
    public BombDefuzer defuzer;

    void Start()
    {
        this.explosionSound = GameObject.FindWithTag("ExplosionSound").GetComponent<AudioSource>();
    }

    public void Explode()
    {
        Instantiate(this.explosion, transform.position, Quaternion.Euler(new Vector3(-90f, 0, 0)), transform.parent);
        this.explosionSound.Play();
        Destroy(gameObject);
    }

    public bool IsDefuzed()
    {
        return this.defuzer.IsDefuzed();
    }
}
