using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooter : MonoBehaviour
{
    public GameObject pieceParent;
    public bool shootingEnabled = false;
    public GameObject piece;
    public float speed = 1.0f;
    private GameObject spawnedPiece;
    private bool spawnedPieceReadyToShoot = false;
    private float minAngle;
    private float maxAngle;
    private Rotator rotator;

    public void Init()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (spawnedPiece == null)
        {
            SpawnPiece();
            spawnedPieceReadyToShoot = true;
        }
        else
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
        if (shootingEnabled && spawnedPiece != null && spawnedPieceReadyToShoot)
        {
            spawnedPiece.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            spawnedPieceReadyToShoot = false;
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
    }

    public void SetShootingEnabled(bool shootingEnabled)
    {
        this.shootingEnabled = shootingEnabled;
    }

    public void SetPiece(GameObject piece)
    {
        this.piece = piece;
    }
}
