using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class OnTap : MonoBehaviour
{
    public UnityEvent onTapStart;
    public UnityEvent onTapRelease;
    private Collider2D col;
    private bool tapStarted;

    void Start()
    {
        this.col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D tappedCollider = Physics2D.OverlapPoint(tapPoint);
            if (tappedCollider == this.col)
            {
                this.tapStarted = true;
                this.onTapStart.Invoke();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(this.tapStarted)
            {
                this.onTapRelease.Invoke();
            }
            this.tapStarted = false;
        }
    }
}
