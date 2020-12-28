using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject target;
    private List<GameObject> caughtObjects = new List<GameObject>();
    private string tagToLookFor;
    private Camera mCamera;

    void Start()
    {
        tagToLookFor = transform.parent.tag;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        foreach(GameObject obj in caughtObjects)
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, transform.position, speed * Time.deltaTime);
            Vector2 p1 = mCamera.WorldToScreenPoint(GetTarget().transform.position);
            Vector2 p2 = mCamera.WorldToScreenPoint(obj.transform.position);
            Vector3 r = obj.transform.eulerAngles;
            Vector3 targetRotation = new Vector3(r.x, r.y, 90 + Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI);
            obj.transform.eulerAngles = targetRotation;
        }
    }

    private GameObject GetTarget()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("bomb");
        }
        return target;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(tagToLookFor))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if(!rb.isKinematic)
            {
                rb.isKinematic = true;
                rb.velocity = new Vector3(0, 0, 0);
                caughtObjects.Add(col.gameObject);
            }
        }
    }
}
