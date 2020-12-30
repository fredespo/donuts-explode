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
    private bool caughtInMagnet = false;
    private GameObject bomb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bomb = GameObject.FindGameObjectWithTag("bomb");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!fading && col.gameObject.CompareTag("bomb"))
        {
            fading = true;
        }
    }

    void Update()
    {
        if (caughtInMagnet)
        {
            LookAtBomb();
        }

        if (fading)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            if (color.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void LookAtBomb()
    {
        GameObject target = bomb;
        transform.LookAt(target.transform);
        Vector3 rotation = transform.eulerAngles;
        float rotationSign = target.transform.position.x > transform.position.x ? -1 : 1;
        transform.eulerAngles = new Vector3(0, 0, rotationSign * (rotation.x + 90));
    }

    void CaughtInMagnet()
    {
        fading = false;
        Color color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;
        caughtInMagnet = true;
    }
}
