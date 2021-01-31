using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : Movement
{
    [SerializeField]
    private new Camera camera;

    private Vector2 mouseWorldPos;

    private Vector2 movementAxis;

    private bool allowInput = false;

    private void Awake()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }
    }

    private void Update()
    {
        if (mouseMovement && mouseWorldPos != null && allowInput)
        {
            MoveTowards(mouseWorldPos);

            gameData.playerWorldPosition = transform.position;
        }
        else if (movementAxis.magnitude > 0)
        {
            Move(movementAxis);
        }
        else if (Time.timeSinceLevelLoad > 2.0f)
        {
            animatorController.StartIdleAnimation();
        }
    }

    public void Interact(CallbackContext callbackContext)
    {
        bool moveButton = callbackContext.ReadValueAsButton();

        mouseMovement = moveButton;
    }

    public void AxisMovement(CallbackContext callbackContext)
    {
        movementAxis = callbackContext.ReadValue<Vector2>();

        Debug.Log(movementAxis);
    }

    public void MousePosition(CallbackContext callbackContext)
    {
        mouseWorldPos = camera.ScreenToWorldPoint(callbackContext.ReadValue<Vector2>());
    }

    public void ToggleMovement(bool enabled)
    {
        allowInput = enabled;
    }
}
