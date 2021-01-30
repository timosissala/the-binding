using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public Vector2 playerWorldPosition;

    public List<Vector2> pickupWorldPositions;
}
