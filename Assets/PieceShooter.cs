using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooter : MonoBehaviour
{
    public GameObject pieceParent;
    public bool shootingEnabled = false;
    private List<GameObject> pieces;
    private int pieceIndex = 0;
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

        if(showingSpawnedPiece)
        {
            spawnedPiece.SetActive(shootingEnabled);
        }
    }

    public void Shoot()
    {
        if (shootingEnabled && Time.time - lastShootTime >= shootDelaySec && showingSpawnedPiece)
        {
            spawnedPiece.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            showingSpawnedPiece = false;
            ++piecesShotCount;
            lastShootTime = Time.time;
        }
    }

    void SpawnPiece()
    {
        if(pieces != null && pieceIndex < pieces.Count && pieceIndex >= 0 && pieces[pieceIndex] != null)
        {
            spawnedPiece = Instantiate(pieces[pieceIndex]);
            spawnedPiece.transform.SetParent(pieceParent.transform);
            spawnedPiece.transform.position = gameObject.transform.position;
            spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
            showingSpawnedPiece = true;
        }
    }

    void RespawnPiece()
    {
        showingSpawnedPiece = false;
        if (spawnedPiece != null)
        {
            Destroy(spawnedPiece);
        }
        SpawnPiece();
    }

    private void ResetPieceShotCount()
    {
        piecesShotCount = 0;
    }

    public int GetPiecesShotCount()
    {
        return piecesShotCount;
    }

    public void SetShootingEnabled(bool shootingEnabled)
    {
        this.shootingEnabled = shootingEnabled;
    }

    public void SetPieces(List<GameObject> pieces)
    {
        this.pieces = pieces;
        this.pieceIndex = 0;
    }

    public void IncrementPieceIndex()
    {
        pieceIndex = (pieceIndex + 1) % pieces.Count;
        RespawnPiece();
    }

    public void DecrementPieceIndex()
    {
        --pieceIndex;
        if(pieceIndex < 0)
        {
            pieceIndex = pieces.Count - 1;
        }
        RespawnPiece();
    }
}
