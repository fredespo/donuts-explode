/*This script created with help from this vide: https://youtu.be/tdSmKaJvCoA
 * and https://learn.unity.com/tutorial/introduction-to-object-pooling */

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class ParticleEndSound : MonoBehaviour
{
    public float poolReturnTimer = 1.5f;
    public float explosionMinVolume = 0.3f;
    public float explosionMaxVolume = 0.7f;
    public float explosionPitchMin = 0.75f;
    public float explosionPitchMax = 1.25f;
    public float shootMinVolume = 0.05f;
    public float shootMaxVolume = 0.1f;
    public float shootPitchMin = 0.75f;
    public float shootPitchMax = 1.25f;
    public AudioClip[] audioExplosion;
    public AudioClip[] audioShot;
    public AudioMixerGroup mixer;
    

    public static ParticleEndSound SharedInstance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    // Need 2 pools, 1 for explosion and 1 for shooting
    // Create 2 pools in the prefab with these tags: AudioExplosion and AudioShot
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        // Creating prefab instances from all pools;
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject tmp = Instantiate(pool.prefab);
                AudioSource explosionComponent = tmp.GetComponent<AudioSource>();
                if (pool.tag == "AudioExplosion")
                {
                    explosionComponent.clip = audioExplosion[UnityEngine.Random.Range(0, audioExplosion.Length)];
                    explosionComponent.volume = UnityEngine.Random.Range(explosionMinVolume, explosionMaxVolume);
                    explosionComponent.pitch = UnityEngine.Random.Range(explosionPitchMin, explosionPitchMax);
                    explosionComponent.outputAudioMixerGroup = this.mixer;
                }
                else if (pool.tag == "AudioShot")
                {
                    explosionComponent.clip = audioShot[UnityEngine.Random.Range(0, audioExplosion.Length)];
                    explosionComponent.volume = UnityEngine.Random.Range(shootMinVolume, shootMaxVolume);
                    explosionComponent.pitch = UnityEngine.Random.Range(shootPitchMin, shootPitchMax);
                    explosionComponent.outputAudioMixerGroup = this.mixer;
                }
                tmp.transform.parent = gameObject.transform;
                tmp.SetActive(false);
                objectPool.Enqueue(tmp);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // To get prefabs from pool
    public GameObject SpawnFromPool (string tag, Vector3 position)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not excist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    

    public void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
        int length = GetComponent<ParticleSystem>().GetParticles(particles);
        int i = 0;
        while (i < length)
        {
            if (audioExplosion.Length > 0 && particles[i].remainingLifetime < Time.deltaTime)
            {
                GameObject soundInstance = ParticleEndSound.SharedInstance.SpawnFromPool("AudioExplosion", particles[i].position);
                if(soundInstance != null)
                {
                    StartCoroutine(LateCall(soundInstance));
                }
            }
            if (audioShot.Length > 0 && particles[i].remainingLifetime >= particles[i].startLifetime - Time.deltaTime)
            {
                GameObject soundInstance = ParticleEndSound.SharedInstance.SpawnFromPool("AudioShot", particles[i].position);
                if (soundInstance != null)
                {
                    StartCoroutine(LateCall(soundInstance));
                }
            }
            i++;
        }
    }

    // Return Instances to the pool
    private IEnumerator LateCall(GameObject soundInstance)
    {
        yield return new WaitForSeconds(poolReturnTimer);
        soundInstance.SetActive(false);
    }
}
