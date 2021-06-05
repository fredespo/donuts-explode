using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShooter : MonoBehaviour
{
    public GameObject pieceParent;
    public bool shootingEnabled = false;
    public GameObject[] pieces;
    public int pieceIndex = 0;
    public float speed = 1.0f;
    public AngleChangeMode angleChangeMode = AngleChangeMode.ALTERNATE;
    private bool alternateAngleMin = true;
    private GameObject spawnedPiece;
    private bool spawnedPieceReadyToShoot = false;
    private float minAngle;
    private float maxAngle;
    private Rotator rotator;
    private float currAngle;

    public void Init()
    {
        pieceIndex = 0;
        if (angleChangeMode == AngleChangeMode.ALTERNATE)
        {
            SetAngle(minAngle);
            alternateAngleMin = true;
        }
        else if(angleChangeMode == AngleChangeMode.TRI)
        {
            SetAngle(minAngle);
        }
        else
        {
            SetAngle(0);
        }
        SpawnPiece();
    }

    void Update()
    {
        if (spawnedPiece == null && CanSpawnPiece())
        {
            ChangeAngleIfNeeded();
            SpawnPiece();
        }
        else if(spawnedPieceReadyToShoot)
        {
            spawnedPiece.SetActive(shootingEnabled);
        }

        if (shootingEnabled)
        {
            if(angleChangeMode == AngleChangeMode.SMOOTH)
            {
                ChangeAngleSmoothly();
            }

            if (spawnedPieceReadyToShoot && spawnedPiece != null)
            {
                Vector3 pieceRotation = spawnedPiece.transform.eulerAngles;
                spawnedPiece.transform.eulerAngles = new Vector3(pieceRotation.x, pieceRotation.y, transform.eulerAngles.z);
            }
        }
    }

    private void ChangeAngleIfNeeded()
    {
        if (angleChangeMode == AngleChangeMode.ALTERNATE)
        {
            SetAngle(alternateAngleMin ? maxAngle : minAngle);
            alternateAngleMin = !alternateAngleMin;
        }
        else if (angleChangeMode == AngleChangeMode.TRI)
        {
            float midAngle = (this.minAngle + this.maxAngle) / 2;
            if (this.currAngle == this.minAngle)
            {
                SetAngle(midAngle);
            }
            else if (this.currAngle == midAngle)
            {
                SetAngle(this.maxAngle);
            }
            else
            {
                SetAngle(this.minAngle);
            }
        }
    }

    private void ChangeAngleSmoothly()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 90)
        {
            angle -= 360;
        }

        if (angle < minAngle || angle > maxAngle)
        {
            float newAngle = (GetRotator().GetDir() == Rotator.RotationDir.Clockwise ? maxAngle : minAngle);
            Vector3 rotation = transform.eulerAngles;
            transform.eulerAngles = new Vector3(rotation.x, rotation.y, angle < minAngle ? minAngle : maxAngle);
            GetRotator().Reverse();
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

    private void SetAngle(float angle)
    {
        currAngle = angle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }

    public void SetAngleRange(Vector2 angleRange)
    {
        minAngle = angleRange.x;
        maxAngle = angleRange.y;
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
        if (!CanSpawnPiece())
        {
            return;
        }

        if(GameObject.FindGameObjectsWithTag(pieces[pieceIndex].transform.tag).Length == 0)
        {
            ++pieceIndex;
        }

        if(pieceIndex < pieces.Length)
        {
            spawnedPiece = Instantiate(pieces[pieceIndex], gameObject.transform.position, gameObject.transform.rotation);
            spawnedPiece.transform.SetParent(pieceParent.transform);
            spawnedPiece.transform.position = gameObject.transform.position;
            spawnedPiece.transform.localScale = new Vector3(81, 81, 1);
        }

        spawnedPieceReadyToShoot = true;
    }

    public void SetShootingEnabled(bool shootingEnabled)
    {
        this.shootingEnabled = shootingEnabled;
    }

    public void SetAngleChangeMode(AngleChangeMode mode)
    {
        angleChangeMode = mode;
        GetRotator().enabled = (mode == AngleChangeMode.SMOOTH);
    }

    public enum AngleChangeMode
    {
        ALTERNATE, SMOOTH, TRI
    }

    public bool IsSpawnedPieceReadyToShoot()
    {
        return this.spawnedPieceReadyToShoot;
    }

    public void Inactivate()
    {
        gameObject.SetActive(false);
    }

    private bool CanSpawnPiece()
    {
        return pieceIndex >= 0 && pieceIndex < pieces.Length;
    }
}
