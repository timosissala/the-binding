using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterMovement : Movement
{
    private PathFinder pathFinder;

    [SerializeField]
    private List<Transform> patrolPoints;
    private int patrolIndex;
    private Vector2 patrolTarget;

    [SerializeField]
    private float monsterWaitTime = 1.0f;

    [SerializeField]
    private float playerDetectRadius = 2.5f;

    private enum MovementMode
    {
        DoNothing,
        ChasePlayer,
        Patrol,
        RoamAimlessly
    }

    private MovementMode movementMode = MovementMode.DoNothing;

    private void Start()
    {
        pathFinder = new PathFinder(gameData.groundMap, gameData.terrainMap);
        mouseMovement = true;

        movementMode = MovementMode.Patrol;
        patrolIndex = 0;
    }

    private void Update()
    {
        if (mouseMovement && Time.timeSinceLevelLoad > monsterWaitTime)
        {
            DecideMovement();

            if (Vector2.Distance(transform.position, gameData.playerWorldPosition) < playerDetectRadius)
            {
                movementMode = MovementMode.ChasePlayer;
            }
        }
    }

    private void DecideMovement()
    {
        if (movementMode == MovementMode.ChasePlayer)
        {
            Vector2 playerPos = gameData.playerWorldPosition;
            if (playerPos != null)
            {
                MoveTowardsPlayer();
            }
        }
        else if (movementMode == MovementMode.Patrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (patrolTarget == null || Vector2.Distance(transform.position, patrolTarget) < 0.2f)
        {
            patrolIndex = patrolIndex < patrolPoints.Count - 1 ? patrolIndex + 1 : 0;

            patrolTarget = patrolPoints[patrolIndex].position;
        }

        MoveTowards(patrolTarget);
    }

    private void MoveTowardsPlayer()
    {
        Vector3Int tilemapPos = gameData.WorldToTIlePosition(transform.position, gameData.groundMap);
        Vector3Int tilemapPlayerPos = gameData.WorldToTIlePosition(gameData.playerWorldPosition, gameData.groundMap);

        Vector2 targetDirection = pathFinder.GetTargetDirection(new Vector2Int(tilemapPos.x, tilemapPos.y), new Vector2Int(tilemapPlayerPos.x, tilemapPlayerPos.y));
        targetDirection = gameData.groundMap.CellToWorld(new Vector3Int((int)targetDirection.x, (int)targetDirection.y, 0));

        SetMoveAnimation(targetDirection);

        rb.velocity = targetDirection * maxSpeed;
    }
}
