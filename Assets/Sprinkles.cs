using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BinaryCharm.SemanticColorPalette;

public class Sprinkles : MonoBehaviour
{
    public SCP_PaletteProvider paletteProvider;

    // Start is called before the first frame update
    void Start()
    {
        ColorSprinkles();
    }

    private void ColorSprinkles()
    {
        List<Color> colors = GetSprinkleColors();
        List<SpriteRenderer> renderers = GetSprinkleRenderers();
        int rand = Random.Range(1, 100);
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].color = colors[(i+rand) % colors.Count];
        }
    }

    private List<Color> GetSprinkleColors()
    {
        List<Color> colors = new List<Color>();
        SCP_Palette palette = this.paletteProvider.GetPalette();
        for (int i = 0; i < palette.GetNumElems(); i++)
        {
            string name = palette.GetColorNameByIndex(i);
            if (name.StartsWith("Sprinkles"))
            {
                colors.Add(palette.GetColorByIndex(i));
            }
        }
        return colors;
    }

    private List<SpriteRenderer> GetSprinkleRenderers()
    {
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();
        foreach (Transform child in transform)
        {
            renderers.Add(child.GetComponent<SpriteRenderer>());
        }
        return renderers;
    }
}
