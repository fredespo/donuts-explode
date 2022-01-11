using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using System;

public class BombPiece : MonoBehaviour
{
    public float fadeSpeed = 1;
    public float fadeDelaySec = 0.2f;
    SpriteRenderer spriteRenderer;
    private bool fading = false;
    private float fadeStartTime;
    private bool caughtInMagnet = false;
    private bool inMagnetRange = false;
    private bool reflectingToBomb = false;
    private int numReflections = 0;
    private GameObject bomb;
    private Rigidbody2D rigibody;
    private AudioSource hitBombSoundEffect;
    private AudioSource reflectSoundEffect;
    private Action onMiss;
    private Action onFilledHole;
    private bool hitBomb = false;
    private bool leftMagnet = false;
    private Transform origParent;

    void Awake()
    {
        this.bomb = GameObject.FindGameObjectWithTag("BombSpawn");
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigibody = GetComponent<Rigidbody2D>();
        this.hitBombSoundEffect = GetComponent<AudioSource>();
        this.reflectSoundEffect = GameObject.FindWithTag("PieceReflectSoundEffect").GetComponent<AudioSource>();
        this.origParent = transform.parent;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(this.rigibody.velocity.magnitude != 0 && col.gameObject.CompareTag("bomb"))
        {
            this.hitBomb = true;
            if (!fading && !inMagnetRange)
            {
                fading = true;
                fadeStartTime = Time.time;
                this.hitBombSoundEffect.pitch = UnityEngine.Random.Range(0.25f, 0.4f);
                this.hitBombSoundEffect.Play(0);
                var impulse = (UnityEngine.Random.Range(100f, 300f) * Mathf.Deg2Rad) * this.rigibody.inertia;
                this.rigibody.AddTorque(impulse, ForceMode2D.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        if(this.reflectingToBomb && !this.fading && !this.caughtInMagnet && this.bomb != null)
        {
            Vector2 p1 = this.bomb.transform.position;
            Vector2 p2 = gameObject.transform.position;
            if(Vector2.Distance(p1, p2) < 0.1f)
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
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            if (color.a <= 0f)
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 700f * Time.deltaTime);
    }

    public bool CaughtInMagnet(Transform parent)
    {
        if(this.leftMagnet)
        {
            return false;
        }

        this.inMagnetRange = true;
        StopFading();
        if (this.hitBomb)
        {
            this.hitBombSoundEffect.Stop();
            if(!caughtInMagnet)
            {
                caughtInMagnet = true;
                this.transform.parent = parent;
            }
        }
        return this.caughtInMagnet;
    }

    public void LeftMagnet()
    {
        this.inMagnetRange = false;
        this.caughtInMagnet = false;
        this.fading = true;
        this.leftMagnet = true;
        transform.parent = this.origParent;
    }

    private void StopFading()
    {
        fading = false;
        SetAlpha(1.0f);
    }

    private void SetAlpha(float value)
    {
        Color color = spriteRenderer.color;
        color.a = value;
        spriteRenderer.color = color;
    }

    public void ReflectToBomb()
    {
        if(this.numReflections > 0)
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
}
