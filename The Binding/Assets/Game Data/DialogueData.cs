using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    [SerializeField]
    public List<DialogueSequence> dialogueSequences;

    [System.Serializable]
    public class DialogueSequence
    {
        [SerializeField]
        public string name = "DialogueSequence";
        [SerializeField]
        public List<Dialogue> dialogues;
    }

    [System.Serializable]
    public class Dialogue
    {
        [SerializeField]
        public string text = "";
        [SerializeField]
        public Sprite icon = null;

        public UnityEvent OnDialogueFinished;
    }

    public DialogueSequence GetDialogueSequence(string name)
    {
        DialogueSequence sequence = dialogueSequences
            .Where(t => t.name.ToLower() == name.ToLower())
            .FirstOrDefault();

        return sequence;
    }
}
