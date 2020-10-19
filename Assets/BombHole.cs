using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombHole : MonoBehaviour
{
    void Start()
    {
        GetComponent<BombHolePlacer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(gameObject.tag))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
