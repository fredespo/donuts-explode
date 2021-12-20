using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BombHolePlacer : MonoBehaviour
{
    public GameObject center;
    public GameObject edge;
    public float radiusOffset;
    public Vector2 centerOffset;
    public float degrees;

    void Update()
    {
        if(center == null)
        {
            return;
        }
        Vector2 centerPos = new Vector2(this.center.transform.position.x + this.centerOffset.x, this.center.transform.position.y + this.centerOffset.y);
        float radius = Vector2.Distance(centerPos, edge.transform.position) + this.radiusOffset;
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        gameObject.transform.position = new Vector3(centerPos.x + x, centerPos.y + y, 0);
        gameObject.transform.eulerAngles = new Vector3
        (
            0,
            0,
            degrees + 90
        );
    }
}
