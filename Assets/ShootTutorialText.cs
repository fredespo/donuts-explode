using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootTutorialText : MonoBehaviour
{
    public PieceShooter pieceShooter;
    private Text text;
    private Color textColorVisible;
    private Color textColorInvisible;

    void Start()
    {
        pieceShooter = GameObject.FindGameObjectsWithTag("PieceShooter")[0].GetComponent<PieceShooter>();
        text = gameObject.GetComponent<Text>();
        textColorVisible = text.color;
        textColorInvisible = new Color(textColorVisible.r, textColorVisible.g, textColorVisible.b, 0f);
    }

    void Update()
    {
        if(pieceShooter.GetPiecesShotCount() > 0)
        {
            text.color = textColorInvisible;
        }
        else
        {
            text.color = textColorVisible;
        }
    }
}
