using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterMovement : Movement
{
    private PathFinder pathFinder;

    private enum MovementMode
    {
        DoNothing,
        ChasePlayer,
        RoamAimlessly
    }

    private MovementMode movementMode = MovementMode.DoNothing;

    private void Start()
    {
        pathFinder = new PathFinder(gameData.groundMap);
        isMoving = true;

        movementMode = MovementMode.ChasePlayer;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (movementMode == MovementMode.ChasePlayer)
            {
                Vector2 playerPos = gameData.playerWorldPosition;
                if (playerPos != null)
                {
                    MoveTowardsPlayer();
                }
            }
            else if (movementMode == MovementMode.RoamAimlessly)
            {
                
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3Int tilemapPos = gameData.WorldToTIlePosition(transform.position, gameData.groundMap);
        Vector3Int tilemapPlayerPos = gameData.WorldToTIlePosition(gameData.playerWorldPosition, gameData.groundMap);

        rb.velocity = (Vector2)pathFinder.GetTargetDirection(new Vector2Int(tilemapPos.x, tilemapPos.y), new Vector2Int(tilemapPlayerPos.x, tilemapPlayerPos.y)) * maxSpeed;
        Debug.Log(rb.velocity);
    }
}
