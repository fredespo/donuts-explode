using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int degPerSec = 50;
    public RotationDir direction = RotationDir.Clockwise;
    public enum RotationDir
    {
        Clockwise,
        Counterclockwise
    }

    void Update()
    {
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
