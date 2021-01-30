using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static DialogueData;
using static UnityEngine.InputSystem.InputAction;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueData dialogueData;

    public List<Dialogue> pendingDialogues;

    [SerializeField]
    private DialoguePanel dialoguePanel;

    public UnityEvent OnDialogueFinished;

    private Dialogue onGoingDialogue;
    public bool dialogueOnGoing = false;

    private void Awake()
    {
        dialoguePanel.gameObject.SetActive(false);
        pendingDialogues = new List<Dialogue>();
    }

    public void AddDialougueSequence(DialogueSequence sequence)
    {
        foreach (Dialogue dialogue in sequence.dialogues)
        {
            pendingDialogues.Add(dialogue);
        }

        dialoguePanel.gameObject.SetActive(true);

        TriggerNextDialogueOrCloseDialogueWindow();
    }

    public void NextDialogue()
    {
        if (!dialogueOnGoing)
        {
            TriggerNextDialogueOrCloseDialogueWindow();
        }
    }

    public void TriggerNextDialogueOrCloseDialogueWindow()
    {
        if (pendingDialogues.Count > 0)
        {
            dialogueOnGoing = true;

            Dialogue dialogue = pendingDialogues[0];
            pendingDialogues.Remove(dialogue);

            onGoingDialogue = dialogue;

            dialoguePanel.StartDialogue(dialogue.text, dialogue.icon);

            dialoguePanel.OnDialogueFinished.AddListener(DialogueFinished);
        }
        else
        {
            dialoguePanel.gameObject.SetActive(false);
        }
    }

    private void DialogueFinished()
    {
        dialogueOnGoing = false;

        onGoingDialogue.OnDialogueFinished?.Invoke();

        OnDialogueFinished?.Invoke();
        dialoguePanel.OnDialogueFinished.RemoveListener(DialogueFinished);
    }
}
