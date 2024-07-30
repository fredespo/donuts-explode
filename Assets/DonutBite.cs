using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BinaryCharm.SemanticColorPalette;

public class DonutBite : MonoBehaviour
{
    public SpriteRenderer fg;
    public SpriteRenderer donut;
    public SpriteRenderer bg;

    public void setPalettes(SCP_Palette uiPalette, SCP_Palette donutPalette)
    {
        this.fg.color = uiPalette.GetColor(uiPalette.GetColorIdByName("Background"));
        this.bg.color = uiPalette.GetColor(uiPalette.GetColorIdByName("Background"));
        this.donut.color = donutPalette.GetColor(donutPalette.GetColorIdByName("Dough"));
    }

}
