using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsOnRails : MonoBehaviour
{
    private Dictionary<GameObject, MovementDetails> movementDetailsPerObj = new Dictionary<GameObject, MovementDetails>();
    private Dictionary<GameObject, Action<GameObject, float>> callbacksOnMove = new Dictionary<GameObject, Action<GameObject, float>>();

    public void Add(GameObject obj, BezierPath path, float speed)
    {
        this.movementDetailsPerObj.Add(obj, new MovementDetails(path, speed));
    }

    public void Remove(GameObject obj)
    {
        this.movementDetailsPerObj.Remove(obj);
        ClearMoveCallbacks(obj);
    }

    public void SetMoveCallback(GameObject obj, Action<GameObject, float> callback)
    {
        this.callbacksOnMove.Add(obj, callback);
    }

    public void ClearMoveCallbacks(GameObject obj)
    {
        this.callbacksOnMove.Remove(obj);
    }

    void FixedUpdate()
    {
        List<GameObject> keys = new List<GameObject>(this.movementDetailsPerObj.Keys);
        foreach (var obj in keys)
        {
            MovementDetails movementDetails = this.movementDetailsPerObj[obj];
            movementDetails.progress += movementDetails.speed * Time.deltaTime;
            this.movementDetailsPerObj[obj] = movementDetails;
            if (movementDetails.progress < 1)
            {
                Vector2 newPosOnPath = movementDetails.path.GetPosAlongPath2D(movementDetails.progress);
                obj.transform.position = new Vector3(newPosOnPath.x, newPosOnPath.y, obj.transform.position.z);
                if (this.callbacksOnMove.ContainsKey(obj))
                {
                    this.callbacksOnMove[obj].Invoke(obj, movementDetails.progress);
                }
            }
            else
            {
                this.movementDetailsPerObj.Remove(obj);
                Destroy(obj);
            }
        }
    }

    private class MovementDetails
    {
        public BezierPath path;
        public float speed; //fraction of path per second
        public float progress; //fraction of path traveled so far

        public MovementDetails(BezierPath path, float speed)
        {
            this.path = path;
            this.speed = speed;
            this.progress = 0;
        }
    }
}
