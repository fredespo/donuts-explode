using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed = 1.0f;
    public float marginDist = 0.08f;
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
            if(piece != null && !this.leftPieces.Contains(piece))
            {
                if(piece.CaughtInMagnet(transform.parent.transform.parent.transform.parent))
                {
                    MoveTowardsMagnet(piece.gameObject);
                }

                if (Vector3.Distance(transform.position, piece.gameObject.transform.position) <= this.marginDist)
                {
                    this.hole.FillWith(piece.gameObject);
                }
            }
        }
    }

    private void MoveTowardsMagnet(GameObject obj)
    {
        Vector3 objPos = obj.transform.position;
        float offsetX = GetMovementX(objPos);
        float offsetY = GetMovementY(objPos);
        obj.transform.position = new Vector3(objPos.x + offsetX, objPos.y + offsetY, objPos.z);
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
