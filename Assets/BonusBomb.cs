using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBomb : MonoBehaviour
{
    public GameObject explosion;
    public BombDefuzer defuzer;

    public void Explode()
    {
        Instantiate(this.explosion, transform.position, Quaternion.Euler(new Vector3(-90f, 0, 0)), transform.parent);
        Destroy(gameObject);
    }

    public bool IsDefuzed()
    {
        return this.defuzer.IsDefuzed();
    }
}
