using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseFlareSpawner : MonoBehaviour
{
    public GameObject flare;
    public Vector2 spawnDistanceRange;
    public Vector2 rotationRange;
    public float spawnDelay = 1.0f;
    private float spawnTimer = 0.0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnDelay)
        {
            SpawnFlare();
            spawnTimer = 0.0f;
        }
    }

    void SpawnFlare()
    {
        GameObject spawnedFlare = Instantiate(flare);
        spawnedFlare.transform.SetParent(gameObject.transform);
        float degrees = UnityEngine.Random.Range(rotationRange.x, rotationRange.y);
        float radians = degrees * Mathf.Deg2Rad;
        float spawnDistance = UnityEngine.Random.Range(spawnDistanceRange.x, spawnDistanceRange.y);
        float x = Mathf.Cos(radians) * spawnDistance;
        float y = Mathf.Sin(radians) * spawnDistance;
        spawnedFlare.transform.localPosition = new Vector3(x, y, 0);
        spawnedFlare.transform.localEulerAngles = new Vector3
        (
            gameObject.transform.localRotation.x,
            gameObject.transform.localRotation.y,
            degrees
        );
    }
}
