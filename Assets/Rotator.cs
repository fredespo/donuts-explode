using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public RotationDir direction = RotationDir.Clockwise;
    public enum RotationDir
    {
        Clockwise,
        Counterclockwise
    }
    private textTimer timer;

    void Start()
    {
        this.timer = GameObject.FindWithTag("BombTimer").GetComponent<textTimer>();
    }

    void Update()
    {
        float min = 110f;
        float max = 235f;
        float elapsedRatio = this.timer.GetTimeElapsed() / this.timer.GetStartSeconds();
        float degPerSec = min + ((max - min) * elapsedRatio);
        transform.Rotate(0, 0, degPerSec * Time.deltaTime * (direction == RotationDir.Clockwise ? -1 : 1));
    }

    public void Reverse()
    {
        if(direction == RotationDir.Clockwise)
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
