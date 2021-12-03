﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectsOnRails))]
public class BonusBombs : MonoBehaviour
{
    public UnityEvent onBonusLevelComplete;
    private ObjectsOnRails rails;
    private LevelLoader.BonusLevelSpawn[] spawns;
    private bool doneSpawning;
    private DataStorage dataStorage;

    void Start()
    {
        this.rails = GetComponent<ObjectsOnRails>();
        this.dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
    }

    public void Init(LevelLoader.BonusLevelSpawn[] spawns)
    {
        this.spawns = spawns;
        this.doneSpawning = false;
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
        this.doneSpawning = true;
    }

    void Update()
    {
        if(this.doneSpawning && transform.childCount == 0)
        {
            BonusLevelComplete();
        }
    }

    private void BonusLevelComplete()
    {
        this.onBonusLevelComplete.Invoke();
    }
}
