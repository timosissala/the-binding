using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 0.05f;

    [SerializeField, Range(0.1f, 1.0f)]
    private float moveSensitivity = 0.3f;

    [SerializeField]
    private new Camera camera;

    private bool isMoving;
    private Vector2 mouseWorldPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }
    }

    private void Update()
    {
        if (isMoving && mouseWorldPos != null)
        {
            MoveTowards(mouseWorldPos);
        }
    }

    public void Interact(CallbackContext callbackContext)
    {
        bool moveButton = callbackContext.ReadValueAsButton();

        isMoving = moveButton;
    }

    public void MousePosition(CallbackContext callbackContext)
    {
        mouseWorldPos = camera.ScreenToWorldPoint(callbackContext.ReadValue<Vector2>());
    }

    public void MoveTowards(Vector2 target)
    {
        float distanceToMouse = Vector2.Distance(transform.position, target);
        float moveSpeed = distanceToMouse * maxSpeed * moveSensitivity < maxSpeed ? distanceToMouse * maxSpeed * moveSensitivity : maxSpeed;

        Debug.Log(moveSpeed);

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed);
    }
}
