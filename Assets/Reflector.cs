using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public GameObject target;
    private Camera mCamera;

    void Start()
    {
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rigidbody = col.gameObject.GetComponent<Rigidbody2D>();
        if(rigidbody != null && target != null)
        {
            Vector2 p1 = mCamera.WorldToScreenPoint(target.transform.position);
            Vector2 p2 = mCamera.WorldToScreenPoint(col.gameObject.transform.position);
            Vector3 r = col.gameObject.transform.eulerAngles;
            col.gameObject.transform.eulerAngles = new Vector3(r.x, r.y, 90 + Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI);
            rigidbody.velocity = col.gameObject.transform.up * rigidbody.velocity.magnitude;
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
