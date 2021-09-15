﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class BombPiece : MonoBehaviour
{
    public float fadeSpeed = 1;
    SpriteRenderer spriteRenderer;
    private bool fading = false;
    private bool caughtInMagnet = false;
    private bool reflectingToBomb = false;
    private GameObject bomb;
    private Rigidbody2D rigibody;
    private AudioSource hitBombSoundEffect;
    private AudioSource reflectSoundEffect;

    void Awake()
    {
        this.bomb = GameObject.FindGameObjectWithTag("bomb");
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigibody = GetComponent<Rigidbody2D>();
        this.hitBombSoundEffect = GetComponent<AudioSource>();
        this.reflectSoundEffect = GameObject.FindWithTag("PieceReflectSoundEffect").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!fading && col.gameObject.CompareTag("bomb") && !caughtInMagnet)
        {
            fading = true;
            this.hitBombSoundEffect.pitch = Random.Range(0.25f, 0.55f);
            this.hitBombSoundEffect.Play(0);
            var impulse = (Random.Range(100f, 300f) * Mathf.Deg2Rad) * this.rigibody.inertia;
            this.rigibody.AddTorque(impulse, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if(this.reflectingToBomb && !this.fading && !this.caughtInMagnet && this.bomb != null)
        {
            Vector2 p1 = this.bomb.transform.position;
            Vector2 p2 = gameObject.transform.position;
            rigibody.velocity = (p1 - p2).normalized * rigibody.velocity.magnitude;
            rigibody.MoveRotation(90 + Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI);
        }
    }

    void Update()
    {
        if(this.bomb == null && (caughtInMagnet || reflectingToBomb))
        {
            this.bomb = GameObject.FindGameObjectWithTag("bomb");
        }

        if (caughtInMagnet)
        {
            LookAt(this.bomb);
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
        this.hitBombSoundEffect.Stop();
    }

    public void ReflectToBomb()
    {
        if (!this.reflectingToBomb)
        {
            PlayBounceSoundEffect();
        }
        this.reflectingToBomb = true;
    }

    private void PlayBounceSoundEffect()
    {
        float xPos = gameObject.transform.position.x;
        this.reflectSoundEffect.panStereo = xPos < 25 ? -1 : 1;
        this.reflectSoundEffect.pitch = Random.Range(1.5f, 2.0f);
        this.reflectSoundEffect.Play();
    }

    public bool ShouldReflect()
    {
        return !this.fading;
    }
}
