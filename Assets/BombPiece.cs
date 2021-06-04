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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            LookAt(GameObject.FindGameObjectWithTag("bomb"));
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

    private void LookAt(GameObject target)
    {
        if (target == null) return;
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
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
