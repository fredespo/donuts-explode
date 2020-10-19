using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BombHolePlacer : MonoBehaviour
{
    public GameObject center;
    public GameObject edge;
    public float degrees;

    void Update()
    {
        float radius = Vector2.Distance(center.transform.position, edge.transform.position);
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        gameObject.transform.position = new Vector3(center.transform.position.x + x, center.transform.position.y + y, 0);
        gameObject.transform.eulerAngles = new Vector3
        (
            0,
            0,
            degrees + 90
        );
    }
}
