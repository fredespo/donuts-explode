using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using System;
using BinaryCharm.SemanticColorPalette.Colorers.Renderers;

public class BombPiece : MonoBehaviour
{
    public float fadeSpeed = 1;
    public float fadeDelaySec = 0.2f;
    public GameObject impactEffect;
    SpriteRenderer[] spriteRenderers;
    SCP_SpriteRendererColorer[] spriteRendererColorers;
    private bool fading = false;
    private float fadeStartTime;
    private bool caughtInMagnet = false;
    private bool inMagnetRange = false;
    private bool reflectingToBomb = false;
    private int numReflections = 0;
    private GameObject bomb;
    private Rigidbody2D rigibody;
    private AudioSource hitBombSoundEffect;
    public float hitBombSoundPitchMin = 1.0f;
    public float hitBombSoundPitchMax = 1.2f;
    private AudioSource reflectSoundEffect;
    private Action onMiss;
    private Action onFilledHole;
    private bool hitBomb = false;
    private bool leftMagnet = false;
    private Transform origParent;
    public GameObject sprinkles;

    void Awake()
    {
        this.bomb = GameObject.FindGameObjectWithTag("BombSpawn");
    }

    void Start()
    {
        this.spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        this.spriteRendererColorers = GetComponentsInChildren<SCP_SpriteRendererColorer>();
        this.rigibody = GetComponent<Rigidbody2D>();
        this.hitBombSoundEffect = GetComponent<AudioSource>();
        this.reflectSoundEffect = GameObject.FindWithTag("PieceReflectSoundEffect").GetComponent<AudioSource>();
        this.origParent = transform.parent;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.rigibody.velocity.magnitude != 0 && col.gameObject.CompareTag("bomb"))
        {
            this.hitBomb = true;
            if (!fading && !inMagnetRange)
            {
                fading = true;
                fadeStartTime = Time.time;
                this.hitBombSoundEffect.pitch = UnityEngine.Random.Range(this.hitBombSoundPitchMin, this.hitBombSoundPitchMax);
                this.hitBombSoundEffect.Play(0);
                var impulse = (UnityEngine.Random.Range(100f, 300f) * Mathf.Deg2Rad) * this.rigibody.inertia;
                this.rigibody.AddTorque(impulse, ForceMode2D.Impulse);
                Instantiate(impactEffect, col.contacts[0].point, transform.rotation);
                Color doughColor = this.spriteRenderers[0].color;
                impactEffect.GetComponent<ParticleSystem>().startColor = doughColor;
            }
        }
    }

    void FixedUpdate()
    {
        if (this.reflectingToBomb && !this.fading && !this.caughtInMagnet && this.bomb != null)
        {
            Vector2 p1 = this.bomb.transform.position;
            Vector2 p2 = gameObject.transform.position;
            if (Vector2.Distance(p1, p2) < 0.1f)
            {
                this.reflectingToBomb = false;
            }
            else
            {
                rigibody.velocity = (p1 - p2).normalized * rigibody.velocity.magnitude;
                rigibody.MoveRotation(90 + Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI);
            }
        }

        if (caughtInMagnet)
        {
            LookAt(this.bomb);
        }
    }

    void Update()
    {
        if (fading && Time.time >= this.fadeStartTime + this.fadeDelaySec)
        {
            bool isDoneFading = true;

            foreach (SpriteRenderer spriteRenderer in this.spriteRenderers)
            {
                if (spriteRenderer == null) continue;
                Color color = spriteRenderer.color;
                color.a -= fadeSpeed * Time.deltaTime;
                spriteRenderer.color = color;
                if (color.a <= 0f)
                {
                    color.a = 0f;
                }
                else
                {
                    isDoneFading = false;
                }
            }

            if (isDoneFading)
            {
                this.onMiss.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void OutOfBounds()
    {
        if (this.inMagnetRange) return;
        this.onMiss.Invoke();
        Destroy(gameObject);
    }

    private void LookAt(GameObject target)
    {
        if (target == null) return;
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

        //subtract 90 degrees because bomb pieces are always displayed at +90 degrees relative to their actual rotation due to how the sprite is defined (oops lol)
        angle -= 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000f * Time.deltaTime);
    }

    public bool CaughtInMagnet(Transform parent)
    {
        if (this.leftMagnet)
        {
            return false;
        }

        this.inMagnetRange = true;
        if (this.hitBomb)
        {
            this.hitBombSoundEffect.Stop();
            if (!caughtInMagnet)
            {
                caughtInMagnet = true;
                this.transform.SetParent(parent);
            }
        }
        return this.caughtInMagnet;
    }

    public void LeftMagnet()
    {
        this.inMagnetRange = false;
        this.caughtInMagnet = false;
        startFading();
        this.leftMagnet = true;
        transform.SetParent(this.origParent);
    }

    private void startFading()
    {
        disableColorers();
        this.fading = true;
    }

    private void StopFading()
    {
        if (!this.fading) return;

        this.fading = false;
        disableColorers();
        SetAlpha(1.0f);
    }

    private void disableColorers()
    {
        foreach (SCP_SpriteRendererColorer colorer in this.spriteRendererColorers)
        {
            colorer.enabled = false;
        }
    }

    private void SetAlpha(float value)
    {
        foreach (SpriteRenderer spriteRenderer in this.spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        }
    }

    public void ReflectToBomb()
    {
        if (this.numReflections > 0)
        {
            return;
        }

        if (!this.reflectingToBomb)
        {
            PlayBounceSoundEffect();
        }
        this.reflectingToBomb = true;
        ++this.numReflections;
    }

    private void PlayBounceSoundEffect()
    {
        this.reflectSoundEffect.pitch = UnityEngine.Random.Range(2.0f, 2.5f);
        this.reflectSoundEffect.Play();
    }

    public bool ShouldReflect()
    {
        return !this.fading;
    }

    public void SetOnMiss(Action onMiss)
    {
        this.onMiss = onMiss;
    }

    public void SetOnFilledHole(Action onFilledHole)
    {
        this.onFilledHole = onFilledHole;
    }

    public void FilledHole()
    {
        this.onFilledHole.Invoke();
    }

    public bool IsCaughtInMagnet()
    {
        return this.caughtInMagnet;
    }

    public GameObject GetSprinkles()
    {
        return this.sprinkles;
    }

    public void SetSprinkles(GameObject sprinkles)
    {
        Destroy(this.sprinkles);
        sprinkles.transform.SetParent(this.transform);
        this.sprinkles = sprinkles;
    }
}
