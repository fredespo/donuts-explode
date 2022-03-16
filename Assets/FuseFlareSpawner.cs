using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseFlareSpawner : MonoBehaviour
{
    public GameObject flare;
    public int poolSize;
    public int numFlaresToAnimateAtOnce;
    private FuseFlare[] flares;
    public Vector2 spawnDistanceRange;
    public Vector2 rotationRange;
    public float delay = 1.0f;
    private float timer = 0.0f;
    private int flareIndex = 0;

    private void Start()
    {
        this.flares = new FuseFlare[this.poolSize];
        for(int i = 0; i < this.poolSize; ++i)
        {
            this.flares[i] = Instantiate(flare, transform).GetComponent<FuseFlare>();
            MoveFlare(this.flares[i]);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            MoveFlares(numFlaresToAnimateAtOnce);
            timer = 0.0f;
        }
    }

    void MoveFlares(int count)
    {
        for(int i = 0; i < count; i++)
        {
            FuseFlare f = this.flares[(this.flareIndex + i) % this.flares.Length];
            MoveFlare(f);
            f.Init();
        }
        this.flareIndex = (this.flareIndex + count) % this.flares.Length;
    }

    void MoveFlare(FuseFlare flare)
    {
        float degrees = UnityEngine.Random.Range(rotationRange.x, rotationRange.y);
        float radians = degrees * Mathf.Deg2Rad;
        float spawnDistance = UnityEngine.Random.Range(spawnDistanceRange.x, spawnDistanceRange.y);
        float x = Mathf.Cos(radians) * spawnDistance;
        float y = Mathf.Sin(radians) * spawnDistance;
        Transform t = flare.transform;
        t.localPosition = new Vector3(x, y, 0);
        t.localEulerAngles = new Vector3
        (
            gameObject.transform.localRotation.x,
            gameObject.transform.localRotation.y,
            degrees
        );
    }
}
