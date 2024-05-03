using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BinaryCharm.SemanticColorPalette;

public class DonutPaletteProvider : MonoBehaviour
{
    private SCP_PaletteProvider paletteProvider;
    private Vector2 startPos;
    private float minSwipeDist = 50f;

    // Start is called before the first frame update
    void Start()
    {
        this.paletteProvider = GetComponent<SCP_PaletteProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for swipe gestures on mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float swipeDist = (touch.position - startPos).magnitude;
                if (swipeDist > minSwipeDist) // minSwipeDist is a threshold for minimum swipe distance
                {
                    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                    ChangePalette(swipeValue > 0 ? 1 : -1);
                }
            }
        }
        // For non-mobile platforms, use the arrow keys to change the palette
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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
