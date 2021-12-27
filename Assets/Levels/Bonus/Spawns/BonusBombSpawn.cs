using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BonusBombSpawn", menuName = "Bonus/BonusBombSpawn")]
public class BonusBombSpawn : ScriptableObject
{
    [SerializeField]
    public GameObject bomb;
    [SerializeField]
    public BonusBombMovement[] movements;
    [Tooltip("Destroy the object once it has traveled this fraction of the last path specified above.")]
    [SerializeField]
    public float destroyAt = 0.8f;
}