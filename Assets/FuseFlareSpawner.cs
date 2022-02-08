using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseFlareSpawner : MonoBehaviour
{
    public GameObject flare;
    public Vector2 spawnDistanceRange;
    public Vector2 rotationRange;
    public float spawnsPerSecond = 60;
    public int numConcurrent = 20;
    private float numToSpawn= 0;

    void Update()
    {
        this.numToSpawn += this.spawnsPerSecond * Time.deltaTime;
        int numToSpawnThisFrame = (int)this.numToSpawn;
        if (numToSpawnThisFrame > 0)
        {
            this.numToSpawn %= numToSpawnThisFrame;
        }

        for(int i = 0; i < numToSpawnThisFrame; ++i)
        {
            FuseFlare spawnedFlare = SpawnFlare();
            spawnedFlare.fadeSpeed = 1 / (Time.deltaTime * this.numConcurrent);
        }
    }

    FuseFlare SpawnFlare()
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
        return spawnedFlare.GetComponent<FuseFlare>();
    }
}
