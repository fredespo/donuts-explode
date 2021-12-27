using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusLevel", menuName = "Bonus/BonusLevel")]
public class BonusLevel : ScriptableObject
{
    [SerializeField]
    public int afterLevel;

    [SerializeField]
    public SpawnData[] spawns;

    [System.Serializable]
    public class SpawnData
    {
        [SerializeField]
        public float delay;
        [SerializeField]
        public BonusBombSpawn spawn;
    }
}
