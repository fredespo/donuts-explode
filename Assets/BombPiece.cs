using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class BombPiece : MonoBehaviour
{
    public float fadeSpeed = 1;
    SpriteRenderer spriteRenderer;
    private bool fading = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!fading && col.gameObject.CompareTag("bomb"))
        {
            fading = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void Update()
    {
        if(fading)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            if(color.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
