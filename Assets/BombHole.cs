using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHole : MonoBehaviour
{
    void Start()
    {
        GetComponent<BombHolePlacer>().enabled = false;
    }
}
