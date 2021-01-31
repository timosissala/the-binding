using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : Movement
{
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private TorchController torchController;

    [SerializeField]
    private LevelData levelData;

    private Vector2 mouseWorldPos;

    private Vector2 movementAxis;

    private bool allowInput = false;
    private bool isAlive = true;

    public UnityEvent OnDeath;

    [SerializeField]
    private DialogueManager dialogueManager;

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
        if (mouseMovement && mouseWorldPos != null && allowInput && isAlive && !dialogueManager.dialogueOnGoing)
        {
            MoveTowards(mouseWorldPos);
        }
        else if (movementAxis.magnitude > 0 && isAlive && allowInput && !dialogueManager.dialogueOnGoing)
        {
            Move(movementAxis);
        }
        else if (Time.timeSinceLevelLoad > 2.0f)
        {
            animatorController.StartIdleAnimation();
        }

        gameData.playerWorldPosition = transform.position;
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
        else if (collision.tag == "Exit")
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        int nextLevelIndex = levelData.currentLevel + 1;

        if (levelData.Levels.Length > nextLevelIndex)
        {
            levelData.currentLevel = nextLevelIndex;
            SceneManager.LoadScene(levelData.Levels[nextLevelIndex].levelName);
        }
        else
        {
            levelData.currentLevel = 0;
            SceneManager.LoadScene(levelData.MainMenu);
        }
    }
}
