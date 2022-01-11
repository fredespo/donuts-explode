using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D col)
    {
        BombPiece bombPiece = col.gameObject.GetComponent<BombPiece>();
        if(bombPiece != null)
        {
            Debug.Log("Out of bounds!");
            bombPiece.OutOfBounds();
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
