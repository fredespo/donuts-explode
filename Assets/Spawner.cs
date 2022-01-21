using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float intervalSeconds = 1.0f;
    public float probabilityPerInterval = 1.0f;
    public GameObject[] toSpawn;
    private IEnumerator spawnCoroutine;

    public void Init()
    {
        this.spawnCoroutine = SpawnCoroutine();
        StartCoroutine(this.spawnCoroutine);
    }

    public void Stop()
    {
        StopCoroutine(this.spawnCoroutine);
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            if (Random.value < this.probabilityPerInterval)
            {
                Spawn();
            }
            yield return new WaitForSeconds(this.intervalSeconds);
        }
    }

    private void Spawn()
    {
        if(this.toSpawn.Length == 0)
        {
            return;
        }

        int randSpawnIndex = Random.Range(0, this.toSpawn.Length);
        Instantiate(this.toSpawn[randSpawnIndex], transform);
    }

    public void DestroyAllChildren()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
