using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed = 1.0f;
    private HashSet<BombPiece> caughtPieces = new HashSet<BombPiece>();
    private HashSet<BombPiece> leftPieces = new HashSet<BombPiece>();
    private string tagToLookFor;
    private Camera mCamera;
    private BombHole hole;

    void Start()
    {
        tagToLookFor = transform.parent.tag;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hole = transform.parent.GetComponent<BombHole>();
    }

    void FixedUpdate()
    {
        foreach(BombPiece piece in caughtPieces)
        {
            if(piece != null && !this.leftPieces.Contains(piece) && piece.CaughtInMagnet())
            {
                MoveTowardsMagnet(piece.gameObject);
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
            caughtPieces.Add(col.gameObject.GetComponent<BombPiece>());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(tagToLookFor))
        {
            BombPiece piece = col.gameObject.GetComponent<BombPiece>();
            leftPieces.Add(piece);
            piece.LeftMagnet();
        }
    }
}
