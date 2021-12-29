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
        BlowAwayPieces();
        Destroy(gameObject);
    }

    public bool IsDefuzed()
    {
        return this.defuzer.IsDefuzed();
    }

    private void BlowAwayPieces()
    {
        GameObject pieces = GameObject.FindGameObjectWithTag("PieceKeeper");
        if (pieces != null)
        {
            foreach (Transform child in pieces.transform)
            {
                BlowAway(child.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void BlowAway(Rigidbody2D rb)
    {
        if(rb.velocity.magnitude == 0)
        {
            return;
        }
        Vector2 force = (rb.gameObject.transform.position - gameObject.transform.position).normalized * 20;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
