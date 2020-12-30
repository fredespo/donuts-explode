using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed = 1.0f;
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
            if(obj != null)
            {
                obj.SendMessage("CaughtInMagnet");
                MoveTowardsMagnet(obj);
            }
        }
    }

    private void MoveTowardsMagnet(GameObject obj)
    {
        Vector3 objPos = obj.transform.position;
        float xOffset = GetOffsetX(objPos);
        float yOffSet = GetOffsetY(objPos);
        obj.transform.position = new Vector3(objPos.x + xOffset, objPos.y + yOffSet, objPos.z);
    }

    private float GetOffsetX(Vector3 otherPos)
    {
        float otherX = otherPos.x;
        float thisX = transform.position.x;
        float offset = speed * Time.deltaTime;
        if(Mathf.Abs(otherX - thisX) < offset)
        {
            offset = Mathf.Abs(otherX - thisX);
        }
        return otherX > thisX ? -offset : offset;
    }

    private float GetOffsetY(Vector3 otherPos)
    {
        float otherY = otherPos.y;
        float thisY = transform.position.y;
        float offset = speed * Time.deltaTime;
        if (Mathf.Abs(otherY - thisY) < offset)
        {
            offset = Mathf.Abs(otherY - thisY);
        }
        return otherY > thisY ? -offset : offset;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(tagToLookFor))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
                rb.velocity = new Vector3(0, 0, 0);
                caughtObjects.Add(col.gameObject);
            }
        }
    }
}
