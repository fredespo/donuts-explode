using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BombHolePlacer : MonoBehaviour
{
    public Vector2 center;
    public float radius;
    public float degrees;

    void Update()
    {
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        gameObject.transform.position = new Vector3(center.x + x, center.y + y, 0);
        gameObject.transform.eulerAngles = new Vector3
        (
            0,
            0,
            degrees + 90
        );
    }
}
