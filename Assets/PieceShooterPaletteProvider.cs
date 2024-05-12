using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooterPaletteProvider : MonoBehaviour
{
    private int activePaletteIndex = 0;

    public void SetActivePaletteIndex(int paletteIndex)
    {
        this.activePaletteIndex = paletteIndex;
    }

    public int GetActivePaletteIndex()
    {
        return this.activePaletteIndex;
    }
}
