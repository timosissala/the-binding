using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField]
    protected GameData gameData;

    [SerializeField]
    protected float maxSpeed = 1.0f;

    [SerializeField, Range(0.1f, 1.0f)]
    protected float moveSensitivity = 0.3f;

    protected bool isMoving;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTowards(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(transform.position, target);
        float moveSpeed = distanceToTarget * maxSpeed * moveSensitivity;

        rb.velocity = Vector2.MoveTowards(transform.position, target, ClampSpeed(moveSpeed)) - (Vector2)transform.position;
    }

    public void MoveTowardsMaxSpeed(Vector2 target)
    {
        float moveSpeed = maxSpeed;

        rb.velocity = Vector2.MoveTowards(transform.position, target, moveSpeed) - (Vector2)transform.position;
    }

    private float ClampSpeed(float speed)
    {
        return speed < maxSpeed ? speed : maxSpeed;
    }
}
