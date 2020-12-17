using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTutorialText : MonoBehaviour
{
    public PieceShooter pieceShooter;

    void Update()
    {
        if(pieceShooter.GetPiecesShotCount() > 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
