using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BinaryCharm.SemanticColorPalette;

public class DonutPaletteProvider : MonoBehaviour
{
    private SCP_PaletteProvider paletteProvider;

    // Start is called before the first frame update
    void Start()
    {
        this.paletteProvider = GetComponent<SCP_PaletteProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        // on mobile if you shake the device then the palette will change
        // or if the device is not mobile then you can use the arrow keys to change the palette
        if (Input.acceleration.sqrMagnitude > 10 || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePalette(Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : 1);
        }
    }

    private void ChangePalette(int direction = 1)
    {
        int nextPaletteIndex = this.paletteProvider.GetActivePaletteIndex() + direction;
        if (nextPaletteIndex >= this.paletteProvider.GetNumPalettes())
        {
            nextPaletteIndex = 0;
        }
        else if (nextPaletteIndex < 0)
        {
            nextPaletteIndex = this.paletteProvider.GetNumPalettes() - 1;
        }
        this.paletteProvider.SetActivePaletteIndex(nextPaletteIndex);
    }
}
