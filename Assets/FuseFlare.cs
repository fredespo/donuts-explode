using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseFlare : MonoBehaviour
{
    public List<Color> possibleColors;
    public float fadeSpeed = 1;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int colorIndex = (int)Mathf.Round(UnityEngine.Random.Range(0, possibleColors.Count - 1));
        spriteRenderer.color = possibleColors[colorIndex];
    }

    void Update()
    {
        if(spriteRenderer.color.a > 0)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            if (color.a < 0)
            {
                color.a = 0;
            }
            spriteRenderer.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
