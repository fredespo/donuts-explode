﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsOnRails : MonoBehaviour
{
    private Dictionary<GameObject, MovementDetails> movementDetailsPerObj = new Dictionary<GameObject, MovementDetails>();
    private Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>> callbacksOnMove = new Dictionary<GameObject, Action<ObjectsOnRails.MovementDetails>>();
    private Vector3 centerPos;

    void Start()
    {
        this.centerPos = GameObject.FindWithTag("center").transform.position;
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
                Vector2 newPosOnPath = movementDetails.CurrPath().GetPosAlongPath2D(movementDetails.progress, this.centerPos);
                obj.transform.position = new Vector3(newPosOnPath.x, newPosOnPath.y, obj.transform.position.z);
                if (this.callbacksOnMove.ContainsKey(obj))
                {
                    this.callbacksOnMove[obj].Invoke(movementDetails);
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
