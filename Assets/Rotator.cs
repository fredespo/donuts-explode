using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool useConstSpeed = false;
    public float constSpeed = 60f;
    public RotationDir direction = RotationDir.Clockwise;
    public float Speed { get; private set; }

    public enum RotationDir
    {
        Clockwise,
        Counterclockwise,
        Random
    }
    private textTimer timer;

    public void Start()
    {
        this.Speed = this.constSpeed;
        if (this.direction == RotationDir.Random)
        {
            this.direction = UnityEngine.Random.Range(0, 2) == 0 ? RotationDir.Clockwise : RotationDir.Counterclockwise;
        }
    }

    public void Init(textTimer timer)
    {
        if (!this.useConstSpeed)
        {
            this.timer = timer;
        }
    }

    void FixedUpdate()
    {
        if (!this.useConstSpeed)
        {
            float min = 40f;
            float max = 220f;
            float elapsedRatio = this.timer != null ? this.timer.GetTimeElapsed() / this.timer.GetStartSeconds() : 0f;
            float degPerSec = min + ((max - min) * elapsedRatio);
            this.Speed = degPerSec;
        }
        transform.Rotate(0, 0, this.Speed * Time.deltaTime * (direction == RotationDir.Clockwise ? -1 : 1));
    }

    public void Reverse()
    {
        if (direction == RotationDir.Clockwise)
        {
            direction = RotationDir.Counterclockwise;
        }
        else
        {
            direction = RotationDir.Clockwise;
        }
    }

    public RotationDir GetDir()
    {
        return direction;
    }
}
