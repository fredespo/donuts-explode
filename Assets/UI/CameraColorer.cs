using UnityEngine;
using UnityEngine.UI;
using BinaryCharm.SemanticColorPalette.Colorers.UI;
using BinaryCharm.SemanticColorPalette.Colorers;
using BinaryCharm.SemanticColorPalette;

[AddComponentMenu("Semantic Color Palette/Camera Colorer")]
[RequireComponent(typeof(Camera))]
public class CameraColorer : SCP_AColorer
{
    protected override void applyPalette(SCP_IPaletteCore rPalette)
    {
        Camera rCamera = GetComponent<Camera>();
        applyPalette(rPalette, rCamera, GetColorId());
    }

    public static void applyPalette(SCP_IPaletteCore rPalette, Camera rCamera, SCP_ColorId colorId)
    {
        if (colorId != SCP_ColorId.DO_NOT_APPLY)
        {
            rCamera.backgroundColor = rPalette.GetColor(colorId);
        }
    }
}
