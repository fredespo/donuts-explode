using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsOnRails : MonoBehaviour
{
    private Dictionary<GameObject, MovementDetails> movementDetailsPerObj = new Dictionary<GameObject, MovementDetails>();
    private Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>> callbacksOnMove = new Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>>();
    private Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>> callbacksOnPathChange = new Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>>();
    private Vector3 centerPos;
    private Vector3 rightPos;
    private Vector3 topPos;
    private float width;
    private float height;
    public float targetWidth = 6.75f;
    public float targetHeight = 12f;
    private float widthRatio;
    private float heightRatio;

    void Start()
    {
        this.centerPos = GameObject.FindWithTag("center").transform.position;
        this.rightPos = GameObject.FindWithTag("right").transform.position;
        this.topPos = GameObject.FindWithTag("top").transform.position;
        this.width = (this.rightPos.x - this.centerPos.x) * 2;
        this.height = (this.topPos.y - this.centerPos.y) * 2;
        this.widthRatio = this.width / this.targetWidth;
        this.heightRatio = this.height / this.targetHeight;
    }

    public void Add(GameObject obj, BonusBombMovement[] movements)
    {
        this.movementDetailsPerObj.Add(obj, new MovementDetails(movements));
    }

    public void Remove(GameObject obj)
    {
        this.movementDetailsPerObj.Remove(obj);
        ClearMoveCallbacks(obj);
    }

    public void SetMoveCallback(GameObject obj, Action<ObjectsOnRails.MovementDetails> callback)
    {
        this.callbacksOnMove.Add(obj, callback);
    }

    public void SetPathChangeCallback(GameObject obj, Action<ObjectsOnRails.MovementDetails> callback)
    {
        this.callbacksOnPathChange.Add(obj, callback);
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
            movementDetails.progress += movementDetails.movements[movementDetails.idx].speed * Time.deltaTime;
            this.movementDetailsPerObj[obj] = movementDetails;
            if (movementDetails.progress < 1)
            {
                Vector2 newPosOnPath = movementDetails.CurrPath().GetPosAlongPath2D(movementDetails.progress, this.centerPos, this.widthRatio, this.heightRatio);
                obj.transform.position = new Vector3(newPosOnPath.x, newPosOnPath.y, obj.transform.position.z);
                if (this.callbacksOnMove.ContainsKey(obj))
                {
                    this.callbacksOnMove[obj].Invoke(movementDetails);
                }
            }
            else if(movementDetails.HasNextPath())
            {
                movementDetails.GoToNextPath();
                if (this.callbacksOnPathChange.ContainsKey(obj))
                {
                    this.callbacksOnPathChange[obj].Invoke(movementDetails);
                }
            }
            else
            {
                this.movementDetailsPerObj.Remove(obj);
                Destroy(obj);
            }
        }
    }

    public class MovementDetails
    {
        public BonusBombMovement[] movements;
        public int idx;
        public float progress; //fraction of path traveled so far

        public MovementDetails(BonusBombMovement[] movements)
        {
            this.movements = movements;
            this.progress = 0;
            this.idx = 0;
        }

        public BezierPath CurrPath()
        {
            return this.movements[this.idx].path;
        }

        public float currSpeed()
        {
            return this.movements[this.idx].speed;
        }

        public bool HasNextPath()
        {
            return this.idx < this.movements.Length - 1;
        }

        public void GoToNextPath()
        {
            ++this.idx;
            this.progress = 0;
        }
    }
}
