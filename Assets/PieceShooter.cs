using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooter : MonoBehaviour
{
    public GameObject pieceParent;
    public GameObject piece;
    public float speed = 1.0f;
    public float shootDelaySec = 1.0f;
    private float lastShootTime = 0.0f;
    private GameObject spawnedPiece;
    private bool showingSpawnedPiece = false;
    private int piecesShotCount = 0;

    public void Init()
    {
        SpawnPiece();
        ResetPieceShotCount();
    }

    void Update()
    {
        if (Time.time - lastShootTime >= shootDelaySec && !showingSpawnedPiece)
        {
            SpawnPiece();
        }
    }

    public void Shoot()
    {
        if (Time.time - lastShootTime >= shootDelaySec && showingSpawnedPiece)
        {
            spawnedPiece.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            showingSpawnedPiece = false;
            ++piecesShotCount;
            lastShootTime = Time.time;
        }
    }

    void SpawnPiece()
    {
        spawnedPiece = Instantiate(piece);
        spawnedPiece.transform.SetParent(pieceParent.transform);
        spawnedPiece.transform.position = gameObject.transform.position;
        spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
        showingSpawnedPiece = true;
    }

    private void ResetPieceShotCount()
    {
        piecesShotCount = 0;
    }

    public int GetPiecesShotCount()
    {
        return piecesShotCount;
    }
}
