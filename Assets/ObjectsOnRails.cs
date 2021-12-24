using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsOnRails : MonoBehaviour
{
    private Dictionary<GameObject, MovementDetails> movementDetailsPerObj = new Dictionary<GameObject, MovementDetails>();
    private Dictionary<GameObject, Action<GameObject, float, BezierPath[], int>> callbacksOnMove = new Dictionary<GameObject, Action<GameObject, float, BezierPath[], int>>();

    public void Add(GameObject obj, BezierPath[] paths, float[] speeds)
    {
        this.movementDetailsPerObj.Add(obj, new MovementDetails(paths, speeds));
    }

    public void Remove(GameObject obj)
    {
        this.movementDetailsPerObj.Remove(obj);
        ClearMoveCallbacks(obj);
    }

    public void SetMoveCallback(GameObject obj, Action<GameObject, float, BezierPath[], int> callback)
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
            movementDetails.progress += movementDetails.speeds[movementDetails.pathIdx] * Time.deltaTime;
            this.movementDetailsPerObj[obj] = movementDetails;
            if (movementDetails.progress < 1)
            {
                Vector2 newPosOnPath = movementDetails.CurrPath().GetPosAlongPath2D(movementDetails.progress);
                obj.transform.position = new Vector3(newPosOnPath.x, newPosOnPath.y, obj.transform.position.z);
                if (this.callbacksOnMove.ContainsKey(obj))
                {
                    this.callbacksOnMove[obj].Invoke(obj, movementDetails.progress, movementDetails.paths, movementDetails.pathIdx);
                }
            }
            else if(movementDetails.HasNextPath())
            {
                movementDetails.GoToNextPath();
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
        public BezierPath[] paths;
        public int pathIdx;
        public float[] speeds; //fraction of path per second
        public float progress; //fraction of path traveled so far

        public MovementDetails(BezierPath[] paths, float[] speeds)
        {
            this.paths = paths;
            this.speeds = speeds;
            this.progress = 0;
            this.pathIdx = 0;
        }

        public BezierPath CurrPath()
        {
            return this.paths[this.pathIdx];
        }

        public bool HasNextPath()
        {
            return this.pathIdx < this.paths.Length - 1;
        }

        public void GoToNextPath()
        {
            ++this.pathIdx;
            this.progress = 0;
        }
    }
}
