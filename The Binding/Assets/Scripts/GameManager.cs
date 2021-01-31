using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueData;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private DialogueData dialogueData;

    [SerializeField]
    private string levelStartDialogue;

    private void Start()
    {
        playerMovement.ToggleMovement(true);

        DialogueSequence dialogueSequence = dialogueData.GetDialogueSequence(levelStartDialogue);

        if (dialogueSequence !=null)
        {
            dialogueManager.AddDialougueSequence(dialogueSequence);
        }
    }
}
