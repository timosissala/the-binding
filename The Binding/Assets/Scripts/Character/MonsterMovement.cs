using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : Movement
{
    private void Start()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 playerPos = gameData.playerWorldPosition;
            if (playerPos != null)
            {
                MoveTowards(playerPos);
            }
        }
    }
}
