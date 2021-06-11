using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        BombPiece bombPiece = col.gameObject.GetComponent<BombPiece>();
        if(bombPiece != null && bombPiece.ShouldReflect())
        {
            bombPiece.ReflectToBomb();
        }
    }
}
