using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private DialogueData dialogueData;

    private void Start()
    {
        dialogueManager.AddDialougueSequence(dialogueData.GetDialogueSequence("Awakening"));
    }
}
