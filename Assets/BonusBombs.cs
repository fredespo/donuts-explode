using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectsOnRails))]
public class BonusBombs : MonoBehaviour
{
    private ObjectsOnRails rails;
    private LevelLoader.BonusLevelSpawn[] spawns;

    void Start()
    {
        this.rails = GetComponent<ObjectsOnRails>();
    }

    public void Init(LevelLoader.BonusLevelSpawn[] spawns)
    {
        this.spawns = spawns;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i < this.spawns.Length; ++i)
        {
            LevelLoader.BonusLevelSpawn curr = this.spawns[i];
            yield return new WaitForSeconds(curr.spawnDelaySec);
            GameObject spawn = Instantiate(curr.obj, curr.path.GetPosAlongPath2D(0), Quaternion.identity, this.transform);
            this.rails.Add(spawn, curr.path, curr.speed);
        }
    }
}
