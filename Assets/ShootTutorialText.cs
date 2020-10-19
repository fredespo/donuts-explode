using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTutorialText : MonoBehaviour
{
    public PieceShooter pieceShooter;
    public GameObject bomb;

    void Update()
    {
        if(pieceShooter.GetPiecesShotCount() > 0 || bomb == null)
        {
            Destroy(gameObject);
        }
    }
}
