using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueData;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueData dialogueData;

    [SerializeField]
    private DialoguePanel dialoguePanel;

    public void TriggerDialogue(int index)
    {
        Dialogue dialogue = dialogueData.dialogues[index];
        dialoguePanel.StartDialogue(dialogue.text, dialogue.icon);
    }
}
