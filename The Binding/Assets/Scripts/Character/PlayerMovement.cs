using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : Movement
{
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private TorchController torchController;

    private Vector2 mouseWorldPos;

    private Vector2 movementAxis;

    private bool allowInput = false;
    private bool isAlive = true;

    public UnityEvent OnDeath;

    private void Awake()
    {
        isAlive = true;

        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }
    }

    private void Update()
    {
        if (mouseMovement && mouseWorldPos != null && allowInput && isAlive)
        {
            MoveTowards(mouseWorldPos);

            gameData.playerWorldPosition = transform.position;
        }
        else if (movementAxis.magnitude > 0 && isAlive)
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

    private void Die()
    {
        isAlive = false;

        OnDeath?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fuel")
        {
            collision.gameObject.SetActive(false);

            torchController.TorchFuel += 2;
        }
        else if (collision.tag == "Monster")
        {
            Die();
        }
    }
}
