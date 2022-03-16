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
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        Init();
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
    }

    public void Init()
    {
        int colorIndex = (int)Mathf.Round(UnityEngine.Random.Range(0, possibleColors.Count - 1));
        spriteRenderer.color = possibleColors[colorIndex];
    }
}
