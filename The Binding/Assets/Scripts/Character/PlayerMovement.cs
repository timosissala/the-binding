using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : Movement
{
    [SerializeField]
    private new Camera camera;

    private Vector2 mouseWorldPos;

    private void Awake()
    {
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

            gameData.playerWorldPosition = transform.position;
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
}
