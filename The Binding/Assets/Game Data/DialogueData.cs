using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    [SerializeField]
    public List<Dialogue> dialogues;

    [System.Serializable]
    public class Dialogue
    {
        [SerializeField]
        public string name = "Dialogue";
        [SerializeField]
        public string text = "";
        [SerializeField]
        public Sprite icon = null;
    }
}
