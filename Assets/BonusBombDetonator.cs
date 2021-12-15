using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BonusBombDetonator : MonoBehaviour
{
    private Collider2D collider;

    void Start()
    {
        this.collider = GetComponent<Collider2D>();
    }


    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collidedWith)
    {
        if(collidedWith.tag == "BonusBomb")
        {
            collidedWith.gameObject.GetComponent<BonusBomb>().Explode();
        }
    }
}
