using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooter : MonoBehaviour
{
    public GameObject pieceParent;
    public bool shootingEnabled = false;
    public GameObject piece;
    public float speed = 1.0f;
    public float shootDelaySec = 1.0f;
    private float lastShootTime = 0.0f;
    private GameObject spawnedPiece;
    private bool showingSpawnedPiece = false;
    private int piecesShotCount = 0;
    private float minAngle;
    private float maxAngle;
    private Rotator rotator;

    public void Init()
    {
        SpawnPiece();
        ResetPieceShotCount();
        transform.eulerAngles = new Vector3(0, 0, 0);
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
            Vector3 pieceRotation = spawnedPiece.transform.eulerAngles;
            spawnedPiece.transform.eulerAngles = new Vector3(pieceRotation.x, pieceRotation.y, transform.eulerAngles.z);
        }

        if (shootingEnabled)
        {
            float angle = transform.eulerAngles.z;
            if (angle > 90)
            {
                angle = angle - 360;
            }
            if (angle < minAngle || angle > maxAngle)
            {
                float newAngle = GetRotator().GetDir() == Rotator.RotationDir.Clockwise ? maxAngle : minAngle;
                Vector3 rotation = transform.eulerAngles;
                transform.eulerAngles = new Vector3(rotation.x, rotation.y, angle < minAngle ? minAngle : maxAngle);
                GetRotator().Reverse();
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
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

    public void SetAngleRange(Vector2 angleRange)
    {
        minAngle = angleRange.x;
        maxAngle = angleRange.y;
        GetRotator().enabled = minAngle != maxAngle;
    }

    private Rotator GetRotator()
    {
        if(rotator == null)
        {
            rotator = GetComponent<Rotator>();
        }
        return rotator;
    }

    void SpawnPiece()
    {
        spawnedPiece = Instantiate(piece, gameObject.transform.position, gameObject.transform.rotation);
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

    public void SetShootingEnabled(bool shootingEnabled)
    {
        this.shootingEnabled = shootingEnabled;
    }
}
