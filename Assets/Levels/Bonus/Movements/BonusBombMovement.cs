using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusBombMovement", menuName = "Bonus/BonusBombMovement")]
public class BonusBombMovement : ScriptableObject
{
    [SerializeField]
    public BezierPath path;
    public float speed = 0.6f;
}
