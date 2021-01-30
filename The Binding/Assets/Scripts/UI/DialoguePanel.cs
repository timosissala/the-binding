using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private Image dialogueIcon;

    [SerializeField]
    private float textDelay = 0.05f;

    public UnityEvent OnDialogueFinished;

    public void StartDialogue(string text, Sprite iconSprite = null)
    {
        dialogueIcon.enabled = iconSprite != null;
        
        if (dialogueIcon.enabled)
        {
            dialogueIcon.sprite = iconSprite == null ? null : iconSprite;
        }

        StopAllCoroutines();
        StartCoroutine(AnimatedDialogue(text));
    }

    private IEnumerator AnimatedDialogue(string text)
    {
        dialogueText.text = "";
        char[] chars = text.ToCharArray();
        
        for (int i = 0; i < chars.Length; i++)
        {
            dialogueText.text += chars[i];

            yield return new WaitForSeconds(textDelay);
        }

        OnDialogueFinished?.Invoke();
    }

    private void ClearText()
    {
        dialogueText.text = "";
    }
}
