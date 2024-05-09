using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BinaryCharm.SemanticColorPalette;

public class DonutPaletteProvider : MonoBehaviour
{
    public bool randomizePaletteOnStart = true;
    private SCP_PaletteProvider paletteProvider;
    private Vector2 touchStart;

    void Start()
    {
        this.paletteProvider = GetComponent<SCP_PaletteProvider>();
        if (this.randomizePaletteOnStart) changePaletteToRandom();
    }

    void Update()
    {
        changePaletteWithSwipe(50f);
        changePaletteWithKeys(KeyCode.LeftArrow, KeyCode.RightArrow);
    }

    private void changePaletteToRandom()
    {
        this.paletteProvider.SetActivePaletteIndex(
            Random.Range(0, this.paletteProvider.GetNumPalettes())
        );
    }

    private void changePaletteWithKeys(KeyCode keyCodeDec, KeyCode keyCodeInc)
    {
        if (Input.GetKeyDown(keyCodeDec) || Input.GetKeyDown(keyCodeInc))
        {
            ChangePalette(Input.GetKeyDown(keyCodeDec) ? -1 : 1);
        }
    }

    private void changePaletteWithSwipe(float minSwipeDist)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float swipeDist = (touch.position - touchStart).magnitude;
                if (swipeDist > minSwipeDist)
                {
                    float swipeValue = Mathf.Sign(touch.position.x - touchStart.x);
                    ChangePalette(swipeValue > 0 ? 1 : -1);
                }
            }
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
