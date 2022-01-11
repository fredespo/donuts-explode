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
    public AudioSource bombHittingSideSoundEffect;
    public int numBonusBombsDefuzed;
    public GameObject winUI;
    public Score score;
    public int pointsPerBombDefuzed = 100;
    public float endingDelay;
    public UnityEvent onAllBombsDestroyed;
    public UnityEvent onBonusLevelComplete;
    private ObjectsOnRails rails;
    private BonusLevel.SpawnData[] spawns;
    private bool doneSpawning;
    private int bonusPoints;
    private DataStorage dataStorage;
    private Vector3 centerPos;

    void Start()
    {
        this.rails = GetComponent<ObjectsOnRails>();
        this.dataStorage = GameObject.FindGameObjectWithTag("DataStorage").GetComponent<DataStorage>();
        this.spawnSoundEffectPitchOrig = this.spawnSoundEffect.pitch;
        this.centerPos = GameObject.FindWithTag("center").transform.position;
    }

    public void Init(BonusLevel.SpawnData[] spawns)
    {
        this.spawns = spawns;
        this.doneSpawning = false;
        this.numBonusBombsDefuzed = 0;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i < this.spawns.Length; ++i)
        {
            yield return new WaitForSeconds(this.spawns[i].delay);
            BonusBombSpawn bonusSpawn = this.spawns[i].spawn;
            GameObject bomb = Instantiate(bonusSpawn.bomb, bonusSpawn.movements[0].path.GetPosAlongPath2D(0, this.centerPos), Quaternion.identity, this.transform);
            this.spawnSoundEffect.pitch = Random.Range(this.spawnSoundEffectPitchOrig - this.spawnSoundEffectPitchSpread, this.spawnSoundEffectPitchOrig + this.spawnSoundEffectPitchSpread);
            this.spawnSoundEffect.Play();
            this.rails.Add(bomb, bonusSpawn.movements);
            this.rails.SetMoveCallback(bomb, (movementDetails) => {
                bomb.GetComponent<Bomb>().SetFuzePct((movementDetails.progress / bonusSpawn.destroyAt) * ((movementDetails.idx + 1) / movementDetails.movements.Length));
                if (movementDetails.idx == movementDetails.movements.Length - 1 && movementDetails.progress >= bonusSpawn.destroyAt)
                {
                    BonusBomb bonusBomb = bomb.GetComponent<BonusBomb>();
                    if(!bonusBomb.IsDefuzed())
                    {
                        this.rails.Remove(bomb);
                        bonusBomb.Explode();
                    }
                    else
                    {
                        this.rails.ClearMoveCallbacks(bomb);
                    }
                }

            });
            this.rails.SetPathChangeCallback(bomb, (movementDetails) =>
            {
                this.bombHittingSideSoundEffect.Play();
            });
        }
        this.doneSpawning = true;
    }

    void Update()
    {
        if(this.doneSpawning && NumBonusBombsInFlight() == 0)
        {
            this.onAllBombsDestroyed.Invoke();
            StartCoroutine(BonusLevelCompleteCoroutine());
            this.doneSpawning = false;
        }
    }

    private IEnumerator BonusLevelCompleteCoroutine()
    {
        yield return new WaitForSeconds(this.endingDelay);
        BonusLevelComplete();
    }

    private void BonusLevelComplete()
    {
        this.onBonusLevelComplete.Invoke();
        this.bonusPoints = this.pointsPerBombDefuzed * this.numBonusBombsDefuzed;
        GameObject winUI = Instantiate(this.winUI, transform.parent.transform);
        winUI.GetComponent<BonusLevelWinUI>().RevealAndAwardBonus(this.numBonusBombsDefuzed, this.bonusPoints);
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
