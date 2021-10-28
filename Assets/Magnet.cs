using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed = 1.0f;
    private List<GameObject> caughtObjects = new List<GameObject>();
    private string tagToLookFor;
    private Camera mCamera;
    private BombHole hole;

    void Start()
    {
        tagToLookFor = transform.parent.tag;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hole = transform.parent.GetComponent<BombHole>();
    }

    void Update()
    {
        foreach(GameObject obj in caughtObjects)
        {
            if(obj != null)
            {
                if(obj.GetComponent<BombPiece>().CaughtInMagnet())
                {
                    MoveTowardsMagnet(obj);
                    RotateTowardsMagnet(obj);
                }
            }
        }
    }

    private void MoveTowardsMagnet(GameObject obj)
    {
        Vector3 objPos = obj.transform.position;
        float offsetX = GetMovementX(objPos);
        float offsetY = GetMovementY(objPos);
        
        if (Mathf.Abs(objPos.x - transform.position.x) + Mathf.Abs(objPos.y - transform.position.y) < 0.08f)
        {
            this.hole.fillWith(obj);
        }
        else
        {
            obj.transform.position = new Vector3(objPos.x + offsetX, objPos.y + offsetY, objPos.z);
        }
    }

    private void RotateTowardsMagnet(GameObject obj)
    {
        Vector3 rotationVector = obj.transform.position - transform.position;
        if(rotationVector.sqrMagnitude > 0.001)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotationVector);
            Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 0.5f);
        }
    }

    private float GetMovementX(Vector3 otherPos)
    {
        float offset = speed * Time.deltaTime;
        float diff = Mathf.Abs(otherPos.x - transform.position.x);
        if (diff < offset)
        {
            offset = diff;
        }
        return otherPos.x > transform.position.x ? -offset : offset;
    }

    private float GetMovementY(Vector3 otherPos)
    {
        float offset = speed * Time.deltaTime;
        float diff = Mathf.Abs(otherPos.y - transform.position.y);
        if (diff < offset)
        {
            offset = diff;
        }
        return otherPos.y > transform.position.y ? -offset : offset;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(tagToLookFor))
        {
            caughtObjects.Add(col.gameObject);
        }
    }

    public void FillWithFirstCaught()
    {
        GameObject firstCaught = this.caughtObjects[0];
        if (firstCaught != null)
        {
            this.hole.fillWith(firstCaught);
        }
    }
}
