/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2022 Binary Charm - All Rights Reserved
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@                        @@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@                           @@@@@@@@@@@@@@@@@@
             @@@@@@@@@   @@@@@@@@@@@  @@@@@        @@@@@@@@@@@@@@@
             @@@@@@@@@@@  @@@@@@@@@  @@@@@@@@@@       (@@@@@@@@@@@
             @@@@@@@@@@@@  @@@@@@@@& @@@@@@@@@@ @@@@     @@@@@@@@@
             @@@@@@@@@@@@@ @@@@@@@@@@ *@@@@@@@ @@@@@@@@@*   @@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*/

using UnityEngine;
using UnityEngine.UI;

using BinaryCharm.SemanticColorPalette.Colorers;
using BinaryCharm.SemanticColorPalette.Colorers.Others;
using BinaryCharm.SemanticColorPalette.Colorers.Renderers;
using BinaryCharm.SemanticColorPalette.Colorers.UI;

#if TEXTMESHPRO_PRESENT
using BinaryCharm.SemanticColorPalette.Colorers.TMPro;
#endif

/// <summary>
/// Namespace containing extension classes related to SCP_Colorers.
/// You might want to import this namespace if you implement your own colorers 
/// for composite objects and want to apply colors to child elements using a
/// simpler syntax.
/// 
/// e.g. if you have a Selectable element child, you can call
/// GetComponent<Selectable>().ApplySemanticPalette(rPalette)
/// in place of
/// SCP_SelectableColorer.ApplySemanticPalette(rPalette, GetComponent<Selectable>())
/// </summary>
namespace BinaryCharm.SemanticColorPalette.Extensions {

    #region Others ------------------------------------------------------------

    public static class LightExtensions {
        public static void ApplySemanticPalette(this Light t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_LightColorer.applyPalette(p, t, c);
        }
    }

    public static class TextMeshExtensions {
        public static void ApplySemanticPalette(this TextMesh t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_TextMeshColorer.applyPalette(p, t, c);
        }
    }

    #endregion ----------------------------------------------------------------

    #region Renderers ---------------------------------------------------------

    public static class LineRendererExtensions {
        public static void ApplySemanticPalette(this LineRenderer t, SCP_IPaletteCore p, SCP_ColorId[] c) {
            SCP_LineRendererColorer.applyPalette(p, t, c);
        }
    }

    public static class RendererMaterialColorer {
        public static void ApplySemanticPalette(this Renderer t, SCP_IPaletteCore p, SCP_MaterialColorDef[] c, int iMaterialIndex = 0) {
            SCP_RendererMaterialColorer.applyPalette(p, t, c, iMaterialIndex);
        }
    }

    public static class SpriteRendererExtensions {
        public static void ApplySemanticPalette(this SpriteRenderer t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_SpriteRendererColorer.applyPalette(p, t, c);
        }
    }

    public static class TrailRendererExtensions {
        public static void ApplySemanticPalette(this TrailRenderer t, SCP_IPaletteCore p, SCP_ColorId[] c) {
            SCP_TrailRendererColorer.applyPalette(p, t, c);
        }
    }

    #endregion ----------------------------------------------------------------

#if TEXTMESHPRO_PRESENT
    #region TMPro -------------------------------------------------------------

    public static partial class ButtonExtensions {
        public static void ApplySemanticPalette(this Button t, SCP_IPaletteCore p, in SCP_TMP_ButtonColorDefs c) {
            SCP_TMP_ButtonColorer.applyPalette(p, t, c);
        }
    }

    public static class TMP_DropdownExtensions {
        public static void ApplySemanticPalette(this TMPro.TMP_Dropdown t, SCP_IPaletteCore p, in SCP_TMP_DropdownColorDefs c) {
            SCP_TMP_DropdownColorer.applyPalette(p, t, c);
        }
    }

    public static class TMP_InputFieldExtensions {
        public static void ApplySemanticPalette(this TMPro.TMP_InputField t, SCP_IPaletteCore p, in SCP_TMP_InputFieldColorDefs c) {
            SCP_TMP_InputFieldColorer.applyPalette(p, t, c);
        }
    }

    public static class TMP_TextExtensions {
        public static void ApplySemanticPalette(this TMPro.TMP_Text t, SCP_IPaletteCore p, in SCP_TMP_TextColorsDef c) {
            SCP_TMP_TextColorer.applyPalette(p, t, c);
        }
        public static void ApplySemanticPalette(this TMPro.TMP_Text t, SCP_IPaletteCore p, in SCP_TMP_FontMaterialColorsDef c) {
            SCP_TMP_FontMaterialColorer.applyPalette(p, t, c);
        }
        public static void SetSemanticPaletteText(this TMPro.TMP_Text t, SCP_IPaletteCore p, in string sText) {
            SCP_TMP_RichTextColorer.SetText(p, t, sText);
        }
    }

    #endregion ----------------------------------------------------------------
#endif


    #region UI ----------------------------------------------------------------

    public static partial class ButtonExtensions {
        public static void ApplySemanticPalette(this Button t, SCP_IPaletteCore p, in SCP_ButtonColorsDef c) {
            SCP_ButtonColorer.applyPalette(p, t, c);
        }
    }

    public static class DropdownExtensions {
        public static void ApplySemanticPalette(this Dropdown t, SCP_IPaletteCore p, in SCP_DropdownColorsDef c) {
            SCP_DropdownColorer.applyPalette(p, t, c);
        }
    }

    public static class GraphicExtensions {
        public static void ApplySemanticPalette(this Graphic t, SCP_IPaletteCore p, SCP_MaterialColorDef[] c) {
            SCP_GraphicMaterialColorer.applyPalette(p, t, c);
        }
    }

    public static class ImageExtensions {
        public static void ApplySemanticPalette(this Image t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_ImageColorer.applyPalette(p, t, c);
        }
    }

    public static class InputFieldExtensions {
        public static void ApplySemanticPalette(this InputField t, SCP_IPaletteCore p, in SCP_InputFieldColorsDef c) {
            SCP_InputFieldColorer.applyPalette(p, t, c);
        }
    }

    public static class RawImageExtensions {
        public static void ApplySemanticPalette(this RawImage t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_RawImageColorer.applyPalette(p, t, c);
        }
    }

    public static class ScrollbarExtensions {
        public static void ApplySemanticPalette(this Scrollbar t, SCP_IPaletteCore p, in SCP_ScrollbarColorsDef c) {
            SCP_ScrollbarColorer.applyPalette(p, t, c);
        }
    }

    public static class SelectableExtensions {
        public static void ApplySemanticPalette(this Selectable t, SCP_IPaletteCore p, in SCP_SelectableColorsDef c) {
            SCP_SelectableColorer.applyPalette(p, t, c);
        }
    }

    public static class SliderExtensions {
        public static void ApplySemanticPalette(this Slider t, SCP_IPaletteCore p, in SCP_SliderColorsDef c) {
            SCP_SliderColorer.applyPalette(p, t, c);
        }
    }

    public static class TextExtensions {
        public static void ApplySemanticPalette(this Text t, SCP_IPaletteCore p, SCP_ColorId c) {
            SCP_TextColorer.applyPalette(p, t, c);
        }
    }

    public static class ToggleExtensions {
        public static void ApplySemanticPalette(this Toggle t, SCP_IPaletteCore p, in SCP_ToggleColorsDef c) {
            SCP_ToggleColorer.applyPalette(p, t, c);
        }
    }

    #endregion ----------------------------------------------------------------

    #region Material ----------------------------------------------------------

    public static class MaterialExtensions {
        public static void ApplySemanticPalette(this Material t, SCP_IPaletteCore p, SCP_MaterialColorDef[] c) {
            SCP_AMaterialColorer.applyPalette(p, t, c);
        }

#if TEXTMESHPRO_PRESENT
        public static void ApplySemanticPalette(this Material t, SCP_IPaletteCore p, in SCP_TMP_FontMaterialColorsDef c) {
            SCP_TMP_FontMaterialColorer.applyPalette(p, t, c);
        }
#endif
    }

    #endregion ----------------------------------------------------------------
}