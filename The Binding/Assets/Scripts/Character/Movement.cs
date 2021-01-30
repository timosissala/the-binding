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

    [SerializeField]
    protected AnimatorController animatorController;

    protected bool isMoving;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTowards(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(transform.position, target);
        float moveSpeed = distanceToTarget * maxSpeed * moveSensitivity;

        Vector2 velocity = Vector2.MoveTowards(transform.position, target, ClampSpeed(moveSpeed)) - (Vector2)transform.position;
        SetMoveAnimation(velocity);

        rb.velocity = velocity;
    }

    protected void SetMoveAnimation(Vector2 velocity)
    {
        if (animatorController != null)
        {
            Vector2 normalizedVelocity = velocity.normalized;

            string animationName;

            if (normalizedVelocity.magnitude > 0)
            {
                animationName = "move_";

                float difference = Mathf.Abs(normalizedVelocity.x) - Mathf.Abs(normalizedVelocity.y);
                float absoluteDifference = Mathf.Abs(difference);

                if (absoluteDifference > 0.5f)
                {
                    if (difference > 0)
                    {
                        if (normalizedVelocity.x > 0)
                        {
                            animationName += "east";
                        }
                        else
                        {
                            animationName += "west";
                        }
                    }
                    else
                    {
                        if (normalizedVelocity.y > 0)
                        {
                            animationName += "north";
                        }
                        else
                        {
                            animationName += "south";
                        }
                    }
                }
                else
                {
                    if (normalizedVelocity.x > 0 && normalizedVelocity.y > 0)
                    {
                        animationName += "northeast";
                    }
                    else if (normalizedVelocity.x > 0 && normalizedVelocity.y < 0)
                    {
                        animationName += "southeast";
                    }
                    else if (normalizedVelocity.x < 0 && normalizedVelocity.y < 0)
                    {
                        animationName += "southwest";
                    }
                    else if (normalizedVelocity.x < 0 && normalizedVelocity.y > 0)
                    {
                        animationName += "northwest";
                    }
                }
            }
            else
            {
                animationName = "idle";
            }

            animatorController.StartTransitionTo(animationName);
        }
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
