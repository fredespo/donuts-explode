using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectsOnRails))]
public class BonusBombs : MonoBehaviour
{
    public AudioSource spawnSoundEffect;
    public float spawnSoundEffectPitchSpread;
    private float spawnSoundEffectPitchOrig;
    public float explodeAfterTraveledPct = 0.8f;
    public int numBonusBombsDefuzed;
    public BonusLevelWinUI winUI;
    public Score score;
    public int pointsPerBombDefuzed = 100;
    public UnityEvent onBonusLevelComplete;
    private ObjectsOnRails rails;
    private LevelLoader.BonusLevelSpawn[] spawns;
    private float defaultBombSpeed;
    private bool doneSpawning;
    private int bonusPoints;
    private DataStorage dataStorage;

    void Start()
    {
        this.rails = GetComponent<ObjectsOnRails>();
        this.dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        this.spawnSoundEffectPitchOrig = this.spawnSoundEffect.pitch;
    }

    public void Init(LevelLoader.BonusLevelSpawn[] spawns, float defaultBombSpeed)
    {
        this.spawns = spawns;
        this.defaultBombSpeed = defaultBombSpeed;
        this.doneSpawning = false;
        this.numBonusBombsDefuzed = 0;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i < this.spawns.Length; ++i)
        {
            LevelLoader.BonusLevelSpawn curr = this.spawns[i];
            yield return new WaitForSeconds(curr.spawnDelaySec);
            GameObject spawn = Instantiate(curr.obj, curr.path.GetPosAlongPath2D(0), Quaternion.identity, this.transform);
            this.spawnSoundEffect.pitch = Random.Range(this.spawnSoundEffectPitchOrig - this.spawnSoundEffectPitchSpread, this.spawnSoundEffectPitchOrig + this.spawnSoundEffectPitchSpread);
            this.spawnSoundEffect.Play();
            this.rails.Add(spawn, curr.path, this.defaultBombSpeed);
            this.rails.SetMoveCallback(spawn, (obj, pctTraveled) => {
                obj.GetComponent<Bomb>().SetFuzePct(pctTraveled / this.explodeAfterTraveledPct);
                if (pctTraveled >= this.explodeAfterTraveledPct)
                {
                    BonusBomb bonusBomb = obj.GetComponent<BonusBomb>();
                    if(!bonusBomb.IsDefuzed())
                    {
                        this.rails.Remove(spawn);
                        bonusBomb.Explode();
                    }
                    else
                    {
                        this.rails.ClearMoveCallbacks(spawn);
                    }
                }

            });
        }
        this.doneSpawning = true;
    }

    void Update()
    {
        if(this.doneSpawning && NumBonusBombsInFlight() == 0)
        {
            BonusLevelComplete();
            this.doneSpawning = false;
        }
    }

    private void BonusLevelComplete()
    {
        this.onBonusLevelComplete.Invoke();
        this.bonusPoints = this.pointsPerBombDefuzed * this.numBonusBombsDefuzed;
        this.winUI.RevealAndAwardBonus(this.numBonusBombsDefuzed, this.bonusPoints);
    }

    public void DefuzedBonusBomb()
    {
        ++this.numBonusBombsDefuzed;
    }

    private int NumBonusBombsInFlight()
    {
        int count = 0;
        foreach(Transform child in transform)
        {
            if(child.tag == "BonusBomb" || child.tag == "Explosion")
            {
                ++count;
            }
        }
        return count;
    }
}
